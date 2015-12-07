using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HtmlBuilder
{
    public class HtmlBuilderId : HtmlBuilderAttribute
    {
        public HtmlBuilderId(string value) : base("id", value)
        {
        }

        public override void SetName(string newName)
        {
            //Do nothing because Name doesn't need to be changed
        }
    }

}
