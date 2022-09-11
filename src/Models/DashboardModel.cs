using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;

namespace TheMuscleBar.Models
{
    public class APIUserCounts
    {
        public int Hits { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public int Pending { get; set; }
        public int HitsAmount { get; set; }
        public int SuccessAmnt { get; set; }
        public int FailedAmnt { get; set; }
        public int PendingAmnt { get; set; }


    }

    public class AdminUserCounts
    {
        public int Hits { get; set; }
        public int Users { get; set; }
        public int ActiveSubscription { get; set; }
        public int ExpiredSubscription { get; set; }
        public int HitsAmount { get; set; }
        public int UsersAmount { get; set; }
        public int ActiveSubscriptionAmnt { get; set; }
        public int ExpiredSubscriptionAmnt { get; set; }
    }

    public class APIUserDashboardChart
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
        public List<ChartData> CData { get; set; }
    }
    public class ChartData
    {
        public DateTime EntryOnDate { get; set; }
        public long EntryOn { get; set; }
        public long SuccessTransaction { get; set; }
    }
    public class DonutData
    {
        public int Total { get; set; }
        public int Remaining { get; set; }
        public int Consumed { get; set; }
        public string Expiry { get; set; }
    }
    public class PieChart
    {
        public decimal Pending { get; set; }
        public decimal Failed { get; set; }
        public decimal Success { get; set; }
    }
}
