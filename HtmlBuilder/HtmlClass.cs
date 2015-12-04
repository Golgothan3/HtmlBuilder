using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public class HtmlClass : HtmlAttribute
    {

        public HtmlClass(string className) : base("Class", className)
        {
        }

        public void AddClass(string className)
        {
            if (string.IsNullOrEmpty(Value.Trim()))
            {
                Value = className;
            }
            else {
                Value = string.Concat(Value, " ", className);
            }
        }

        public void RemoveClass(string className)
        {
            className = Regex.Escape(className);
            Regex reg = new Regex(string.Format("((?<start>^)|\\s){0}(?(start)($|\\s)|(?=$|\\s))", className), RegexOptions.IgnoreCase);
            Value = reg.Replace(Value, "");
        }

        public override void SetName(string newName)
        {
            //Do nothing because Name doesn't need to be changed
        }

    }
}
