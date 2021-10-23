namespace KeySecret.DesktopApp.Library.Models
{
    public interface ICurrentUser
    {
        string Token { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        void ResetCurrentUser();
    }
}