using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<AccountModel, InsertAccountModel, UpdateAccountModel> _accountsRepository;

        public AccountsController(IRepository<AccountModel, InsertAccountModel, UpdateAccountModel> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        [HttpGet("/api/accounts")]
        public async Task<IEnumerable<AccountModel>> GetAllAccountsAsync()
        {
            var list = await _accountsRepository.GetItemsAsync();
            return list;
        }

        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(int id)
        {
            var item = await _accountsRepository.GetItemAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync([FromBody] InsertAccountModel account)
        {
            int id = await _accountsRepository.InsertItemAsync(account);

            var createdItem = new AccountModel()
            {
                Id = id,
                Name = account.Name,
                WebAdress = account.WebAdress,
                Password = account.Password,
                CreatedDate = DateTime.Now
            };

            return CreatedAtAction(nameof(InsertAccountAsync), createdItem);
        }

        [HttpPut("/api/accounts/upd")]
        public IActionResult UpdateAccountAsync([FromBody] UpdateAccountModel account)
        {
            _accountsRepository.UpdateItemAsync(account);
            return NoContent();
        }

        [HttpDelete("/api/accounts/del/{id}")]
        public IActionResult DeleteAccountAsync(int id)
        {
            _accountsRepository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}