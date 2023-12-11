using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NuGet.Protocol.Plugins;

namespace SOS_Resources.Models
{
    public class Resource
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int TypeID { get; set; }


        public ResourceType Type { get; set; }

      

        [StringLength(250)]
        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public bool Available { get; set;}

        public ICollection<ResourceRequest> ResourceRequests { get; set; }
    }
}