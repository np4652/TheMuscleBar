using System;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class UPISetting
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string UUID { get; set; }
        public string DeviceFingerprint { get; set; }
        public string UserId { get; set; }
        public string AuthToken { get; set; }
        public string Refreshtoken { get; set; }
        public string UserGroupId { get; set; }
        public string MerchantId { get; set; }
        public string StoreId { get; set; }
        public string QrId { get; set; }
        public int EntryBy { get; set; }
        public string DisplayName { get; set; }
        public bool IsLoggedOut { get; set; }
    }
}
