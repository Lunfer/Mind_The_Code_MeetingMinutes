using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingNotes.Models
{
    public class Meetings : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime MeetingDate { get; set; }

        public string Status { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ExternalParticipants { get; set; }
    }

    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
