using System;

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
        public static string ToString(Position p)
        {
            switch(p)
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

        public static bool IsActive(Position p)
        {
            return (p != Position.NA && p != Position.DL && p != Position.BN);
        }
    }
}
