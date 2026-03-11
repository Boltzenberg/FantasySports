using ClaudeExporter.DataModel;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
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

        public async Task<List<BidItem>> GetPlayersUpForAuction()
        {
            await this.NavigateAsync(BIDITEMS);

            string script = @"
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

                        const currentBid = cells[6].innerText.trim();
                        const oldPrice = cells[7].innerText.trim();

                        const timeParts = cells[8].innerText.trim().split(' ');

                        const topper = cells[10].innerText.trim();

                        const yahooUrl = cells[11].querySelector('a')?.href ?? '';
                        const yahooId = yahooUrl.substring(yahooUrl.lastIndexOf('/') + 1);

                        return {
                            name,
                            mlbTeam,
                            rank,
                            preseasonRank,
                            positions,
                            highestBidder,
                            currentBid,
                            oldPrice,
                            timeParts,
                            topper,
                            yahooId
                        };
                    });
                })();
                ";

            string json = await this.webview.CoreWebView2.ExecuteScriptAsync(script);
            var rawitems = JsonSerializer.Deserialize<List<RawBidItem>>(json);

            List<BidItem> items = new List<BidItem>();
            foreach (var r in rawitems)
            {
                var culture = System.Globalization.CultureInfo.InvariantCulture;

                float currentBid = string.IsNullOrWhiteSpace(r.currentBid)
                    ? 0f
                    : float.Parse(r.currentBid.Substring(1), culture);

                float oldPrice = string.IsNullOrWhiteSpace(r.oldPrice)
                    ? 0f
                    : float.Parse(r.oldPrice.Substring(1), culture);

                string daysStr = r.timeParts[0].Substring(0, r.timeParts[0].Length - 1);
                string hoursStr = r.timeParts[1].Substring(0, r.timeParts[1].Length - 1);
                string minutesStr = r.timeParts[2].Substring(0, r.timeParts[2].Length - 1);
                string secondsStr = r.timeParts[3].Substring(0, r.timeParts[3].Length - 1);

                TimeSpan timeRemaining = new TimeSpan(
                    int.Parse(daysStr),
                    int.Parse(hoursStr),
                    int.Parse(minutesStr),
                    int.Parse(secondsStr)
                );

                items.Add(new BidItem(
                    r.name,
                    r.mlbTeam,
                    r.rank,
                    r.preseasonRank,
                    r.positions,
                    r.highestBidder,
                    currentBid,
                    oldPrice,
                    timeRemaining,
                    r.topper,
                    r.yahooId
                ));
            }

            /*
            IWebElement auctionTable = this.driver.FindElement(By.Id("dgPlayers"));
            IList<IWebElement> auctionRows = auctionTable.FindElements(By.TagName("tr"));
            for (int r = 1; r < auctionRows.Count - 1; r++)
            {
                IWebElement row = auctionRows[r];
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                string name = cells[0].Text;
                string mlbTeam = cells[1].Text;
                int rank = int.Parse(cells[2].Text);
                int preseasonRank = int.Parse(cells[3].Text);
                string positions = cells[4].Text;
                string highestBidder = cells[5].Text;
                float currentBid = string.IsNullOrEmpty(cells[6].Text.Trim()) ? 0f : float.Parse(cells[6].Text.Substring(1));
                float oldPrice = string.IsNullOrEmpty(cells[7].Text.Trim()) ? 0f : float.Parse(cells[7].Text.Substring(1));
                string[] timeRemainingStrs = cells[8].Text.Split(' ');
                TimeSpan timeRemaining = new TimeSpan(
                    int.Parse(timeRemainingStrs[0].Substring(0, timeRemainingStrs[0].Length - 1)),
                    int.Parse(timeRemainingStrs[1].Substring(0, timeRemainingStrs[1].Length - 1)),
                    int.Parse(timeRemainingStrs[2].Substring(0, timeRemainingStrs[2].Length - 1)),
                    int.Parse(timeRemainingStrs[3].Substring(0, timeRemainingStrs[3].Length - 1)));
                string topper = cells[10].Text;
                string yahooUrl = cells[11].FindElement(By.TagName("a")).GetAttribute("href");
                string yahooId = yahooUrl.Substring(yahooUrl.LastIndexOf("/") + 1);
                items.Add(new BidItem(name, mlbTeam, rank, preseasonRank, positions, highestBidder, currentBid, oldPrice, timeRemaining, topper, yahooId));
            }
            */
            return items;
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
                string balance = raw[1].Substring(raw[1].IndexOf("$")).Trim();

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

            /*
            Dictionary<int, string> colToTeamName = new Dictionary<int, string>();

            this.NavigateWithSignIn(HOMEPAGE);
            IWebElement teamsTable = this.driver.FindElement(By.Id("dgTeams"));
            IList<IWebElement> teamsRows = teamsTable.FindElements(By.TagName("tr"));

            // Get the team names first
            IList<IWebElement> teamsCells = teamsRows[0].FindElements(By.TagName("td"));
            for (int i = 1; i < teamsCells.Count; i++)
            {
                IWebElement teamsCell = teamsCells[i];
                string teamName = teamsCell.Text.Split('\r').First();
                colToTeamName[i] = teamName;
                results[teamName] = new List<RosteredPlayer>();
            }

            // Now get the roster per team
            for (int r = 1; r < teamsRows.Count; r++)
            {
                IWebElement row = teamsRows[r];
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                if (cells[0].Text != r.ToString())
                {
                    throw new Exception("The first column should be the row index!");
                }

                for (int c = 1; c < cells.Count; c++)
                {
                    IWebElement cell = cells[c];
                    if (!string.IsNullOrEmpty(cell.Text.Trim()))
                    {
                        IWebElement nameLink = cells[c].FindElement(By.TagName("a"));
                        string name = nameLink.Text.Trim();
                        string yahooLink = nameLink.GetAttribute("href");
                        string yahooId = yahooLink.Substring(yahooLink.LastIndexOf("/") + 1);
                        // (HOU - OF): $24.69
                        string rest = cells[c].Text;
                        int openParen = rest.LastIndexOf('(');
                        int spaceAfterTeamName = rest.IndexOf(' ', openParen);
                        int spaceBeforePositions = rest.IndexOf(' ', spaceAfterTeamName + 1);
                        int closeParen = rest.IndexOf(')', openParen);
                        string mlbTeamName = rest.Substring(openParen + 1, spaceAfterTeamName - openParen - 1);
                        string positions = rest.Substring(spaceBeforePositions + 1, closeParen - spaceBeforePositions - 1);
                        string priceStr = rest.Substring(rest.IndexOf('$') + 1).Trim();
                        float price = float.Parse(priceStr);
                        results[colToTeamName[c]].Add(new RosteredPlayer(name, yahooId, mlbTeamName, positions, price));
                    }
                }
            }
            */
            return results;
        }
    }
}
