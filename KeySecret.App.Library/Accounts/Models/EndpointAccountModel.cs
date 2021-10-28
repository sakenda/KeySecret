using System;

namespace KeySecret.App.Library.Models
{
    public class EndpointAccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAdress { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}