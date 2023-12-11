using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOS_Resources.Models
{
    public class TextbookRequest
    {
        public int ID { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Submitted")]
        public DateTime RequestDate { get; set; }

        public bool Active { get; set; }
        

        [Required]
        public Participant Requester {get; set;}

        [Required]
        [Display(Name = "Copy")]
        public Copy copy { get; set; }
    }
}