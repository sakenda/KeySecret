using System;

namespace KeySecret.App.Library.Models
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public string Password { get; set; }
        public CategoryDto? Category { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}