using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Library.Accounts.Models
{
    public class UpdateAccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAdress { get; set; }
        public string Password { get; set; }
    }
}