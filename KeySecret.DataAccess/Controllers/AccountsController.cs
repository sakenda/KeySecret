using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<AccountModel>>> Get()
        {
            return await _accountsRepository.GetItemsAsync();
        }

        // GET: /api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> GetById(int id)
        {
            return await _accountsRepository.GetItemAsync(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountModel account)
        {
            _accountsRepository.InsertItemAsync(account);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Put([FromBody] AccountModel account)
        {
            _accountsRepository.UpdateItemAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _accountsRepository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}