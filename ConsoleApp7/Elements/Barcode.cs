namespace ConsoleApp7.Elements
{
    public class Barcode : Element
    {
        public float XPos { get; }

        public float YPos { get; }

        public float Height { get; }

        public float BarWidthFactor { get; }

        public string Content { get; }

        public Barcode(
            float xPos,
            float yPos,
            float height,
            float barWidthFactor,
            string content)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Height = height;
            this.BarWidthFactor = barWidthFactor;
            this.Content = content;
        }
    }
}
