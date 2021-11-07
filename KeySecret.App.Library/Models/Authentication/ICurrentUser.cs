namespace KeySecret.App.Library.Models
{
    public interface ICurrentUser
    {
        string Token { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        bool IsLoggedIn { get; }
    }
}