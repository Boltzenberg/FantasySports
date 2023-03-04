using Newtonsoft.Json;
using System;
using System.IO;

namespace ConfigLib
{
    public static class Directories
    {
        public static string LeagueFileDirectory;
        public static string ProjectionCacheDirectory;

        static Directories()
        {
            LeagueFileDirectory = @"C:\Users\jon_r\OneDrive\Documents";
            ProjectionCacheDirectory = @"C:\Users\jon_r\OneDrive\Documents";

            try
            {
                string dirConfigFileName = Environment.GetEnvironmentVariable("COMPUTERNAME") + ".dirconfig.json";
                if (File.Exists(dirConfigFileName))
                {
                    DirConfig config = JsonConvert.DeserializeObject<DirConfig>(File.ReadAllText(dirConfigFileName));
                    LeagueFileDirectory = config.LeagueFileDir;
                    ProjectionCacheDirectory = config.ProjectionCacheDir;
                }
                else
                {
                    DirConfig config = new DirConfig()
                    {
                        LeagueFileDir = LeagueFileDirectory,
                        ProjectionCacheDir = ProjectionCacheDirectory
                    };

                    File.WriteAllText(dirConfigFileName, JsonConvert.SerializeObject(config));
                }
            }
            catch
            {
            }
        }

        private class DirConfig
        {
            public string LeagueFileDir;
            public string ProjectionCacheDir;
        }
    }
}
