using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeySecret.DataAccess.Library.Models
{
    public class CategoryModel
    {
        [Required, Key, ForeignKey("Account")]
        public Guid CategoryId { get; set; }

        [MaxLength(128, ErrorMessage = "There are only 128 characters allowed!")]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}