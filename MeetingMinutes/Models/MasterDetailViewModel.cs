using System.Collections.Generic;

namespace MeetingMinutes.Models
{
    public class MasterDetailViewModel
    {
        public List<Meeting> Meetings { get; set; }
        public Meeting SelectedMeeting { get; set; }
        public MeetingItem SelectedMeetingItem { get; set; }
        public DataEntryTargets DataEntryTarget { get; set; }
        public DataDisplayModes DataDisplayMode { get; set; }

    }
}
