using KeySecret.DataAccess.Library.DataAccess;
using KeySecret.DataAccess.Library.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace KeySecret.DataAccess.Library.Tests
{
    public class AccountsDataTests
    {
        [Fact]
        public void GetAccessDataModels_GetAllAccountModels_ReturnList()
        {
            //List<AccountModel> expected = new List<AccountModel>
            //{
            //    new AccountModel { Id = 1, Name = "Test1", WebAdress = "test1@test.de", Password = "password1", CreatedDate = DateTime.Now },
            //    new AccountModel { Id = 2, Name = "Test2", WebAdress = "test2@test.de", Password = "password2", CreatedDate = DateTime.Now },
            //    new AccountModel { Id = 3, Name = "Test3", WebAdress = "test3@test.de", Password = "password3", CreatedDate = DateTime.Now }
            //};

            //var mock = new Mock<IAccountsData>();
            //mock.Setup(x => x.GetAccessDataModels());
        }
    }
}