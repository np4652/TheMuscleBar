using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class Legder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string PhoneNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string EntryOn { get; set; }
    }
}
