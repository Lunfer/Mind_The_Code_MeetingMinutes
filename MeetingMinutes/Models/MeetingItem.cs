using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace MeetingMinutes.Models
{
    public class MeetingItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingItemID { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }

        public string AssignedTo { get; set; }

        public string RequestedBy { get; set; }

        public bool ChangeRequested { get; set; }

        [Required]
        public bool VisibleInMinutes { get; set; } = true;

        public string FileAttachment { get; set; }

        public string  FileName { get; set; }

        public string FileType { get; set; }


        //Navigation Properties
        public int Meetingid { get; set; }
        public Meeting Meeting { get; set; }

        public int RiskLevelid { get; set; }
        public RiskLevel RiskLevel { get; set; }

    }
}
