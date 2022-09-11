using Microsoft.AspNetCore.Http;

namespace ShyamtelecomCMS.Models
{
    public class JournalEntry
    {
        public decimal Amount { get; set; } 
        public IFormFile File { get; set; } 
    }
}
