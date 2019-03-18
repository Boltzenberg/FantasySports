using System.Collections.Generic;
using System.Linq;

namespace FantasyAlgorithms
{
    public static class DataSourceMerge
    {
        public class YahooPosition<U>
        {
            public U Position;
            public int PlayerCount;
            public float PercentMatch;
        }

        public class YahooPlayer<U>
        {
            public string Name;
            public List<U> Positions;
        }

        public class ESPNPositionMap<T, U>
        {
            public T ESPNPosition;
            public List<YahooPosition<U>> YahooPosition;
            public List<YahooPlayer<U>> Players;
        }

        public static Dictionary<U, T> MergeOnName<T, U>(Dictionary<string, List<T>> ds1, Dictionary<string, List<U>> ds2)
        {
            Dictionary<T, List<string>> inverseDS1 = new Dictionary<T, List<string>>();
            foreach (string s in ds1.Keys)
            {
                foreach (T iKey in ds1[s])
                {
                    List<string> keys;
                    if (!inverseDS1.TryGetValue(iKey, out keys))
                    {
                        keys = new List<string>();
                        inverseDS1[iKey] = keys;
                    }
                    keys.Add(s);
                }
            }

            List<ESPNPositionMap<T, U>> epms = new List<ESPNPositionMap<T, U>>();
            List<T> positions = new List<T>(inverseDS1.Keys);
            foreach (T position in positions)
            {
                Dictionary<U, YahooPosition<U>> yPositionMap = new Dictionary<U, YahooPosition<U>>();
                ESPNPositionMap<T, U> epm = new ESPNPositionMap<T, U>();
                epms.Add(epm);
                epm.ESPNPosition = position;

                List<YahooPlayer<U>> yps = new List<YahooPlayer<U>>();
                foreach (string ePlayer in inverseDS1[position])
                {
                    List<U> ds2Val;
                    if (ds2.TryGetValue(ePlayer, out ds2Val))
                    {
                        yps.Add(new YahooPlayer<U>() { Name = ePlayer, Positions = ds2Val });

                        foreach (U yPos in ds2Val)
                        {
                            YahooPosition<U> yahooPosition;
                            if (!yPositionMap.TryGetValue(yPos, out yahooPosition))
                            {
                                yahooPosition = new YahooPosition<U>();
                                yahooPosition.Position = yPos;
                                yahooPosition.PlayerCount = 0;
                                yPositionMap[yPos] = yahooPosition;
                            }

                            yahooPosition.PlayerCount++;
                        }
                    }
                }
                epm.Players = yps;
                epm.YahooPosition = new List<YahooPosition<U>>(yPositionMap.Values);
            }

            HashSet<U> ypKeys = new HashSet<U>();
            Dictionary<T, List<YahooPosition<U>>> ep2yps = new Dictionary<T, List<YahooPosition<U>>>();
            foreach (ESPNPositionMap<T, U> ep in epms)
            {
                List<YahooPosition<U>> yps = new List<YahooPosition<U>>();
                ep2yps[ep.ESPNPosition] = yps;
                int totalPlayers = ep.Players.Count;
                foreach (YahooPosition<U> yp in ep.YahooPosition)
                {
                    yp.PercentMatch = (float)yp.PlayerCount / (float)totalPlayers;
                    if (yp.PercentMatch > .9f)
                    {
                        ypKeys.Add(yp.Position);
                        yps.Add(yp);
                    }
                }
            }

            Dictionary<U, T> yp2ep = new Dictionary<U, T>();
            foreach (U yp in ypKeys)
            {
                int currentMax = 0;
                foreach (T ep in ep2yps.Keys)
                {
                    YahooPosition<U> yPos = ep2yps[ep].FirstOrDefault(y => y.Position.Equals(yp));
                    if (yPos != null && yPos.PlayerCount > currentMax)
                    {
                        currentMax = yPos.PlayerCount;
                        yp2ep[yp] = ep;
                    }
                }
            }

            return yp2ep;
        }
    }
}
