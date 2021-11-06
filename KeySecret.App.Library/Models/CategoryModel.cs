using System;
using System.ComponentModel.DataAnnotations;

namespace KeySecret.App.Library.Models
{
    public class CategoryModel
    {
        public Guid Id { get; private set; }

        [MaxLength(128, ErrorMessage = "Max length is 128 characters")]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; private set; }

        public CategoryModel(string name = null)
        {
            Id = Guid.NewGuid();
            ModifiedDate = DateTime.Now;

            Name = name;
        }

        /// <summary>
        /// Converts a CategoryModel to a CategoryDto, as comunication model for the API
        /// </summary>
        /// <returns>A CategoryDto model</returns>
        public CategoryDto AsDto()
        {
            return new CategoryDto()
            {
                CategoryId = this.Id,
                Name = this.Name,
                ModifiedDate = this.ModifiedDate
            };
        }

        /// <summary>
        /// Converts a CategoryDto to a CategoryModel, as internal model
        /// </summary>
        /// <param name="dto">The CategoryDto model</param>
        /// <returns>A CategoryModel</returns>
        public static CategoryModel DtoAsCategoryModel(CategoryDto dto)
        {
            if (dto == null)
                return null;

            return new CategoryModel()
            {
                Id = dto.CategoryId,
                Name = dto.Name,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}