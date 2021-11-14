using System;

namespace KeySecret.App.Library.Models
{
    public class AccountModelSelected
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string WebAddress { get; set; }
        public string Password { get; set; }

        public string CategoryId { get; set; }
        public bool isDeleted { get; set; }

    }
}
