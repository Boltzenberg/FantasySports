using ClaudeExporter.DataModel;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClaudeExporter
{
    public class NirDriver
    {
        private const string HOMEPAGE = "http://www.fantasybaseballdraft.top/League.aspx";
        private const string BIDITEMS = "http://www.fantasybaseballdraft.top/BidItems.aspx";

        private WebView2 webview;

        public NirDriver(WebView2 webview)
        {
            this.webview = webview;
        }

        public async Task InitializeAsync()
        {
            await this.webview.EnsureCoreWebView2Async();
        }

        private async Task NavigateAsync(string url)
        {
            await this.webview.EnsureCoreWebView2Async();

            var tcs = new TaskCompletionSource<bool>();

            void Handler(object sender, CoreWebView2NavigationCompletedEventArgs e)
            {
                // Only complete when the navigation that *we* triggered finishes
                if (e.IsSuccess)
                {
                    string currentUrl = this.webview.Source?.ToString() ?? "";
                    if (currentUrl == url)
                    {
                        this.webview.CoreWebView2.NavigationCompleted -= Handler;
                        tcs.TrySetResult(true);
                    }
                }
                else
                {
                    this.webview.CoreWebView2.NavigationCompleted -= Handler;
                    tcs.TrySetException(new Exception($"Navigation failed: {e.WebErrorStatus}"));
                }
            }

            this.webview.CoreWebView2.NavigationCompleted += Handler;

            this.webview.CoreWebView2.Navigate(url);

            await tcs.Task; // Wait for navigation to finish
        }


        private async Task WaitForPageChangeAsync(int previousPage)
        {
            while (true)
            {
                string js = @"
            (() => {
                const pager = document.querySelector('#dgPlayers tr td[colspan]');
                if (!pager) return -1;

                const current = pager.querySelector('span');
                if (!current) return -1;

                return parseInt(current.innerText.trim(), 10);
            })();
        ";

                var json = await webview.CoreWebView2.ExecuteScriptAsync(js);
                int currentPage = JsonSerializer.Deserialize<int>(json);

                if (currentPage != previousPage)
                    break;

                await Task.Delay(100);
            }
        }

        public async Task<List<BidItem>> GetPlayersUpForAuction()
        {
            await NavigateAsync(BIDITEMS);

            var results = new List<BidItem>();

            // JS: extract rows
            string jsExtractRows = @"
        (() => {
            const table = document.getElementById('dgPlayers');
            if (!table) return [];

            const rows = Array.from(table.querySelectorAll('tr')).slice(1, -1);

            return rows.map(row => {
                const cells = row.querySelectorAll('td');

                const name = cells[0].innerText.trim();
                const mlbTeam = cells[1].innerText.trim();
                const rank = parseInt(cells[2].innerText.trim());
                const preseasonRank = parseInt(cells[3].innerText.trim());
                const positions = cells[4].innerText.trim();
                const highestBidder = cells[5].innerText.trim();

                const currentBidText = cells[6].innerText.trim();
                const currentBid = currentBidText.startsWith('$')
                    ? parseFloat(currentBidText.substring(1))
                    : 0;

                const oldPriceText = cells[7].innerText.trim();
                const oldPrice = oldPriceText.startsWith('$')
                    ? parseFloat(oldPriceText.substring(1))
                    : 0;

                const timeParts = cells[8].innerText.trim().split(' ');
                const timeRemaining = {
                    days: parseInt(timeParts[0]),
                    hours: parseInt(timeParts[1]),
                    minutes: parseInt(timeParts[2]),
                    seconds: parseInt(timeParts[3])
                };

                const topper = cells[10].innerText.trim();

                const yahooUrl = cells[11].querySelector('a')?.href ?? '';
                const yahooId = yahooUrl
                    ? yahooUrl.substring(yahooUrl.lastIndexOf('/') + 1)
                    : '';

                return {
                    name,
                    mlbTeam,
                    rank,
                    preseasonRank,
                    positions,
                    highestBidder,
                    currentBid,
                    oldPrice,
                    timeRemaining,
                    topper,
                    yahooId
                };
            });
        })();
    ";

            // JS: click next page
            string jsClickNext = @"
        (() => {
            const pager = document.querySelector('#dgPlayers tr td[colspan]');
            if (!pager) return false;

            const links = [...pager.querySelectorAll('a')];

            const current = pager.querySelector('span');
            if (!current) return false;

            const currentPage = parseInt(current.innerText.trim(), 10);
            const next = links.find(a => parseInt(a.innerText.trim(), 10) === currentPage + 1);

            if (next) {
                next.click();
                return true;
            }

            const ellipsis = links.find(a => 
                a.innerText.trim() === '...' &&
                (current.compareDocumentPosition(a) & Node.DOCUMENT_POSITION_FOLLOWING));
            if (ellipsis) {
                ellipsis.click();
                return true;
            }

            return false;
        })();
    ";

            // JS to read current page number
            string jsGetPage = @"
        (() => {
            const pager = document.querySelector('#dgPlayers tr td[colspan]');
            if (!pager) return -1;

            const current = pager.querySelector('span');
            if (!current) return -1;

            return parseInt(current.innerText.trim(), 10);
        })();
    ";

            while (true)
            {
                // Extract rows
                var json = await webview.CoreWebView2.ExecuteScriptAsync(jsExtractRows);

                var items = JsonSerializer.Deserialize<List<BidItem>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (items != null)
                    results.AddRange(items);

                // Read current page number
                var pageJson = await webview.CoreWebView2.ExecuteScriptAsync(jsGetPage);
                int currentPage = JsonSerializer.Deserialize<int>(pageJson);

                // Try to click next page
                var clickedJson = await webview.CoreWebView2.ExecuteScriptAsync(jsClickNext);
                bool clicked = JsonSerializer.Deserialize<bool>(clickedJson);

                if (!clicked)
                    break;

                // Wait for DOM to stabilize (pager number changes)
                await WaitForPageChangeAsync(currentPage);
            }

            return results;
        }

        public async Task<Dictionary<Team, List<RosteredPlayer>>> GetTeamRosters()
        {
            await this.NavigateAsync(HOMEPAGE);

            string script = @"
                (() => {
                    const table = document.getElementById('dgTeams');
                    if (!table) return { teams: [], rows: [] };

                    const rows = Array.from(table.querySelectorAll('tr'));

                    // First row: team names
                    const headerCells = rows[0].querySelectorAll('td');
                    const teamNames = Array.from(headerCells)
                        .slice(1)
                        .map(td => td.innerText.split('\r')[0].trim());

                    // Remaining rows: roster data
                    const rosterRows = rows.slice(1).map((row, rowIndex) => {
                        const cells = row.querySelectorAll('td');
                        const rowIndexText = cells[0].innerText.trim();

                        return {
                            rowIndex: rowIndexText,
                            cells: Array.from(cells).slice(1).map(cell => {
                                const text = cell.innerText.trim();
                                const link = cell.querySelector('a');
                                const href = link ? link.href : '';

                                return {
                                    text,
                                    href
                                };
                            })
                        };
                    });

                    return {
                        teams: teamNames,
                        rows: rosterRows
                    };
                })();
                ";

            string json = await this.webview.CoreWebView2.ExecuteScriptAsync(script);
            var data = JsonSerializer.Deserialize<TeamsTableResult>(json);

            var colToTeam = new Dictionary<int, Team>();
            var results = new Dictionary<Team, List<RosteredPlayer>>();

            for (int i = 0; i < data.teams.Count; i++)
            {
                string[] raw = data.teams[i].Split('\n');
                string teamName = raw[0].Trim();
                string balance = raw[1].Substring(raw[1].IndexOf(":") + 1).Trim();

                Team team = new Team(teamName, balance);
                colToTeam[i + 1] = team;
                results[team] = new List<RosteredPlayer>();
            }


            for (int r = 0; r < data.rows.Count; r++)
            {
                var row = data.rows[r];

                // Validate row index
                if (row.rowIndex != (r + 1).ToString())
                    throw new Exception("The first column should be the row index!");

                for (int c = 0; c < row.cells.Count; c++)
                {
                    var cell = row.cells[c];
                    if (!string.IsNullOrWhiteSpace(cell.text))
                    {
                        string rest = cell.text.Trim();

                        // Extract name
                        int parenIndex = rest.IndexOf('(');
                        string name = parenIndex > 0
                            ? rest.Substring(0, parenIndex).Trim()
                            : rest;

                        // Extract Yahoo ID
                        string yahooUrl = cell.href;
                        string yahooId = yahooUrl.Substring(yahooUrl.LastIndexOf("/") + 1);

                        // Parse MLB team + positions
                        int openParen = rest.LastIndexOf('(');
                        int spaceAfterTeamName = rest.IndexOf(' ', openParen);
                        int spaceBeforePositions = rest.IndexOf(' ', spaceAfterTeamName + 1);
                        int closeParen = rest.IndexOf(')', openParen);

                        string mlbTeamName = rest.Substring(openParen + 1, spaceAfterTeamName - openParen - 1);
                        string positions = rest.Substring(spaceBeforePositions + 1, closeParen - spaceBeforePositions - 1);

                        // Parse price
                        string priceStr = rest.Substring(rest.IndexOf('$') + 1).Trim();
                        float price = float.Parse(priceStr, CultureInfo.InvariantCulture);

                        results[colToTeam[c + 1]].Add(
                            new RosteredPlayer(name, yahooId, mlbTeamName, positions, price)
                        );
                    }
                }
            }

            return results;
        }
    }
}
