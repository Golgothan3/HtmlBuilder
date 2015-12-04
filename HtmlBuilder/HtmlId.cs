using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace HtmlBuilder
{
    public class HtmlId : HtmlAttribute
    {
        public HtmlId(string value) : base("Id", value)
        {
        }

        public override void SetName(string newName)
        {
            //Do nothing because Name doesn't need to be changed
        }
    }

}
