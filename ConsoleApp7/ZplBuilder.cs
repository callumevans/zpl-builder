using ConsoleApp7.Elements;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp7
{
    public class ZplBuilder
    {
        private readonly StringBuilder zplBuilder = new StringBuilder();

        private readonly float dpi;
        private readonly float width;
        private readonly float height;

        private readonly float xDotsTotal;
        private readonly float yDotsTotal;

        public ZplBuilder(float dpi, float width, float height)
        {
            this.dpi = dpi;
            this.width = width;
            this.height = height;

            xDotsTotal = dpi * width;
            yDotsTotal = dpi * height;
        }

        public string Build(IEnumerable<Element> elements)
        {
            zplBuilder.AppendLine("^XA");
            zplBuilder.AppendLine("^MUI");

            foreach (var element in elements)
            {
                switch (element)
                {
                    case Barcode barcode:
                        DrawBarcode(barcode);
                        break;
                }
            }

            zplBuilder.AppendLine("^XZ");
            return zplBuilder.ToString();
        }

        private void DrawBarcode(Barcode barcode)
        {
            float widthDots = dpi / barcode.BarWidthFactor;

            zplBuilder
                .AppendLine($"^FO{barcode.XPos},{barcode.YPos}")
                .AppendLine($"^BY{barcode.BarWidthFactor}")
                .AppendLine($"^BAN,{barcode.Height}")
                .AppendLine($"^FD{barcode.Content}^FS");
        }
    }
}
