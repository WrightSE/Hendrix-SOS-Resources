using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOSResources.Models
{
    public class Textbook
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Author Last Name")]
        public string AuthorLastName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "Author First Name")]
        public string AuthorFirstName { get; set; }

        [Display(Name = "Author")]
        public string Author
        {
            get
            {
                return AuthorLastName + ", " + AuthorFirstName;
            }
        }

        [StringLength(50)]
        public string Edition { get; set; }

        public int Quantity { get; set; }

        public bool CheckedOut { get; set;}

        public ICollection<TextbookRequest> TextbookRequests { get; set; }
    }
}