using KeySecret.DesktopApp.Library.DataAccess;
using System;
using System.Runtime.CompilerServices;

namespace KeySecret.DesktopApp.Library.Accounts.Models
{
    public class AccountModel
    {
        private int _id;
        private string _name;
        private string _webAdress;
        private string _password;
        private DateTime _createdDate;

        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                IsDirty = true;
            }
        }
        public string WebAdress
        {
            get { return _webAdress; }
            set
            {
                _webAdress = value;
                IsDirty = true;
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                IsDirty = true;
            }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public bool IsDirty { get; private set; }

        public AccountModel()
        {
            Id = -1;
        }

        public AccountModel(string name, string webAdress, string password, DateTime createdDate) : this()
        {
            Name = name;
            WebAdress = webAdress;
            Password = password;
            _createdDate = createdDate;
        }

        public void SetId(int id, [CallerMemberName] string propertyname = "")
        {
            if (propertyname != nameof(AccountEndpoint.GetById) &&
                propertyname != nameof(AccountEndpoint.GetAllAsync) &&
                propertyname != nameof(AccountEndpoint.InsertAsync))
            {
                throw new MethodAccessException($"Zugriff aus Methode {propertyname} ist nicht gestattet.");
            }

            _id = id;
        }
    }
}