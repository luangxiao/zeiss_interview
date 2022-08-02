using System.ComponentModel.DataAnnotations;

namespace zeiss.Models
{
    public class Machine
    {
        [Key]
        public string Id { get; set; }

        public string Machine_Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
    }
}
