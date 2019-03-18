using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyAlgorithms.DataModel
{
    public class Roster : IRoster
    {
        private Dictionary<Position, List<Spot>> spots = new Dictionary<Position, List<Spot>>();
        public IDictionary<string, float> Stats { get; private set; }
        public IDictionary<string, float> Points { get; private set; }

        public Roster(Dictionary<Position, int> spotCount)
        {
            this.Stats = new Dictionary<string, float>();
            this.Points = new Dictionary<string, float>();

            foreach (Position p in spotCount.Keys)
            {
                List<Spot> spotsForPosition = new List<Spot>();
                for (int i = 0; i < spotCount[p]; i++)
                {
                    spotsForPosition.Add(new Spot(p));
                }
                this.spots[p] = spotsForPosition;
            }
        }

        private Roster(Roster other)
        {
            foreach (Position p in other.spots.Keys)
            {
                this.spots[p] = new List<Spot>(other.spots[p]);
            }

            this.Stats = new Dictionary<string, float>(other.Stats);
            this.Points = new Dictionary<string, float>(other.Points);
        }

        public Roster Clone()
        {
            return new Roster(this);
        }

        public IReadOnlyDictionary<Position, List<Spot>> Lineup
        {
            get
            {
                return this.spots;
            }
        }

        public Position GetPositionForPlayer(IPlayer player)
        {
            foreach (Position position in this.spots.Keys)
            {
                foreach (IPlayer p in this.spots[position])
                {
                    if (p == player)
                    {
                        return position;
                    }
                }
            }

            return Position.NA;
        }

        public Dictionary<string, Roster> AddPlayers(IEnumerable<IPlayer> players)
        {
            List<IPlayer> allPlayers = new List<IPlayer>(players);
            Dictionary<string, Roster> results = new Dictionary<string, Roster>();
            DoGetAllRosters(new List<IPlayer>(players), this.Clone(), results);
            return results;
        }

        private void DoGetAllRosters(List<IPlayer> players, Roster current, Dictionary<string, Roster> results)
        {
            if (players.Count == 0)
            {
                string key = current.TeamName;
                if (!results.ContainsKey(key))
                {
                    results[key] = current.Clone();
                }
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                IPlayer player = players[i];
                players.RemoveAt(i);
                foreach (Roster newRoster in current.AddPlayer(player))
                {
                    this.DoGetAllRosters(players, newRoster.Clone(), results);
                }
                players.Insert(i, player);
            }
        }

        private IEnumerable<Roster> AddPlayer(IPlayer player)
        {
            Roster other = this.Clone();
            foreach (Position p in player.Positions)
            {
                List<Spot> positionSpots = other.spots[p];
                foreach (Spot spot in positionSpots)
                {
                    if (spot.Player == null)
                    {
                        spot.Player = player;
                        yield return other;
                        spot.Player = null;
                        break;
                    }
                }
            }
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                foreach (Position p in this.spots.Keys)
                {
                    if (Positions.IsActive(p))
                    {
                        foreach (Spot s in this.spots[p])
                        {
                            if (s.Player != null)
                            {
                                yield return s.Player;
                            }
                        }
                    }
                }
            }
        }

        public string TeamName
        {
            get
            {
                List<string> names = new List<string>(this.Players.Select(p => p.Name));
                names.Sort();
                StringBuilder sb = new StringBuilder();
                foreach (string name in names)
                {
                    sb.Append(name);
                    sb.Append("|");
                }
                return sb.ToString();
            }
        }
        
        public class Spot
        {
            public Position Position { get; }
            public IPlayer Player { get; set; }

            public Spot(Position p)
            {
                this.Position = p;
                this.Player = null;
            }

            public override string ToString()
            {
                return string.Format("{0}: {1}", Positions.ToString(this.Position), this.Player == null ? "Unassigned" : this.Player.Name);
            }
        }
    }
}
