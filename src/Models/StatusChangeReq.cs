using System.ComponentModel.DataAnnotations;

namespace TheMuscleBar.Models
{
    public class StatusChangeReq
    {
      
        public int ID { get; set; }
        public string Remark { get; set; }
        [Required]
        public Status Status { get; set; }

    }
}
