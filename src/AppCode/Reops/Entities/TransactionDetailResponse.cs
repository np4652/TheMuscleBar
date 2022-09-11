namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class TransactionDetailResponse
    {
        public int TransactionId { get; set; }
        public string RequestedId { get; set; }
        public string QRID { get; set; }
        public string DisplayName { get; set; }
        public decimal Amount { get; set; }
        public string ServerHookURL { get; set; }
        public string WebhookURL { get; set; }
    }

    public class UPISettingWithTIDDetail : UPISetting
    {
        public int TransactionId { get; set; }
        public string EntryOn { get; set; }
        public string RequestedId { get; set; }
        public decimal Amount { get; set; }
        public char Status { get; set; }
        public string UTR { get; set; }
        public string PaymentApp { get; set; }
        public string PayerName { get; set; }
        public string PayerVPA { get; set; }
        public string PayerPhone { get; set; }
        public string ServerHookURL { get; set; }

    }
}
