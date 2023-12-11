using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOS_Resources.Models
{
    public class Copy
    {
        public int ID { get; set; }
        [Required]
        public Textbook textbook { get; set; }
        public ICollection<TextbookRequest>? textbookRequests { get; set; }
        public bool CheckedOut { get; set; }
    }
}