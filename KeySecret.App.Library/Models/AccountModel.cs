using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KeySecret.App.Library.Models
{
    public class AccountModel
    {
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "A name is required")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Max length is 256 charaters")]
        public string WebAdress { get; set; }

        [Required(ErrorMessage = "A Password is reqired"), MaxLength(128, ErrorMessage = "Max length is 128 characters")]
        public string Password { get; set; }

        public CategoryModel? Category { get; set; }
        public bool isDeleted { get; set; }
        public DateTime ModifiedDate { get; private set; }

        /// <summary>
        /// Constructor only for method "DtoAsAccountModel" and serialization
        /// </summary>
        public AccountModel()
        {
        }
        /// <summary>
        /// Constructor for new models inside applications
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="webAdress"></param>
        /// <param name="category"></param>
        public AccountModel(string name, string password, string webAdress = null, CategoryModel category = null)
        {
            Id = Guid.NewGuid();
            isDeleted = false;
            ModifiedDate = DateTime.Now;

            Name = name;
            WebAdress = webAdress;
            Password = password;
            Category = category;
        }

        /// <summary>
        /// Converts a AccountModel to a AccountDto, as comunication model for the API
        /// </summary>
        /// <returns>A AccountDto model</returns>
        public AccountDto AsDto()
        {
            return new AccountDto()
            {
                AccountId = this.Id,
                Name = this.Name,
                WebAddress = this.WebAdress,
                Password = this.Password,
                Category = this.Category == null ? null : this.Category.AsDto(),
                ModifiedDate = this.ModifiedDate
            };
        }

        /// <summary>
        /// Converts a AccountDto to a AccountModel, as internal model
        /// </summary>
        /// <param name="dto">The AccountDto model</param>
        /// <returns>A AccountModel</returns>
        public static AccountModel DtoAsAccountModel(AccountDto dto)
        {
            return new AccountModel()
            {
                Id = dto.AccountId,
                Name = dto.Name,
                WebAdress = dto.WebAddress,
                Password = dto.Password,
                Category = CategoryModel.DtoAsCategoryModel(dto.Category),
                isDeleted = false,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}