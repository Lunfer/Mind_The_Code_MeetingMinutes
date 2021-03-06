using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingMinutes.Models
{
    public class MeetingViewModel
    {
        public int MeetingID { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } //The date when the record was created

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Updated")]
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Meeting Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime MeetingDate { get; set; } = DateTime.Now;

        public Status Status { get; set; }
        [Required]
        public string Title { get; set; } //The title of the meeting  

        [Display(Name = "External Participants")]
        [Required]
        public string ExternalParticipants { get; set; } // A comma-separated string, keeping the mails of external(not registered in the system) participants.

        [Display(Name = "Internal Participants")]
        public List<CheckBoxViewModel> MeetingParticipants { get; set; }
    }
}
