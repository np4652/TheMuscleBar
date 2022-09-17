using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class SubscripitionReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int LedgerId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
