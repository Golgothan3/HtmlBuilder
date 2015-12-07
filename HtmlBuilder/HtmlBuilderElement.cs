using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
    }

}
