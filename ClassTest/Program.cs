using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlBuilder;

namespace ClassTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument("RandomPage");

            HtmlElement root = new HtmlElement("div", "topDiv", "randomClass");
            root.CssClass.AddClass("abc");
            root.CssClass.AddClass("asdfjklj");
            root.CssClass.AddClass("butt");
            root.CssClass.RemoveClass("asdfjklj");
            root.CssClass.RemoveClass("abc");
            root.CssClass.RemoveClass("RandomClass");
            root.CssClass.RemoveClass("butt");

            HtmlTable table = new HtmlTable();
            table.SetColumns(new List<string> { "aaa", "bbb", "ccc" });
            table.AddRow(new List<string> { "111", "222", "333" });
            table.AddRow(new List<string> { "total", "2" });
            table.Rows.Last().Children[0].Attributes.Add(new HtmlAttribute("colspan", "2"));

            root.Children.Add(table.Root);

            doc.Body.Children.Add(root);

            doc.AddScript("randomScript");
            doc.AddScript("blahScript");
            doc.AddScript("deleteScript");
            doc.AddScript("yayScript");
            doc.RemoveScript("deleteScript");

            doc.AddStylesheet("randomStylesheet");
            doc.AddStylesheet("blahStylesheet");
            doc.AddStylesheet("deleteStylesheet");
            doc.AddStylesheet("yayStylesheet");
            doc.RemoveStylesheet("deleteStylesheet");

            string output = doc.ToString();
            output = HtmlDocument.CleanupHtml(output);
            output = HtmlDocument.PrettyPrint(output);
            Console.WriteLine(output);
            
            Console.In.Read();
        }
    }
}
