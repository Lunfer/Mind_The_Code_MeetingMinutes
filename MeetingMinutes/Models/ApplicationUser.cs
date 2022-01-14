using Microsoft.AspNetCore.Identity;

namespace MeetingMinutes.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }
    }
}
