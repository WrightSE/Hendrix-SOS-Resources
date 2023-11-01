using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSResources.Models
{
    public class ResourceRequest
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Submitted")]
        public DateTime RequestDate { get; set; }
        [Required]
        public int ParticipantID { get; set; }
        [Required]
        public Participant Participant {get; set;}
        [Required]
        public int ResourceID { get; set; }
        [Required]
        public Resource Resource { get; set; }
    }
}