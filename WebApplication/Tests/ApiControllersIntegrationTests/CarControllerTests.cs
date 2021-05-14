using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.Helpers;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace Tests.ApiControllersIntegrationTests
{
    public class CarControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public CarControllerTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        AllowAutoRedirect = false
                    }
                );
        }
        
        [Fact]
        public async Task Api_Get_cars()
        {
            // ARRANGE
            var uri = "/api/v1/car";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            Assert.Equal(HttpStatusCode.Unauthorized , getTestResponse.StatusCode);
            await Api_Register();
        }
        
        public async Task Api_Register()
        {
            // ARRANGE
            var uri = "/api/v1/account/register";

            // ACT
            var getTestResponse = await _client.PostAsync(uri, 
                new StringContent("{\"email\":\"test@tete\", \"password\": \"Pass.123ABC\", \"displayName\": \"Name\" }", Encoding.UTF8, "application/json"));

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);

            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Identity.JwtResponse>(body);

            Assert.NotNull(data);
        }


    }
}