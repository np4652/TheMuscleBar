
using System;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class AlertHubRequest
    {
        public string Requestid { get; set; }  //Random UniqID
        public string ReceiverNumber { get; set; }  //Mobile No Of User
        public string Content { get; set; }   //Message Text
        public string Messagetype { get; set; } //Message Type
        public string ApiURL { get; set; }
        public string ScanNo { get; set; }
        public string QuoteId { get; set; }
        public string QuoteMsg { get; set; }
        public string ReplyJID { get; set; }
    }

    public class SendSessionMessageResponse
    {
        public object result { get; set; }
        // public string result { get; set; }
        public int statuscode { get; set; }

        // public string message { get; set; }
        public message message { get; set; }
        //  public string message { get; set; }
        public string ticketStatus { get; set; }
        public string Data { get; set; }
        public string conversationId { get; set; }
        public string msg { get; set; }
        public string info { get; set; }
        //  public Message message { get; set; }
    }


    public class message
    {
        public string whatsappMessageId { get; set; }
        public string localMessageId { get; set; }
        public string text { get; set; }
        public Media media { get; set; }
        public object messageContact { get; set; }
        public object location { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public int status { get; set; }
        public object statusString { get; set; }
        public bool isOwner { get; set; }
        public bool isUnread { get; set; }
        public string ticketId { get; set; }
        public object avatarUrl { get; set; }
        public object assignedId { get; set; }
        public object operatorName { get; set; }
        public object replyContextId { get; set; }
        public int sourceType { get; set; }
        public object failedDetail { get; set; }
        public object messageReferral { get; set; }
        public string id { get; set; }
        public DateTime created { get; set; }
        public string conversationId { get; set; }

    }

    public class Media
    {
        public string id { get; set; }
        public string mimeType { get; set; }
        public string caption { get; set; }
    }

    public class WhatsappAPIResponse
    {

        public string status { get; set; }
        public string status_code { get; set; }
        public string description { get; set; }
        public string conversationId { get; set; }
        public string datetime { get; set; }
    }
}
