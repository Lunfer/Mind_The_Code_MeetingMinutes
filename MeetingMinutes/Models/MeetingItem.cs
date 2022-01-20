using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; } =  DateTime.Now;
        [Display(Name ="Assigned To")]
        public string AssignedTo { get; set; }
        [Display(Name ="Requested By")]
        public string RequestedBy { get; set; }
        [Display(Name = "Change Requested")]
        public bool ChangeRequested { get; set; }

        [Required]
        [Display(Name = "Visible in minutes")]
        public bool VisibleInMinutes { get; set; } = true;

        [NotMapped]
        public List<IFormFile> Image { get; set; }

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
