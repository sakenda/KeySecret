using System;

namespace KeySecret.DataAccess.Library.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAdress { get; set; }
        public string Password { get; set; }
        public int? CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}