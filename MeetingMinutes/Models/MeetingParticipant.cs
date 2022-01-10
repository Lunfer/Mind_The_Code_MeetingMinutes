using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;

namespace MeetingMinutes.Models
{
    public class MeetingParticipant
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingParticipantsID { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("FK_Meeting")]
        public int MeetingId { get; set; }
    }
}
