using ConsoleApp7.Elements;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsoleApp7
{
    public class DefinitionParser
    {
        public List<Element> ParseFile(string path)
        {
            var elements = new List<Element>();
            var reader = new XmlTextReader(path);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    Element node = ParseNode(reader);

                    if (node != null)
                    {
                        elements.Add(node);
                    }
                }
            }

            return elements;
        }

        private Element ParseNode(XmlTextReader node)
        {
            switch (node.Name)
            {
                case "Barcode":
                    float xPos = float.Parse(node.GetAttribute("xPos"));
                    float yPos = float.Parse(node.GetAttribute("yPos"));
                    float height = float.Parse(node.GetAttribute("height"));
                    float widthFactor = float.Parse(node.GetAttribute("widthFactor"));
                    string content = node.GetAttribute("content");
                    return new Barcode(xPos, yPos, height, widthFactor, content);

                default:
                    return null;
            }
        }
    }
}