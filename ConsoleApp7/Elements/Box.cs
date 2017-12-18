using System.Xml;

namespace ConsoleApp7.Elements
{
    public class Box : Element
    {
        public float Height { get; set; }

        public float Width { get; set; }

        public float Thickness { get; set; }

        public Box(XmlTextReader node)
            : base(node)
        {
            this.Width = float.Parse(node.GetAttribute("width"));
            this.Height = float.Parse(node.GetAttribute("height"));
            this.Thickness = float.Parse(node.GetAttribute("thickness"));
        }
    }
}
