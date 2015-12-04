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
            HtmlDocument doc = new HtmlDocument("HtmlBuilder Rocks");

            doc.DocType = "<!DOCTYPE html>";

            doc.AddStylesheet(@"css/main.css");
            doc.AddStylesheet(@"css/backgrounds.css");

            doc.AddScript(@"js/jquery.js");

            HtmlElement div = new HtmlElement("div", "wrapper");
            HtmlElement p = new HtmlElement("p");
            string baconFiller = @"Bacon ipsum dolor amet spare ribs beef ribs porchetta meatloaf ham shoulder ham hock bresaola ball tip rump kielbasa swine alcatra kevin. Turducken andouille jowl, corned beef short ribs beef beef ribs flank fatback pork belly shank frankfurter cupim shoulder. Sirloin meatloaf porchetta t-bone. Sirloin kevin venison meatball tenderloin flank turducken pig tongue t-bone cow corned beef alcatra. Kielbasa landjaeger ball tip prosciutto salami pork chop tail rump fatback.";
            p.Children.Add(new HtmlContent(baconFiller));

            doc.Body.Children.Add(div);
            doc.Body.Children.Add(p);

            string output = doc.ToString();
            output = HtmlDocument.CleanupHtml(output);
            output = HtmlDocument.PrettyPrint(output);

            Console.WriteLine(output);
            
            //This line just makes the console window stay open, it doesn't actually do anything
            Console.In.Read();
        }
    }
}
