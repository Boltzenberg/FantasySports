using FantasySports.DataModels.DataProcessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasySports.DataModels
{
    public class Root
    {
        // Set of Players
        public Dictionary<int, Player> Players { get; private set; }

        // Set of Leagues
        public Dictionary<int, ILeague> Leagues { get; private set; }

        public static Root Create()
        {
            return new Root();
        }

        public static Root Load(string fileName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            return JsonConvert.DeserializeObject<Root>(File.ReadAllText(fileName), settings);
        }

        public void Save(string fileName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, settings));
        }

        public Root Clone()
        {
            string content = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Root>(content);
        }

        private Root()
        {
            Players = new Dictionary<int, Player>();
            Leagues = new Dictionary<int, ILeague>();
        }
    }
}
