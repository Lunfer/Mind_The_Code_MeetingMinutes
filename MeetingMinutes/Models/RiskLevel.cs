using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MeetingMinutes.Models
{
    public class RiskLevel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RiskLevelID { get; set; }

        public string Name { get; set; }

        //Navigation Properties
        public List<MeetingItem> MeetingItemss { get; set; }
    }
}
