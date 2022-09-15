using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Models.ViewModel
{
    public class CollectFeeViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public MembershipType MembershipType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}
