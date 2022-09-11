using System;
using TheMuscleBar.AppCode.PhonePay;

namespace TheMuscleBar.Models
{
    public class TransactionStatusResponse :Response
    {
        public int TransactionId { get; set; }
        public string RequestedId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string UTR { get; set; }
        public string PaymentApp { get; set; }
        public string PayerName { get; set; }
        public string PayerVPA { get; set; }
        public string PayerPhone { get; set; }
        public string Remark { get; set; }
    }

    public class StatusCheckJobRequest
    {
        public int TransactionId { get; set; }  
        public string RequestedId { get; set; }  
        public decimal Amount { get; set; }  
        public int EntryBy { get; set; }  
        public int UPISettingId { get; set; }  
        public string ServerHookURL { get; set; }  
        public string WebhookURL { get; set; }  
        public DateTime EntryOn { get; set; }  
        public DateTime ModifyOn { get; set; }  
        public string TxnObject { get; set; }  
        public char Status { get; set; }  
        public int StatusCheckCount { get; set; }  
        public bool IsServerHookProceed { get; set; }  
    }

    public class UpdateTransactionStatusRequest
    {
        public int TID { get; set; }
        public char Status { get; set; }
        public string TxnObject { get; set; }
        public string UTR { get; set; }
        public string Remark { get; set; }
        public string PaymentApp { get; set; }
        public string PayerName { get; set; }
        public string PayerVPA { get; set; }
        public string PayerPhone { get; set; }
        public bool isServerhook { get; set; }

    }

    public class StatusCheckRequestNew
    {
        public int TransactionId { get; set; }
        public string RequestedId { get; set; }
        public string CallingFrom { get; set; }
        public string Refreshtoken { get; set; }
        public string Authtoken { get; set; }
        public string Devicefingerprint { get; set; }
        public string UUID { get; set; }
        public string MerchantId { get; set; }
        public decimal Amount { get; set; }
        public string EntryOn { get; set; }//Payment initiationTime
        public string ServerHookURL { get; set; }
        public string WebhookURL { get; set; }
        public char Status { get; set; }
        public bool IsServerHookProceed { get; set; }
        public string PayerVPA { get; set; }
        public string PayerPhone { get; set; }
        public string PayerName { get; set; }
        public string PaymentApp { get; set; }
        public string Remark { get; set; }
        public string UTR { get; set; }
        public string QrId { get; set; }
    }
}
