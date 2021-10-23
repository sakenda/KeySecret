using System;

namespace KeySecret.DesktopApp.Library.Models
{
    public class CurrentUser : ICurrentUser
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Expiration { get; set; }

        public void ResetCurrentUser()
        {
            Token = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Expiration = DateTime.MinValue;
        }
    }
}