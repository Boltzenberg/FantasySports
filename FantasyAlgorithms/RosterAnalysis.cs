using FantasyAlgorithms.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public static class RosterAnalysis
    {
        public static void AssignStatsAndPoints(List<IRoster> teams, List<IStatExtractor> extractors)
        {
            foreach (IRoster team in teams)
            {
                foreach (IStatExtractor extractor in extractors)
                {
                    List<IStatValue> statValues = new List<IStatValue>();
                    foreach (IPlayer p in team.Players)
                    {
                        IStatValue value = extractor.Extract(p);
                        if (value != null)
                        {
                            statValues.Add(value);
                        }
                    }

                    IStatValue agg = extractor.Aggregate(statValues);
                    team.Stats[extractor.StatName] = agg.Value;
                }
            }

            foreach (IStatExtractor extractor in extractors)
            {
                if (extractor.MoreIsBetter)
                {
                    teams.Sort((x, y) => y.Stats[extractor.StatName].CompareTo(x.Stats[extractor.StatName]));
                }
                else
                {
                    teams.Sort((x, y) => x.Stats[extractor.StatName].CompareTo(y.Stats[extractor.StatName]));
                }

                for (int i = 0; i < teams.Count;)
                {
                    int countOfEqual = 1;
                    int totalPoints = teams.Count - i;
                    while (i + countOfEqual < teams.Count && teams[i + countOfEqual - 1].Stats[extractor.StatName] == teams[i + countOfEqual].Stats[extractor.StatName])
                    {
                        totalPoints += teams.Count - (i + countOfEqual);
                        countOfEqual++;
                    }

                    float pointsPer = (float)totalPoints / countOfEqual;
                    for (int k = 0; k < countOfEqual; k++)
                    {
                        teams[i + k].Points[extractor.StatName] = pointsPer;
                    }

                    i += countOfEqual;
                }
            }
        }

        public static float ExtractTotalPoints(IRoster roster)
        {
            float total = 0.0f;
            foreach (float points in roster.Points.Values)
            {
                total += points;
            }

            return total;
        }
    }
}
