namespace GW2SDK.Colors
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int[] BaseRgb { get; set; }

        public ColorInfo Leather { get; set; }

        public ColorInfo Metal { get; set; }

        public ColorInfo Fur { get; set; }

        public ColorCategory[] Categories { get; set; }
    }
}