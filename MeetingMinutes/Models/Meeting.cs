using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace MeetingMinutes.Models
{
    public enum Status
     { 
            New,
            Started,
            Finished
    }
    public class Meeting
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now; //The date when the record was created

        [Required]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }
        //[ForeignKey("CreatedBy")]

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateUpdated { get; set; }

        [Required]
        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }

        public Status Status { get; set; }

        [Required]
        public string Title { get; set; } //The title of the meeting  

        [Required]
        [Display(Name = "External Participants")]
        public string ExternalParticipants { get; set; } // A comma-separated string, keeping the mails of external(not registered in the system) participants.

        //Navigation Properties
        public List<MeetingItem> MeetingItems { get; set; }
        [Display(Name = "Meeting Participants")]
        public List<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
