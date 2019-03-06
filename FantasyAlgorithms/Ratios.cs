namespace FantasyAlgorithms
{
    public static class Ratios
    {
        public static float Divide(int n, int d)
        {
            return (float)n / (float)d;
        }

        public static float ERA(int n, int d)
        {
            return (float)n / ((float)d / 27);
        }

        public static float WHIP(int n, int d)
        {
            return (float)n / ((float)d / 3);
        }
    }
}
