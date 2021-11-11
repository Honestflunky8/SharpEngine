namespace SharpEngine
{
    public struct Color
    {
        public float r, g, b, a;
        public static readonly Color Red = new Color(1f, 0, 0, 1f);
        public static readonly Color Green = new Color(0, 1f, 0, 1f);
        public static readonly Color Blue = new Color(0, 0, 1f, 1f);
        public static readonly Color Greenish = new Color(0.5f, 1, 0.4f, 1f);
        public static readonly Color Purple = new Color(0.2f, .05f, .2f, 1f);

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}