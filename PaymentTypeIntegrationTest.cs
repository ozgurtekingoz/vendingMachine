using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Net.Http.Headers;
using VendingMachine;

namespace IntegrationTest
{
    public class PaymentTypeIntegrationTest : IClassFixture<PaymentTypeTestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        public PaymentTypeIntegrationTest(PaymentTypeTestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Read_GET_Action()
        {
            // Act
            var response = await _client.GetAsync("/api/paymentType/all");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
