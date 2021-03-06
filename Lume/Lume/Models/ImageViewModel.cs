﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class ImageViewModel
    {
        public long Id { get; set; }
        public long? Id_Properies { get; set; }
        public long? Id_Author { get; set; }
        public string Author_Name { get; set; }
        public long ScanningCount { get; set; }
        public bool isMy { get; set; }
        public long? Id_ImageCategory { get; set; }
        public long? Id_Event { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string description { get; set; }
        public string Src { get; set; }
        public double? N { get; set; }
        public double? E { get; set; }
        public EventViewModel Event { get; set; }
        public bool IsConfirmed { get; set; }
    }
}