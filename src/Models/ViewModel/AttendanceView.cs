using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMuscleBar.Models.ViewModel
{
    public class AttendanceView
    {
        public string UserId { get; set; }
        public string VDateTime { get; set; }
        public string Name { get; set; }
    }
    public class AttendanceInsert
    {
        public string UserId { get; set; }
        public string VDateTime { get; set; }
        
    }

}
