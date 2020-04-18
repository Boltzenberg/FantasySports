using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public class Player
    {
        // ID used by the data model to uniquely identify this player
        public int Id { get; private set; }

        // Display name of the player
        public string Name { get; private set; }

        // Player stats per stat set.
        public Dictionary<Constants.StatSource, IPlayerData> PlayerData { get; private set; }

        public Player(string name, int id)
        {
            this.Id = id;
            this.Name = name;
            this.PlayerData = new Dictionary<Constants.StatSource, IPlayerData>();
        }
    }
}
