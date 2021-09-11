using KeySecret.DataAccess.Library.DataAccess;
using KeySecret.DataAccess.Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KeySecret.DataAccess.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsData _accountsDataAccess;

        public AccountsController(IAccountsData credentialDataAccess)
        {
            _accountsDataAccess = credentialDataAccess;
        }

        // GET: /api/accounts/
        [HttpGet]
        public List<AccountModel> Get()
        {
            return _accountsDataAccess.GetAccessDataModels();
        }

        // GET: /api/accounts/{id}
        [HttpGet("{id}")]
        public AccountModel GetById(int id)
        {
            return _accountsDataAccess.GetAccessDataModels().FirstOrDefault(x => x.Id == id);
        }

        // GET: /api/accounts/name/{name}
        [HttpGet("name/{name}")]
        public AccountModel GetByName(string name)
        {
            return _accountsDataAccess.GetAccessDataModels().FirstOrDefault(x => x.Name == name);
        }

        [HttpPost]
        public void Post(AccountModel account)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, AccountModel account)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}