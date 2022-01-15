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
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Meeting Date")]
        [DataType(DataType.DateTime)]
        public DateTime MeetingDate { get; set; } = DateTime.Now;

        public Status Status { get; set; }

        public string Title { get; set; } //The title of the meeting  

        [Display(Name = "External Participants")]
        public string ExternalParticipants { get; set; } // A comma-separated string, keeping the mails of external(not registered in the system) participants.

        [Display(Name = "Internal Participants")]
        public List<CheckBoxViewModel> MeetingParticipants { get; set; }
    }
}
