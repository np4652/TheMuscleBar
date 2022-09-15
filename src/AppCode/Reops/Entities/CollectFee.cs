using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class CollectFee
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public int EntryBy { get; set; }
    }
}
