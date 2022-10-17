using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheMuscleBar.AppCode.Enums;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class CollectFee
    {
        [Required(ErrorMessage = "UserId must be provided")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Amount cannot be empty or less than 0")]
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "From Date cannot be empty")]
        public string FromDate { get; set; }
        //[Required(ErrorMessage = "To Date cannot be empty")]
        public string ToDate { get; set; }
        public TransactionType TransactionType { get; set; }
        [Required(ErrorMessage = "Payment mode must be selected")]
        public PaymentMode PaymentMode { get; set; }
        public int EntryBy { get; set; }
        public string PhoneNumber { get; set; }
        public MembershipType MembershipType { get; set; }
    }

    public class CollectFeeResponse: Response
    {
        public int UserId { get; set; }
        public int LedgerId { get; set; }
    }
}
