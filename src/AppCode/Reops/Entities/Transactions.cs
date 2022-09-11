namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class Transactions
    {
        public int TransactionId { get; set; }
        public string RequestedId { get; set; }
        public decimal Amount { get; set; }
        public string EntryBy { get; set; }
        public int EntryById { get; set; }
        public int UPISettingId { get; set; }
        public string EntryOn { get; set; }
        public string ModifyOn { get; set; }
        public string TxnObject { get; set; }
        public string Status { get; set; }
        public string ServerHookURL { get; set; }
        public string WebHookURL { get; set; }
        public int StatusCheckCount { get; set; }
        public bool IsServerHookProceed { get; set; }
    }

    public class TransactionRequest
    {
        public int TransactionId { get; set; }
        public string RequestedId { get; set; }
        public decimal Amount { get; set; }
        public int EntryBy { get; set; }
        public string UPIId { get; set; }
        public string TxnObject { get; set; }
        public string Status { get; set; }
        public string ServerHookURL { get; set; }
        public string WebHookURL { get; set; }
    }

    public class TransactionReport : Transactions
    {
        public string Name { get; set; }
        public string qrId { get; set; }
        public string UTR { get; set; }
        public string Remark { get; set; }
    }

    public class TransactionReportRequest
    {
        public int UserId { get; set; }
        public int Top { get; set; }
        public string RequestedId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string  Status { get; set; }
    }
}
