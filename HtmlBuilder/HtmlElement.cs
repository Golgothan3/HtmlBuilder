using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HtmlBuilder
{
    public class HtmlElement
    {
        public string TagName { get; set; }
        public HtmlId Id { get; set; } = new HtmlId("");
        public HtmlClass CssClass { get; set; } = new HtmlClass("");
        public List<HtmlAttribute> Attributes { get; set; } = new List<HtmlAttribute>();
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public HtmlElement(string tagName)
        {
            this.TagName = tagName;
        }

        public HtmlElement(string tagName, string idName)
        {
            this.TagName = tagName;
            this.Id = new HtmlId(idName);
        }
        public HtmlElement(string tagName, string idName, string className)
        {
            this.TagName = tagName;
            this.Id = new HtmlId(idName);
            this.CssClass = new HtmlClass(className);
        }

        public HtmlElement SetContent(string content)
        {
            this.Children.Add(new HtmlContent(content));
            return this;
        }

        public override string ToString()
        {
            return string.Format("<{0} {1} {2} {3}>{4}</{0}>", TagName, Id.ToString(), CssClass.ToString(), GetAttributesString(), GetChildrenString());
        }

        public object GetChildrenString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HtmlElement child in Children)
            {
                sb.Append(child.ToString());
            }
            return sb.ToString();
        }

        public object GetAttributesString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HtmlAttribute attr in Attributes)
            {
                sb.Append(attr.ToString());
            }
            return sb.ToString();

        }

        public List<HtmlElement> GetChildrenByTagName(string tagName)
        {
            return (from child in Children where child.TagName == tagName select child).ToList();
        }

        public void RemoveAttribute(string attributeName)
        {
            Attributes.Remove((from HtmlAttribute attr in Attributes where attr.Name == attributeName select attr).First());
        }
    }

}
