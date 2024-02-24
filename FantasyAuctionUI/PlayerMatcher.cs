using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class PlayerMatcher : Form
    {
        private List<IPlayer> espnPlayersWithoutYahooIds;
        private List<YahooFantasySports.DataModel.Player> potentialYahooPlayers;

        public PlayerMatcher(List<IPlayer> espnPlayersWithoutYahooIds, List<YahooFantasySports.DataModel.Player> potentialYahooPlayers)
        {
            this.espnPlayersWithoutYahooIds = espnPlayersWithoutYahooIds;
            this.potentialYahooPlayers = potentialYahooPlayers;
            InitializeComponent();

            this.potentialYahooPlayers.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            this.lbYahooPlayers.Items.Clear();
            this.lbYahooPlayers.Items.AddRange(this.potentialYahooPlayers.ToArray());

            this.espnPlayersWithoutYahooIds.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            this.lbEspn.Items.Clear();
            this.lbEspn.Items.AddRange(espnPlayersWithoutYahooIds.ToArray());
            this.lbEspn.SelectedIndex = 0;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (this.lbEspn.SelectedItem != null &&
                this.lbYahooPlayers.SelectedItem != null)
            {
                IPlayer espnPlayer = (IPlayer)this.lbEspn.SelectedItem;
                YahooFantasySports.DataModel.Player selectedPlayer = (YahooFantasySports.DataModel.Player)this.lbYahooPlayers.SelectedItem;
                espnPlayer.Update(selectedPlayer);
                this.wb.DocumentText = espnPlayer.GetHTML();
            }
        }

        private void OnESPNPlayerSelected(object sender, EventArgs e)
        {
            IPlayer espnPlayer = (IPlayer)this.lbEspn.SelectedItem;
            if (espnPlayer != null)
            {
                this.wb.DocumentText = espnPlayer.GetHTML();
                if (!string.IsNullOrEmpty(espnPlayer.YahooId))
                {
                    for (int i = 0; i < this.lbYahooPlayers.Items.Count; i++)
                    {
                        YahooFantasySports.DataModel.Player yahooPlayer = (YahooFantasySports.DataModel.Player)this.lbYahooPlayers.Items[i];
                        if (yahooPlayer.Id == espnPlayer.YahooId)
                        {
                            this.lbYahooPlayers.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this.lbYahooPlayers.Items.Count; i++)
                    {
                        YahooFantasySports.DataModel.Player yahooPlayer = (YahooFantasySports.DataModel.Player)this.lbYahooPlayers.Items[i];
                        if (yahooPlayer.Name.CompareTo(espnPlayer.Name) > 0)
                        {
                            this.lbYahooPlayers.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                IPlayer espnPlayer = (IPlayer)this.lbEspn.SelectedItem;
                if (espnPlayer != null)
                {
                    if (!string.IsNullOrEmpty(espnPlayer.YahooId))
                    {
                        espnPlayer.ClearYahooData();
                    }
                    else
                    {
                        YahooFantasySports.DataModel.Player selectedPlayer = (YahooFantasySports.DataModel.Player)this.lbYahooPlayers.SelectedItem;
                        espnPlayer.Update(selectedPlayer);
                    }
                    this.wb.DocumentText = espnPlayer.GetHTML();
                }
            }
        }
    }
}
