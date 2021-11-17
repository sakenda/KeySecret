using KeySecret.DataAccess.Library.Models;
using KeySecret.DataAccess.Library.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    /// <summary>
    /// <see cref="AccountModel"/> endpoints of the api.
    /// </summary>
    //[Authorize]
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

        /// <summary>
        /// Get all <see cref="AccountModel"/> from the database.
        /// </summary>
        /// <returns><seealso cref="IEnumerable{T}"/> if found, else <seealso cref="NotFoundResult"/>.</returns>
        [HttpGet("/api/accounts")]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAllAccountsAsync()
        {
            var response = new Response();
            IEnumerable<AccountModel> list = await _accountsRepository.GetItemsAsync();

            if (list == null)
            {
                response.Status = "Not Found";
                response.Message = "Return from the repository was NULL";
                _logger.LogError(response.Status + ": " + response.Message + $" ({nameof(GetAllAccountsAsync)})");

                return NotFound(response);
            }

            response.Status = "Ok";
            response.Message = $"Returned {((List<AccountModel>)list).Count} items";
            _logger.LogInformation(response.Status + ": " + response.Message + $" ({nameof(GetAllAccountsAsync)})");

            return Ok(list);
        }

        /// <summary>
        /// Get a <see cref="AccountModel"/> by id.
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <returns><see cref="AccountModel"/> if found, else <see cref="NotFoundResult"/>.</returns>
        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(Guid id)
        {
            var response = new Response();
            AccountModel item = await _accountsRepository.GetItemAsync(id);

            if (item == null)
            {
                response.Status = "Not Found";
                response.Message = "Return from the repository was NULL";

                _logger.LogError(response.Status + ": " + response.Message + $" ({nameof(GetByIdAsync)})");
                return NotFound(response);
            }

            response.Status = "Ok";
            response.Message = $"Account with ID {id} returned.";

            _logger.LogInformation(response.Status + ": " + response.Message + $" ({nameof(GetByIdAsync)})");
            return Ok(item);
        }

        /// <summary>
        /// Insert a new <see cref="AccountModel"/> into the database.
        /// </summary>
        /// <param name="account">The serialized json as <see cref="AccountModel"/>.</param>
        /// <returns><seealso cref="CreatedAtActionResult"/> on success, else <seealso cref="BadRequestResult"/>.</returns>
        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync([FromBody] AccountModel account)
        {
            var response = new Response();
            AccountModel model = await _accountsRepository.InsertItemAsync(account);

            if (model == null)
            {
                response.Status = "Bad request";
                response.Message = "Return from the repository was NULL";

                _logger.LogError(response.Status + ": " + response.Message + $" ({nameof(InsertAccountAsync)})");
                return BadRequest(response);
            }

            response.Status = "Ok";
            response.Message = "Successfully created an item";

            _logger.LogInformation(response.Status + ": " + response.Message + $" ({nameof(InsertAccountAsync)})");
            return CreatedAtAction(nameof(InsertAccountAsync), response);
        }

        /// <summary>
        /// Updates a <see cref="AccountModel"/>.
        /// </summary>
        /// <param name="account">The serialized json as <see cref="AccountModel"/>.</param>
        /// <returns><seealso cref="NoContentResult"/></returns>
        [HttpPut("/api/accounts/upd")]
        public ActionResult UpdateAccountAsync([FromBody] AccountModel account)
        {
            var response = new Response();

            _accountsRepository.UpdateItemAsync(account);

            response.Status = "No content";
            response.Message = $"Account {account.AccountId} updated";
            _logger.LogInformation(response.Status + ": " + response.Message + $" ({nameof(UpdateAccountAsync)})");

            return NoContent();
        }

        /// <summary>
        /// Deletes a <see cref="AccountModel"/> from the database
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <returns><seealso cref="NoContentResult"/></returns>
        [HttpDelete("/api/accounts/del/{id}")]
        public ActionResult DeleteAccountAsync(Guid id)
        {
            var response = new Response();

            _accountsRepository.DeleteItemAsync(id);

            response.Status = "No content";
            response.Message = $"Account {id} deleted";
            _logger.LogInformation(response.Status + ": " + response.Message + $" ({nameof(DeleteAccountAsync)})");

            return NoContent();
        }
    }
}