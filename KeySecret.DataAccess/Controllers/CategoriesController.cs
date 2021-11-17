using KeySecret.DataAccess.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using KeySecret.DataAccess.Library.Repositories;
using System;

namespace KeySecret.DataAccess.Controllers
{
    /// <summary>
    /// <see cref="CategoryModel"/> endpoints of the api.
    /// </summary>
    //[Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<CategoryModel> _categoriesRepository;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IRepository<CategoryModel> categoriesRepository, ILogger<CategoriesController> logger)
        {
            _categoriesRepository = categoriesRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all <see cref="CategoryModel"/> from the database.
        /// </summary>
        /// <returns><seealso cref="IEnumerable{T}"/> if found, else <seealso cref="NotFoundResult"/>.</returns>
        [HttpGet("/api/categories")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryModel> list = await _categoriesRepository.GetItemsAsync();

            if (list == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'list' ist NULL");
                return NotFound(nameof(GetAllCategoriesAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation($"Es wurde eine Liste aller Kategorien angefordert. {((List<CategoryModel>)list).Count} Kategorien wurden zurückgegeben.");
            return Ok(list);
        }

        /// <summary>
        /// Get a <see cref="CategoryModel"/> by id.
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <returns><see cref="CategoryModel"/> if found, else <see cref="NotFoundResult"/>.</returns>
        [HttpGet("/api/categories/{id}")]
        public async Task<ActionResult<CategoryModel>> GetByIdAsync(Guid id)
        {
            CategoryModel item = await _categoriesRepository.GetItemAsync(id);

            if (item == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'list' ist NULL");
                return NotFound(nameof(GetByIdAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation($"Die Kategorie mit der ID {id} wurd angefordert.");
            return Ok(item);
        }

        /// <summary>
        /// Insert a new <see cref="CategoryModel"/> into the database.
        /// </summary>
        /// <param name="account">The serialized json as <see cref="CategoryModel"/>.</param>
        /// <returns><seealso cref="CreatedAtActionResult"/> on success, else <seealso cref="BadRequestResult"/>.</returns>
        [HttpPost("/api/categories/ins")]
        public async Task<ActionResult<CategoryModel>> InsertCategoryAsync([FromBody] CategoryModel category)
        {
            CategoryModel item = await _categoriesRepository.InsertItemAsync(category);

            if (item == null)
            {
                _logger.LogError("Fehler bei der Rückgabe. Objekt 'model' ist NULL");
                return BadRequest(nameof(InsertCategoryAsync) + ": NULL-Object returned");
            }

            _logger.LogInformation("Es wurde ein Eintrag in die Datenbank geschrieben.");
            return CreatedAtAction(nameof(InsertCategoryAsync), item);
        }

        /// <summary>
        /// Updates a <see cref="CategoryModel"/>.
        /// </summary>
        /// <param name="account">The serialized json as <see cref="CategoryModel"/>.</param>
        /// <returns><seealso cref="NoContentResult"/></returns>
        [HttpPut("/api/categories/upd")]
        public ActionResult UpdateCategoryAsync([FromBody] CategoryModel category)
        {
            _categoriesRepository.UpdateItemAsync(category);

            _logger.LogInformation($"Kategorie wurde aktualisiert.");

            return NoContent();
        }

        /// <summary>
        /// Deletes a <see cref="CategoryModel"/> from the database
        /// </summary>
        /// <param name="id">Id as <seealso cref="Guid"/></param>
        /// <returns><seealso cref="NoContentResult"/></returns>
        [HttpDelete("/api/categories/del/{id}")]
        public ActionResult DeleteAccountAsync(Guid id)
        {
            _categoriesRepository.DeleteItemAsync(id);

            _logger.LogInformation($"Kategorie mit der ID {id} wurde gelöscht.");

            return NoContent();
        }
    }
}