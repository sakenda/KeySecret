using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KeySecret.DataAccess.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<AccountModel> _accountsRepository;

        public AccountsController(IRepository<AccountModel> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        // GET: /api/accounts/
        [HttpGet]
        public async Task<List<AccountModel>> Get()
        {
            return await _accountsRepository.GetItemsAsync();
        }

        // GET: /api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<AccountModel> GetById(int id)
        {
            return await _accountsRepository.GetItemAsync(id);
        }

        [HttpPost("/ins/")]
        public async void Post(AccountModel account)
        {
            await _accountsRepository.InsertItemAsync(account);
        }

        [HttpPut("/upd/{id}")]
        public async void Put(int id, AccountModel account)
        {
            await _accountsRepository.UpdateItemAsync(account);
        }

        [HttpDelete("/del/{id}")]
        public async void Delete(int id)
        {
            await _accountsRepository.DeleteItemAsync(id);
        }
    }
}