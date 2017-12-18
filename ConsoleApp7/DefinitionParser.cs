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
                    return new Barcode(node);
                case "Box":
                    return new Box(node);
                case "Text":
                    return new Text(node);
                default:
                    return null;
            }
        }
    }
}