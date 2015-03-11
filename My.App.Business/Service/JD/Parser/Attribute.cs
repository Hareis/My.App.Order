using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace My.App.Business.Service.JD.Parser
{
    internal class Attribute
    {
        public string ItemName { get; set; }
        public Type ItemType { get; set; }
        public string ListName { get; set; }
        public Type ListType { get; set; }
        public MethodInfo Method { get; set; }
    }
}
