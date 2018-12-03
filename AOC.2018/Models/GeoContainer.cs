namespace AOC._2018.Models
{
    public class GeoContainer
    {
        public GeoContainer(int width, int height)
        {
            Map = new bool[width,height];
        }

        public bool[,] Map { get; set; }
    }
}