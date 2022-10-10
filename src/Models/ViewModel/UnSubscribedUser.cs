using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Models.ViewModel
{
    public class UnSubscribedUser
    {
        public int UserId { get; set; }
        public int Bioid { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
    public class UserDetailsReturn
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
    public class SyncData
    {
        public string data { get; set; }

    }
}
