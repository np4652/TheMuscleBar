namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class ErrorModel
    {
        public int Id { get; set; }
        public string When { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string Trace { get; set; }
        public string Logger { get; set; }
    }
}
