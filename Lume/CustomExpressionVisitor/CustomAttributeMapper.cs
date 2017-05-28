using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExpressionVisitor
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomMapperAttribute : Attribute
    {
        public string MapTo { get; private set; }
        public CustomMapperAttribute (string mapTo)
        {
            MapTo = mapTo;  
        }

    }
}
