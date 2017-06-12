using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class StockViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long id_author { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatar { get; set; }
        public DateTime BeginData { get; set; }
        public DateTime? EndData { get; set; }
        public long id_stockType { get; set; }
        public string stockType { get; set; }
        public List<StockImageViewModel> Image { get; set; }
        public bool isMine { get; set; }
        public List<string> prizes { get; set; }
        public List<string> prizes_data { get; set; }
        public List<long> id_prize { get; set; }
        public bool isCompleted {get;set;}
        public List<StockUserViewModel> Participants { get; set; }
        public bool isTakeParticapent { get; set; }
        public List<PrizeViewModel> prizesToUpload { get; set; }
    }
}