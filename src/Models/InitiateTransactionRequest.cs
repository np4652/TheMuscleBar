namespace TheMuscleBar.Models
{
    public class InitiateTransactionRequest
    {
        public string RequestedId { get; set; }
        public decimal Amount { get; set; }
        public string UPIId { get; set; }
        public string ServerHookURL { get; set; }
        public string WebHookURL { get; set; }
      

    }

    public class InitiateTransactionResponse : Response
    {
        public int TransactionId { get; set; }
        public string URL { get; set; }
        public string IntentString { get; set; }
    }
}
