using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HtmlBuilder
{
    public class HtmlBuilderContent : HtmlBuilderElement
    {


        public string Content;
        public HtmlBuilderContent(string content) : base("TextNode")
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
