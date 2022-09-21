using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class DashboardSummery
    {
        public int TotalUser { get; set; }
        public int TotalSubscription { get; set; }
        public int ActiveSubscription { get; set; }
        public int ExpiredSubscription { get; set; }
        public int AboutToExpired { get; set; }
    }
}
