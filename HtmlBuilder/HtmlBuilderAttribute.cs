using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HtmlBuilder
{
    public class HtmlBuilderAttribute
    {
        private string _name;
        public string Name
        {
            get { return _name; }
        }
        public string Value { get; set; }

        public HtmlBuilderAttribute(string name, string value)
        {
            this._name = name;
            this.Value = value;
        }

        public virtual void SetName(string newName)
        {
            this._name = newName;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return "";
            }
            else {
                return string.Format("{0}='{1}' ", Name, Value);
            }

        }
    }
}
