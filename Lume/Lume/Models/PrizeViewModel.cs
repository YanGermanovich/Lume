using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class PrizeViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long id_PrizeType { get; set; }
        public string PrizeType { get; set; }
        public string Data { get; set; }
    }
}