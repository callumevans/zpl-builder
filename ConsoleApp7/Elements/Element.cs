using System.Xml;

namespace ConsoleApp7.Elements
{
    public abstract class Element
    {
        public float XPos { get; set; }

        public float YPos { get; set; }

        public string DpiFilters { get; set; }

        public Alignment Alignment { get; set; }

        public Element(XmlTextReader node)
        {
            this.XPos = float.Parse(node.GetAttribute("xPos"));
            this.YPos = float.Parse(node.GetAttribute("yPos"));

            string alignment = node.GetAttribute("align");

            if (string.IsNullOrWhiteSpace(alignment) == false)
            {
                if (alignment.ToLower() == "right")
                {
                    this.Alignment = Alignment.Right;
                }
                else if (alignment.ToLower() == "auto")
                {
                    this.Alignment = Alignment.Auto;
                }
                else if (alignment.ToLower() == "left")
                {
                    this.Alignment = Alignment.Left;
                }
            }

            string dpiFilters = node.GetAttribute("when");

            if (string.IsNullOrWhiteSpace(dpiFilters) == false)
            {
                this.DpiFilters = dpiFilters;
            }
        }
    }
}
