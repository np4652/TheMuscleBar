namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class APIModel
    {
        public int Id { get; set; }
        public string TID { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string EntryOn { get; set; }
        public string Method { get; set; }
        public bool IsIncomingOutgoing { get; set; }
    }
    public class APIModelRequest
    {
        public string Col { get; set; }
        public bool IncomingOnly { get; set; }
    }
}
