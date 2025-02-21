using NirSiteLib.DataModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirSiteLib
{
    public class NirDriver : IDisposable
    {
        private const string HOMEPAGE = "http://www.fantasybaseballdraft.top/League.aspx";
        private const string BIDITEMS = "http://www.fantasybaseballdraft.top/BidItems.aspx";

        private IWebDriver driver;

        public NirDriver()
        {
            this.driver = new EdgeDriver();
        }

        public void Dispose()
        {
            if (this.driver != null)
            {
                this.driver.Quit();
                this.driver.Dispose();
                this.driver = null;
            }
        }

        private void NavigateWithSignIn(string url)
        {
            this.driver.Url = url;
            do
            {
                System.Threading.Thread.Sleep(1000);
            } while (this.driver.Url != url);
        }

        public List<BidItem> GetPlayersUpForAuction()
        {
            List<BidItem> items = new List<BidItem>();
            this.NavigateWithSignIn(BIDITEMS);
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

            return items;
        }

        public Dictionary<string, List<RosteredPlayer>> GetTeamRosters()
        {
            Dictionary<string, List<RosteredPlayer>> results = new Dictionary<string, List<RosteredPlayer>>();
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

            return results;
        }
    }
}
