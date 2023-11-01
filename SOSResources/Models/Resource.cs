using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSResources.Models
{
    public class Resource
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        [StringLength(30)]
        public string Type { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int Quantity { get; set; }
        public ICollection<ResourceRequest>? ResourceRequests { get; set; }
    }
}