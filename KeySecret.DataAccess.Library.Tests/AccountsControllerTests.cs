using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using FluentAssertions;
using KeySecret.DataAccess.Controllers;
using KeySecret.DataAccess.Library.Accounts.Models;
using KeySecret.DataAccess.Library.Interfaces;

namespace KeySecret.DataAccess.Library.Tests
{
    public class AccountsControllerTests
    {
        private readonly Mock<IRepository<AccountModel>> repositoryMock = new();
        private readonly Random rand = new Random();

        private AccountModel CreateRandomItem()
        {
            return new AccountModel()
            {
                Id = rand.Next(1000),
                Name = Guid.NewGuid().ToString(),
                WebAdress = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now
            };
        }

        [Fact]
        [Trait("AccountsController", "GetByIdAsync")]
        public async Task GetByIdAsync_WithExistingItem_ReturnAccountModel()
        {
            // Arrange
            var controller = new AccountsController(repositoryMock.Object);
            var expectedItem = CreateRandomItem();
            repositoryMock.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync(expectedItem);

            //Act
            var result = await controller.GetByIdAsync(rand.Next());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        [Trait("AccountsController", "GetByIdAsync")]
        public async Task GetByIdAsync_WithNoExistingItem_ReturnsNotFound()
        {
            // Arrange
            var controller = new AccountsController(repositoryMock.Object);
            repositoryMock.Setup(repo => repo.GetItemAsync(It.IsAny<int>())).ReturnsAsync((AccountModel)null);

            //Act
            var result = await controller.GetByIdAsync(rand.Next());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        [Trait("AccountsController", "GetAllAccountsAsync")]
        public async Task GetAllAccountsAsync_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var controller = new AccountsController(repositoryMock.Object);
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            repositoryMock.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(expectedItems);

            // Act
            var actualItems = await controller.GetAllAccountsAsync();

            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems);
        }

        [Fact]
        [Trait("AccountsController", "InsertAccountAsync")]
        public async Task InsertAccountAsync_WithItemToInsert_ReturnsCreatedItem()
        {
            // Arrange
            var controller = new AccountsController(repositoryMock.Object);
            var newItem = new AccountModel()
            {
                Name = Guid.NewGuid().ToString(),
                WebAdress = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            // Act
            var result = await controller.InsertAccountAsync(newItem);

            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as AccountModel;
            newItem.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<AccountModel>().ExcludingMissingMembers()
                );
            createdItem.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}