using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace SOS_Resources.Models
{
    public class ResourceType
    {
        public int ID { get; set; }

        [StringLength(55, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Resource>? Resources { get; set;}
    }
}