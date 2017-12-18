using ConsoleApp7.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp7
{
    public class ZplBuilder
    {
        private const float BOLD_OFFSET_FACTOR = 0.07f;

        private readonly StringBuilder zplBuilder = new StringBuilder();

        private readonly int dpi;
        private readonly float width;
        private readonly float height;

        private readonly float xDotsTotal;
        private readonly float yDotsTotal;

        public ZplBuilder(int dpi, float width, float height)
        {
            this.dpi = dpi;
            this.width = width;
            this.height = height;

            xDotsTotal = dpi * width;
            yDotsTotal = dpi * height;
        }

        public string Build(IEnumerable<Element> elements)
        {
            zplBuilder.AppendLine("^XA").AppendLine("^MUM");

            foreach (var element in elements)
            {
                if (string.IsNullOrWhiteSpace(element.DpiFilters) == false)
                {
                    var filters = element.DpiFilters.Split('|')
                        .Select(int.Parse).ToList();

                    if (filters.Contains(dpi) == false)
                    {
                        continue;
                    }
                }

                AddOrigin(element);

                switch (element)
                {
                    case Barcode barcode:
                        DrawBarcode(barcode);
                        break;
                    case Box box:
                        DrawBox(box);
                        break;
                    case Text text:
                        DrawText(text);
                        break;
                }

                zplBuilder.AppendLine("^FS");
            }

            zplBuilder.AppendLine("^XZ");
            return zplBuilder.ToString();
        }

        private void DrawBarcode(Barcode barcode)
        {
            zplBuilder
                .AppendLine($"^BY{barcode.BarWidth},3,{barcode.Height}")
                .AppendLine($"^BCN,{barcode.Height}")
                .AppendLine($"^FD{barcode.Content}");
        }

        private void DrawBox(Box box)
        {
            zplBuilder
                .AppendLine($"^GB{box.Width},{box.Height},{box.Thickness}");
        }

        private void DrawText(Text text)
        {
            string alignment =
                (text.Alignment == Alignment.Right)
                ? "R"
                : "L";

            string orientation;

            switch (text.Orientation)
            {
                case Orientation.Right:
                    orientation = "R";
                    break;
                case Orientation.Inverted:
                    orientation = "I";
                    break;
                case Orientation.Bottom:
                    orientation = "B";
                    break;
                case Orientation.Normal:
                default:
                    orientation = "N";
                    break;
            }

            float width = MillimetresToDots(text.Width);

            zplBuilder
                .AppendLine($"^A0{orientation},{text.FontSize},{text.FontSize}")
                .AppendLine($"^FB1000,1,0,{alignment},0")
                .AppendLine($"^FD{text.Content}");

            // Re-draw the text with a slight offset from its original
            // position to present it as bold
            if (text.Style == TextStyle.Bold)
            {
                text.XPos = text.XPos - BOLD_OFFSET_FACTOR;
                text.YPos = text.YPos - BOLD_OFFSET_FACTOR;
                text.Style = TextStyle.Regular;

                zplBuilder.AppendLine("^FS");
                AddOrigin(text);

                DrawText(text);
            }
        }

        private void AddOrigin(Element element)
        {
            zplBuilder
                .AppendLine($"^FO{element.XPos},{element.YPos},{(int)element.Alignment}");
        }

        private float MillimetresToDots(float millimetres)
        {
            return (InchesToDots(MillimetresToInches(millimetres)));
        }

        private float MillimetresToInches(float millimetres)
        {
            return millimetres / 25.4f;
        }

        private float InchesToDots(float inches)
        {
            return inches * dpi;
        }
    }
}
