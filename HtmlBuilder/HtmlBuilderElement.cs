using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public class HtmlBuilderElement
    {
        public string TagName { get; set; }
        public HtmlBuilderId Id { get; set; } = new HtmlBuilderId("");
        public HtmlBuilderClass CssClass { get; set; } = new HtmlBuilderClass("");
        public List<HtmlBuilderAttribute> Attributes { get; set; } = new List<HtmlBuilderAttribute>();
        public List<HtmlBuilderElement> Children { get; set; } = new List<HtmlBuilderElement>();

        public HtmlBuilderElement(string tagName)
        {
            this.TagName = tagName;
        }

        public HtmlBuilderElement(string tagName, string idName)
        {
            this.TagName = tagName;
            this.Id = new HtmlBuilderId(idName);
        }
        public HtmlBuilderElement(string tagName, string idName, string className)
        {
            this.TagName = tagName;
            this.Id = new HtmlBuilderId(idName);
            this.CssClass = new HtmlBuilderClass(className);
        }

        public HtmlBuilderElement SetContent(string content)
        {
            this.Children.Add(new HtmlBuilderContent(content));
            return this;
        }

        public override string ToString()
        {
            return string.Format("<{0} {1} {2} {3}>{4}</{0}>", TagName, Id.ToString(), CssClass.ToString(), GetAttributesString(), GetChildrenString());
        }

        public object GetChildrenString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HtmlBuilderElement child in Children)
            {
                sb.Append(child.ToString());
            }
            return sb.ToString();
        }

        public object GetAttributesString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HtmlBuilderAttribute attr in Attributes)
            {
                sb.Append(attr.ToString());
            }
            return sb.ToString();

        }

        public List<HtmlBuilderElement> GetChildrenByTagName(string tagName)
        {
            return (from child in Children where child.TagName == tagName select child).ToList();
        }

        public void RemoveAttribute(string attributeName)
        {
            Attributes.Remove((from HtmlBuilderAttribute attr in Attributes where attr.Name == attributeName select attr).First());
        }

        public static HtmlBuilderElement Parse(string str)
        {
            str = str.Replace("\n", "").Replace("\t", "");
            HtmlBuilderElement returnElement = new HtmlBuilderElement("");

            Regex tagRegex = new Regex(@"(?<opening>\<(?<tag>[\w\'\=\s\-]+)\>)|(?<closing>\<\/\w+\>)|(?<content>[^\<\>\/\\]+)");
            Regex attributeRegex = new Regex(@"(?<name>[\w\-]+)\=\'(?<value>[\w\-]+)\'");
            Stack<HtmlBuilderElement> stack = new Stack<HtmlBuilderElement>();

            foreach (Match m in tagRegex.Matches(str))
            {
                if (m.Groups["opening"].Captures.Count > 0)
                {
                    string tagText = m.Groups[2].Captures[0].Value.ToString();

                    string[] tagPieces = tagText.Split(' ');

                    HtmlBuilderElement newElement =
    new HtmlBuilderElement(tagPieces[0]);

                    if (tagPieces.Count() > 1)
                    {
                        foreach (string attribute in tagPieces)
                        {
                            if (attributeRegex.IsMatch(attribute))
                            {
                                string name = attributeRegex.Match(attribute).Groups["name"].Captures[0].Value.ToString();
                                string value = attributeRegex.Match(attribute).Groups["value"].Captures[0].Value.ToString();

                                if (name == "class")
                                {
                                    newElement.CssClass.Value = value;
                                }
                                else if (name == "id")
                                {
                                    newElement.Id.Value = value;
                                }
                                else
                                {
                                    newElement.Attributes.Add(new HtmlBuilderAttribute(name, value));
                                }
                            }
                        }
                    }

                    if (stack.Count > 0)
                    {
                        stack.Peek().Children.Add(newElement);
                    }

                    stack.Push(newElement);

                }
                else if (m.Groups["content"].Captures.Count > 0)
                {
                    stack.Peek().SetContent(m.Groups["content"].Captures[0].Value.ToString());
                }
                else
                {
                    if (stack.Count == 1)
                    {
                        return stack.Pop();
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
            }

            return null;
        }
    }

}
