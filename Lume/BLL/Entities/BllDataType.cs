﻿using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllDataType
    {
        [CustomAttributeMapper("Id")]
        public long Id { get; set; }

        [CustomAttributeMapper("Name")]
        public string Name { get; set; }
    }
}
