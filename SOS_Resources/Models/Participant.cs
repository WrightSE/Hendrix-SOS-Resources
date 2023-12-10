using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOS_Resources.Models
{
    public class Participant
    {
        public int ID { get; set; }

        
        [StringLength(100)]
        [Display(Name = "Preffered Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        // [Column("FirstName")]
        [Display(Name = "Legal First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                if (Name != ""){
                    return LastName + ", " + Name;
                } else {
                    return LastName + ", " + FirstName;
                }
            }
        }
        
        public ICollection<ResourceRequest> ResourceRequests { get; set; }
        public ICollection<TextbookRequest> TextbookRequests { get; set; }
        
    }
}