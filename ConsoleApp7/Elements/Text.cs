using System;
using System.Xml;

namespace ConsoleApp7.Elements
{
    public class Text : Element
    {
        public float FontSize { get; set; }

        public float Width { get; set; }

        public string Content { get; set; }

        public TextStyle Style { get; set; } = TextStyle.Regular;

        public Orientation Orientation { get; set; } = Orientation.Normal;

        public Text(XmlTextReader node)
            : base(node)
        {
            this.Width = float.Parse(node.GetAttribute("width"));
            this.FontSize = float.Parse(node.GetAttribute("size"));
            this.Content = node.GetAttribute("content");

            string style = node.GetAttribute("style");

            if (string.IsNullOrWhiteSpace(style) == false)
            {
                if (style.ToLower() == "bold")
                {
                    this.Style = TextStyle.Bold;
                }
            }

            string orientation = node.GetAttribute("orientation");

            if (string.IsNullOrWhiteSpace(orientation) == false)
            {
                this.Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation);
            }
        }
    }
}
