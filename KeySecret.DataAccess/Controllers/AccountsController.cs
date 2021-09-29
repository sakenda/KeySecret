using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<AccountModel> _accountsRepository;

        public AccountsController(IRepository<AccountModel> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        [HttpGet("/api/accounts")]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAllAccountsAsync()
        {
            IEnumerable<AccountModel> list = await _accountsRepository.GetItemsAsync();
            return list == null ? NotFound() : Ok(list);
        }

        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(int id)
        {
            AccountModel item = await _accountsRepository.GetItemAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync(AccountModel account)
        {
            AccountModel model = await _accountsRepository.InsertItemAsync(account);
            return CreatedAtAction(nameof(InsertAccountAsync), model);
        }

        [HttpPut("/api/accounts/upd")]
        public IActionResult UpdateAccountAsync(AccountModel account)
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