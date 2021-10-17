namespace KeySecret.DesktopApp.Library.Authentification
{
    public interface IAuthenticatedUserModel
    {
        string Token { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        void ResetAuthenticatedUser();
    }
}