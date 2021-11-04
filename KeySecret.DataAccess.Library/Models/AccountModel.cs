using System;
using System.ComponentModel.DataAnnotations;

namespace KeySecret.DataAccess.Library.Models
{
    public class AccountModel
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Name must be set.")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "There are only 256 characters allowed!")]
        public string WebAddress { get; set; }

        [Required(ErrorMessage = "Password must be set!")]
        [MaxLength(128, ErrorMessage = "There are only 128 characters allowed!")]
        public string Password { get; set; }

        public DateTime ModifiedDate { get; set; }

#nullable enable
        public Guid? CategoryId { get; set; }

        public virtual CategoryModel? Category { get; set; }
#nullable disable
    }
}