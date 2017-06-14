using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class EventViewModel
    {
        public long Id { get; set; }
        public string Data { get; set; }
        public string TypeData { get; set; }
        public long TypeId { get; set; }
    }
}