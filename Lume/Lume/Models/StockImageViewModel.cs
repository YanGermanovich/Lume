using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class StockImageViewModel
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public bool isScanned { get; set; }
    }
}