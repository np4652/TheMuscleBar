using System.Collections.Generic;

namespace TheMuscleBar.Models
{
    public class NotificationPermissions
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public List<string> Allowed { get; set; }
    }
}
