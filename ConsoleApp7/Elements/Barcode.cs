using System.Xml;

namespace ConsoleApp7.Elements
{
    public class Barcode : Element
    {
        public float Height { get; set; }

        public float BarWidth { get; set; }

        public string Content { get; set; }

        public Barcode(XmlTextReader node)
            : base(node)
        {
            this.Height = float.Parse(node.GetAttribute("height"));
            this.BarWidth = float.Parse(node.GetAttribute("barWidth"));
            this.Content = node.GetAttribute("content");
        }
    }
}
