using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using FluentAssertions;
using KeySecret.DataAccess.Controllers;
using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KeySecret.DataAccess.Library.Tests
{
    public class AccountsControllerTests
    {
        private readonly Mock<IRepository<AccountModel>> _repositoryMock = new();
        private readonly Mock<ILogger<AccountsController>> _loggerMock = new();
        private readonly Random _rand = new Random();

        private AccountModel CreateRandomItem()
        {
            return new AccountModel()
            {
                Id = _rand.Next(1000),
                Name = Guid.NewGuid().ToString(),
                WebAdress = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                CategoryId = _rand.Next(1000),
                CreatedDate = DateTime.Now
            };
        }

        private AccountModel CreateRandomInsertItem()
        {
            return new AccountModel()
            {
                Name = Guid.NewGuid().ToString(),
                WebAdress = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                CategoryId = _rand.Next(1000)
            };
        }

        [Fact]
        [Trait("AccountsController", "GetByIdAsync")]
        public async Task GetByIdAsync_WithExistingItem_ReturnAccountModel()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            var expectedItem = CreateRandomItem();
            _repositoryMock.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(expectedItem);

            //Act
            var result = await controller.GetByIdAsync(_rand.Next());

            // Assert
            var createdItem = (result.Result as OkObjectResult).Value as AccountModel;
            createdItem.Should().BeEquivalentTo(expectedItem);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        [Trait("AccountsController", "GetByIdAsync")]
        public async Task GetByIdAsync_WithNoExistingItem_ReturnsNotFound()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            _repositoryMock.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((AccountModel)null);

            //Act
            var result = await controller.GetByIdAsync(_rand.Next());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        [Trait("AccountsController", "GetAllAccountsAsync")]
        public async Task GetAllAccountsAsync_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            var expectedItems = new List<AccountModel>() { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            _repositoryMock.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actualItems = await controller.GetAllAccountsAsync();

            // Assert
            var createdItem = (actualItems.Result as OkObjectResult).Value as List<AccountModel>;
            createdItem.Should().BeEquivalentTo(expectedItems);
            actualItems.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        [Trait("AccountsController", "InsertAccountAsync")]
        public async Task InsertAccountAsync_WithItemToInsert_ReturnCreatedItem()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            var insertItem = CreateRandomInsertItem();
            var returnItem = insertItem;
            returnItem.Id = _rand.Next(1000);
            returnItem.CreatedDate = DateTime.Now;
            _repositoryMock.Setup(repo => repo.InsertItemAsync(insertItem)).ReturnsAsync(returnItem);

            // Act
            var result = await controller.InsertAccountAsync(insertItem);

            // Assert
            var createdItemResult = result.Result as CreatedAtActionResult;
            createdItemResult.Should().BeOfType<CreatedAtActionResult>();

            var createdItem = createdItemResult.Value as AccountModel;
            createdItem.Should().BeEquivalentTo(returnItem);
        }

        [Fact]
        [Trait("AccountsController", "InsertAccountAsync")]
        public async Task InsertAccountAsync_WithNoDBReturn_ReturnBadRequest()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            var insertItem = CreateRandomInsertItem();
            _repositoryMock.Setup(repo => repo.InsertItemAsync(insertItem)).ReturnsAsync((AccountModel)null);

            // Act
            var result = await controller.InsertAccountAsync(insertItem);

            // Assert
            var createdItem = result.Result as BadRequestObjectResult;
            createdItem.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        [Trait("AccountsController", "UpdateAccountAsync")]
        public void UpdateAccountAsync_WithValidValues_ReturnNoContent()
        {
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            var item = CreateRandomItem();
            _repositoryMock.Setup(repo => repo.UpdateItemAsync(item));

            // Act
            var result = controller.UpdateAccountAsync(item);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        [Trait("AccountsController", "DeleteAccountAsync")]
        public void DeleteAccountAsync_WithId_ReturnNoContent()
        {
            /*
            // Arrange
            var controller = new AccountsController(_repositoryMock.Object, _loggerMock.Object);
            _repositoryMock.Setup(repo => repo.DeleteItemAsync(1));

            // Act
            var result = controller.DeleteAccountAsync(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}