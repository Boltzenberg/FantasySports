namespace FantasyAlgorithms.DataModel
{
    public class Team
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public float Budget { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
