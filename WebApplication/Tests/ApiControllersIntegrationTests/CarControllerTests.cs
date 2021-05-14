using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
                .WithWebHostBuilder(builder => { builder.UseSetting("test_database_name", Guid.NewGuid().ToString()); })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        AllowAutoRedirect = false
                    }
                );
        }

        [Fact]
        public async Task Api_Get_Cars()
        {
            // ARRANGE
            var uri = "/api/v1/car";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            Assert.Equal(HttpStatusCode.Unauthorized, getTestResponse.StatusCode);
            await Api_Register();
        }

        public async Task Api_Register()
        {
            // ARRANGE
            var uri = "/api/v1/account/register";

            // ACT
            var getTestResponse = await _client.PostAsync(uri,
                new StringContent(
                    "{\"email\":\"test@tete\", \"password\": \"Pass.123ABC\", \"displayName\": \"Name\" }",
                    Encoding.UTF8, "application/json"));

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);

            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Identity.JwtResponse>(body);

            Assert.NotNull(data);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data!.Token);
            await Api_GetCarsV2();
        }

        public async Task Api_GetCarsV2()
        {
            // ARRANGE
            var uri = "/api/v1/car";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);

            var data = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Car>>(body);

            Assert.NotNull(data);
            Assert.Empty(data!);
            await Api_PostCar();
        }

        public async Task Api_PostCar()
        {
            // ARRANGE
            var uri = "/api/v1/car";

            // ACT

            var res1 = await _client.GetAsync("/api/v1/cartype");
            res1.EnsureSuccessStatusCode();
            var body1 = await res1.Content.ReadAsStringAsync();

            var types = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.CarType>>(body1);

            var getTestResponse = await _client.PostAsync(uri,
                new StringContent("{\"carTypeId\": \"" + types![0].Id + "\" }", Encoding.UTF8, "application/json"));

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);

            var data = JsonHelper.DeserializeWithWebDefaults<PublicApi.DTO.v1.Car>(body);

            Assert.NotNull(data);
            await Api_GetCarsV3();
        }
        
        public async Task Api_GetCarsV3()
        {
            // ARRANGE
            var uri = "/api/v1/car";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();

            var body = await getTestResponse.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);

            var data = JsonHelper.DeserializeWithWebDefaults<List<PublicApi.DTO.v1.Car>>(body);

            Assert.NotNull(data);
            Assert.Single(data!);
        }
    }
}