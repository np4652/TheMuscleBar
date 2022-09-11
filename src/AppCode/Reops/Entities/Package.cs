namespace TheMuscleBar.AppCode.Reops.Entities
{
    public class Package
    {
    
        public int PackageId { get; set; }    
        public string PackageName { get; set; }    
        public int Validity { get; set; }    
        public int HitCount { get; set; }
        public decimal Cost { get; set; }
        public int Ind { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrail { get; set; }


    }

    public class BuypackageViewModel : Package
    {
        public bool Activated { get; set;}
        public int ExpiredOn { get; set;}
    }
}
