using System;
using System.Threading.Tasks;

using Moq;
using Xunit;

using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.Interfaces;
using RichardSzalay.MockHttp;
using System.Text.Json;
using System.Net.Http;
using Moq.Protected;
using System.Threading;
using System.Net;
using KeySecret.DesktopApp.Library.DataAccess;
using FluentAssertions;

namespace KeySecret.DesktopApp.Library.Tests
{
    internal class Helper : IApiHelper
    {
        public HttpClient Client { get; set; }
    }

    public class AccountEndpointTests
    {
        private readonly Mock<IEndpoint<AccountModel>> mockEndpoint = new();
        private readonly MockHttpMessageHandler mockHttp = new();
        private readonly Random rand = new Random();
        private readonly string apiAdress = "http://localhost:5000/";
        private readonly string wildcard = "*";

        private AccountModel CreateRandomAccountModel()
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

        private string ConvertToJson(AccountModel item) => JsonSerializer.Serialize(item);

        ////SOURCE: https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
        //[Fact]
        //[Trait("AccountEndpoint", "GetById")]
        //public async Task GetById_WithExistingItem_ReturnsAccountModel()
        //{
        //// ARRANGE
        //var expectedItem = CreateRandomAccountModel();
        //var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //handlerMock
        //   .Setup(m => m.)
        //   .ReturnsAsync(new HttpResponseMessage()
        //   {
        //       StatusCode = HttpStatusCode.OK,
        //       Content = new StringContent(ConvertToJson(expectedItem)),
        //   })
        //   .Verifiable();

        //var httpClient = new HttpClient(handlerMock.Object);
        //httpClient.BaseAddress = new Uri(apiAdress);
        //var apiHelper = new Helper() { Client = httpClient };
        //var accountEndpoint = new AccountEndpoint(apiHelper);

        //// ACT
        //var result = await accountEndpoint.GetById(1);

        //// ASSERT
        //result.Should().NotBeNull();
        //result.Id.Should().Be(1);

        //var expectedUri = new Uri("http://test.com/api/test/whatever");

        //handlerMock.Protected().Verify(
        //   "SendAsync",
        //   Times.Exactly(1),
        //   ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == expectedUri),
        //   ItExpr.IsAny<CancellationToken>()
        //);
        //}
    }
}