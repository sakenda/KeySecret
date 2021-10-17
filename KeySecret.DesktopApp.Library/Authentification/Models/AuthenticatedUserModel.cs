using System;

namespace KeySecret.DesktopApp.Library.Authentification.Models
{
    public class AuthenticatedUserModel : IAuthenticatedUserModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Expiration { get; set; }

        public void ResetAuthenticatedUser()
        {
            Token = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Expiration = DateTime.MinValue;
        }
    }
}