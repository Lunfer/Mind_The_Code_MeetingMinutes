using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MeetingMinutes.Models
{
    public class ApplicationUser : IdentityUser
    { 

        [PersonalData]
        public string FullName { get; set; }

        public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; }
    }
}
