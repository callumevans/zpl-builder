using System;
using System.Windows.Forms;

namespace ConsoleApp7
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parser = new DefinitionParser();
            var elements = parser.ParseFile("./MyCustomLabel.splf");

            var builder = new ZplBuilder(600, 4, 6);

            string zpl = builder.Build(elements);

            Clipboard.SetText(zpl);
        }
    }
}