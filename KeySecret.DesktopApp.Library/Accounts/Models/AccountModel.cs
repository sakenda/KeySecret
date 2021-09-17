using System;

namespace KeySecret.DesktopApp.Library.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAdress { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}