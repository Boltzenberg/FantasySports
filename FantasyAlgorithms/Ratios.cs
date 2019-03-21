namespace FantasyAlgorithms
{
    public static class Ratios
    {
        public static float Divide(int n, int d)
        {
            return (float)n / (float)d;
        }

        public static float PerNineInnings(int n, int d)
        {
            return (float)n / ((float)d / 27);
        }

        public static float PerInning(int n, int d)
        {
            return (float)n / ((float)d / 3);
        }
    }
}
