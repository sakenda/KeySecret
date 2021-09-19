namespace KeySecret.DataAccess.Library.Accounts.Models
{
    public class InsertAccountModel
    {
        public string Name { get; set; }
        public string WebAdress { get; set; }
        public string Password { get; set; }

        public InsertAccountModel(string name, string webAdress, string password)
        {
            Name = name;
            WebAdress = webAdress;
            Password = password;
        }
    }
}