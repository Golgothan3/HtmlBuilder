using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{

    public class HtmlBuilderDocument
    {
        public string DocType { get; set; } = "<!DOCTYPE html>";
        public HtmlBuilderElement Root { get; } = new HtmlBuilderElement("html");
        public HtmlBuilderElement Head { get; } = new HtmlBuilderElement("head");
        public HtmlBuilderElement Title { get; } = new HtmlBuilderElement("title");
        public List<string> Stylesheets { get; } = new List<string>();
        public HtmlBuilderElement Body { get; } = new HtmlBuilderElement("body");
        public List<string> Scripts { get; } = new List<string>();

        public HtmlBuilderDocument(string title)
        {
            Root.Children.Add(Head);
            Root.Children.Add(Body);
            Head.Children.Add(this.Title);
            this.Title.Children.Add(new HtmlBuilderContent(title));
        }

        public void AddScript(string src)
        {
            Scripts.Add(src);
        }

        public void RemoveScript(string src)
        {
            Scripts.Remove(src);
        }

        public void AddStylesheet(string href)
        {
            Stylesheets.Add(href);
        }

        public void RemoveStylesheet(string href)
        {
            Stylesheets.Remove(href);
        }

        public override string ToString()
        {
            //Add all the scripts to the end of the body element. It creates a script tag for each string in the Scripts list
            foreach (string src in Scripts)
            {
                HtmlBuilderElement script = new HtmlBuilderElement("script");
                script.Attributes.Add(new HtmlBuilderAttribute("src", src));
                Body.Children.Add(script);
            }
            //Add all stylesheet links to the end of the head element. It creates a link tag for each string in the Stylesheets list
            foreach (string href in Stylesheets)
            {
                HtmlBuilderElement stylesheet = new HtmlBuilderElement("link");
                stylesheet.Attributes.Add(new HtmlBuilderAttribute("rel", "stylesheet"));
                stylesheet.Attributes.Add(new HtmlBuilderAttribute("href", href));
                Head.Children.Add(stylesheet);
            }

            return string.Concat(DocType, Root.ToString());
        }

        public static string PrettyPrint(string html)
        {
            int level = 0;
            Regex tags = new Regex(@"(?<opening>\<\w)|(?<closing>\<?\/\w*\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match m = tags.Match(html);
            while (m.Success)
            {
                if (m.Groups["opening"].Captures.Count > 0)
                {
                    html = html.Insert(m.Index, string.Concat("\n", InsertTabs(level++)));
                }
                else
                {
                    html = html.Insert(m.Index, string.Concat("\n", InsertTabs(--level)));
                }

                //This prevents ArgumentOutOfRange exceptions when starting index value is greater than length of the html string
                if ((m.Index + m.Length + level) > html.Length)
                {
                    m = tags.Match(html, html.Length);
                }
                else
                {
                    m = tags.Match(html, m.Index + m.Length + level);
                }

            }

            return html;
        }

        public static string InsertTabs(int num)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append("\t");
            }
            return sb.ToString();
        }

        public static string CleanupHtml(string html)
        {
            Regex junkSpace = new Regex(@"\s{2,}");
            return junkSpace.Replace(html, " ").Replace(" >", ">");
        }
    }
}
