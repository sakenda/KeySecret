using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAllAccountsAsync()
        {
            IEnumerable<AccountModel> list;

            try
            {
                list = await _accountsRepository.GetItemsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed request: " + ex.InnerException.Message);
            }

            return list == null ? NotFound() : Ok(list);
        }

        [HttpGet("/api/accounts/{id}")]
        public async Task<ActionResult<AccountModel>> GetByIdAsync(int id)
        {
            AccountModel item = null;

            try
            {
                item = await _accountsRepository.GetItemAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed request: " + ex.InnerException.Message);
            }

            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("/api/accounts/ins")]
        public async Task<ActionResult<AccountModel>> InsertAccountAsync([FromBody] InsertAccountModel account)
        {
            int id = -1;

            try
            {
                id = await _accountsRepository.InsertItemAsync(account);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed request: " + ex.InnerException.Message);
            }

            if (id < 1)
                return BadRequest("Something wen't wrong.");

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
            try
            {
                _accountsRepository.UpdateItemAsync(account);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed request: " + ex.InnerException.Message);
            }

            return NoContent();
        }

        [HttpDelete("/api/accounts/del/{id}")]
        public IActionResult DeleteAccountAsync(int id)
        {
            try
            {
                _accountsRepository.DeleteItemAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed request: " + ex.InnerException.Message);
            }

            return NoContent();
        }
    }
}