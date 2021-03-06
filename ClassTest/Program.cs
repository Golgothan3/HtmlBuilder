﻿using System;
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
            HtmlBuilderDocument doc = new HtmlBuilderDocument("HtmlBuilder Rocks") {DocType = "<!DOCTYPE html>"};

            doc.AddStylesheet(@"css/main.css");
            doc.AddStylesheet(@"css/backgrounds.css");

            doc.AddScript(@"js/jquery.js");

            HtmlBuilderElement div = new HtmlBuilderElement("div", "wrapper");
            HtmlBuilderElement p = new HtmlBuilderElement("p");
            string baconFiller = @"Bacon ipsum dolor amet spare ribs beef ribs porchetta meatloaf ham shoulder ham hock bresaola ball tip rump kielbasa swine alcatra kevin. Turducken andouille jowl, corned beef short ribs beef beef ribs flank fatback pork belly shank frankfurter cupim shoulder. Sirloin meatloaf porchetta t-bone. Sirloin kevin venison meatball tenderloin flank turducken pig tongue t-bone cow corned beef alcatra. Kielbasa landjaeger ball tip prosciutto salami pork chop tail rump fatback.";

            div.Children.Add(p.SetContent(baconFiller));
            div.Children.Add(
                HtmlBuilderElement.Parse(
                    "<div class='thisIsAClass' id='thisIsAnId' data-duh='123'><ul><li><strong>Stuff</strong></li><li>and <strong><em>things</em></strong></li></ul></div>"));

            doc.Body.Children.Add(div);

            string output = doc.ToString();
            output = HtmlBuilderDocument.CleanupHtml(output);
            output = HtmlBuilderDocument.PrettyPrint(output);

            Console.WriteLine(output);
            
            //This line just makes the console window stay open, it doesn't actually do anything
            Console.In.Read();
        }
    }
}
