using KeySecret.DataAccess.Library.Categories.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<CategoryModel> _categoriesRepository;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IRepository<CategoryModel> categoriesRepository, ILogger<CategoriesController> logger)
        {
            _categoriesRepository = categoriesRepository;
            _logger = logger;
        }

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

        [HttpGet("/api/categories/{id}")]
        public async Task<ActionResult<CategoryModel>> GetByIdAsync(int id)
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

        [HttpPut("/api/categories/upd")]
        public ActionResult UpdateCategoryAsync([FromBody] CategoryModel category)
        {
            _categoriesRepository.UpdateItemAsync(category);

            _logger.LogInformation($"Kategorie wurde aktualisiert.");

            return NoContent();
        }

        [HttpDelete("/api/categories/del/{id}")]
        public ActionResult DeleteAccountAsync(int id)
        {
            _categoriesRepository.DeleteItemAsync(id);

            _logger.LogInformation($"Kategorie mit der ID {id} wurde gelöscht.");

            return NoContent();
        }
    }
}