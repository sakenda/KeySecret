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

            if (list == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'list' ist NULL");
                return NotFound(nameof(InsertAccountAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation($"Es wurde eine Liste aller Accounts angefordert. {((List<AccountModel>)list).Count} Accounts wurden zurückgegeben.");
            return Ok(list);
        }

        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(int id)
        {
            AccountModel item = await _accountsRepository.GetItemAsync(id);

            if (item == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'list' ist NULL");
                return NotFound(nameof(InsertAccountAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation($"Der Account mit der ID {id} wurd angefordert.");
            return Ok(item);
        }

        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync([FromBody] AccountModel account)
        {
            AccountModel model = await _accountsRepository.InsertItemAsync(account);

            if (model == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'model' ist NULL");
                return BadRequest(nameof(InsertAccountAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation("Es wurde ein Eintrag in die Datenbank geschrieben.");
            return CreatedAtAction(nameof(InsertAccountAsync), model);
        }

        [HttpPut("/api/accounts/upd")]
        public ActionResult UpdateAccountAsync([FromBody] AccountModel account)
        {
            _accountsRepository.UpdateItemAsync(account);

            _logger.LogInformation($"Account wurde aktualisiert.");

            return NoContent();
        }

        [HttpDelete("/api/accounts/del/{id}")]
        public ActionResult DeleteAccountAsync(int id)
        {
            _accountsRepository.DeleteItemAsync(id);

            _logger.LogInformation($"Account mit der ID {id} wurde gelöscht.");

            return NoContent();
        }
    }
}