using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAlgorithms.DataModel
{
    [Flags]
    public enum Position
    {
        C,
        B1,
        B2,
        B3,
        SS,
        OF,
        Util,
        MI,
        CI,

        P,

        BN,
        DL,
        NA
    }

    public static class Positions
    {
        public static readonly HashSet<Position> DisplayPositions = new HashSet<Position>()
        { Position.C, Position.B1, Position.B2, Position.B3, Position.SS, Position.OF, Position.Util, Position.P };

        public static string ToString(Position p)
        {
            switch (p)
            {
                case Position.C: return "Catcher";
                case Position.B1: return "First Base";
                case Position.B2: return "Second Base";
                case Position.B3: return "Third Base";
                case Position.SS: return "Shortstop";
                case Position.MI: return "Middle Infielder";
                case Position.CI: return "Corner Infielder";
                case Position.OF: return "Outfield";
                case Position.Util: return "Utility";
                case Position.P: return "Pitcher";
                case Position.BN: return "Bench";
                case Position.DL: return "Disabled List";
                case Position.NA: return "Not Active";
            }

            return "Unknown";
        }

        public static string ToShortString(Position p)
        {
            switch (p)
            {
                case Position.C: return "C";
                case Position.B1: return "1B";
                case Position.B2: return "2B";
                case Position.B3: return "3B";
                case Position.SS: return "SS";
                case Position.MI: return "MI";
                case Position.CI: return "CI";
                case Position.OF: return "OF";
                case Position.Util: return "Util";
                case Position.P: return "P";
                case Position.BN: return "BN";
                case Position.DL: return "DL";
                case Position.NA: return "NA";
            }

            return "??";
        }

        public static IEnumerable<string> FieldPositionsToShortStrings(IEnumerable<Position> ps)
        {
            foreach (Position p in ps)
            {
                if (DisplayPositions.Contains(p))
                {
                    yield return ToShortString(p);
                }
            }
        }

        public static bool IsActive(Position p)
        {
            return (p != Position.NA && p != Position.DL && p != Position.BN);
        }
    }
}
