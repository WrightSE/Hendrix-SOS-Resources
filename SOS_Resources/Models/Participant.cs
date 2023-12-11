using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SOS_Resources.Areas.Identity.Data;

namespace SOS_Resources.Models
{
    public class Participant
    {
        public int ID { get; set; }

        public string SOS_UserId { get; set;}
        [Required]
        public SOS_User SOS_User { get; set;}
        

        public ICollection<ResourceRequest>? ResourceRequests { get; set; }
        public ICollection<TextbookRequest>? TextbookRequests { get; set; }
        
    }
}