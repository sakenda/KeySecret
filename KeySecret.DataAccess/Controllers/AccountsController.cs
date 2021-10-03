using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<AccountModel> _accountsRepository;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IRepository<AccountModel> accountsRepository, ILogger<AccountsController> logger)
        {
            _accountsRepository = accountsRepository;
            _logger = logger;
        }

        [HttpGet("/api/accounts")]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAllAccountsAsync()
        {
            IEnumerable<AccountModel> list = await _accountsRepository.GetItemsAsync();

            _logger.LogInformation($"Es wurde eine Liste aller Accounts angefordert. {((List<AccountModel>)list).Count} Accounts wurden zurückgegeben.");

            return list == null ? NotFound() : Ok(list);
        }

        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(int id)
        {
            AccountModel item = await _accountsRepository.GetItemAsync(id);

            _logger.LogInformation($"Der Account mit der ID {id} wurd angefordert.");

            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync(AccountModel account)
        {
            AccountModel model = await _accountsRepository.InsertItemAsync(account);

            _logger.LogInformation($"Folgender Eintrag wurde in die Datenbank geschrieben:\r\n{model.Id}\r\n{model.Name}\r\n{model.WebAdress}\r\n{model.Password}\r\n{model.CreatedDate}");

            return CreatedAtAction(nameof(InsertAccountAsync), model);
        }

        [HttpPut("/api/accounts/upd")]
        public IActionResult UpdateAccountAsync(AccountModel account)
        {
            _accountsRepository.UpdateItemAsync(account);

            _logger.LogInformation($"Account wurde aktualisiert.");

            return NoContent();
        }

        [HttpDelete("/api/accounts/del/{id}")]
        public IActionResult DeleteAccountAsync(int id)
        {
            _accountsRepository.DeleteItemAsync(id);

            _logger.LogInformation($"Account mit der ID {id} wurde gelöscht.");

            return NoContent();
        }
    }
}