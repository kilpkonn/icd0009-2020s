using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Test.Helpers;
using Tests.Helpers;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace Tests.MvcControllersIntegrationTests
{
    public class CarControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public CarControllerTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = _client = factory
                .WithWebHostBuilder(builder => { builder.UseSetting("test_database_name", Guid.NewGuid().ToString()); })
                .CreateClient(new WebApplicationFactoryClientOptions()

                {
                    AllowAutoRedirect = false
                });
        }

        [Fact]
        public async Task TestAuthAction_AuthFlow()
        {
            // ARRANGE
            var uri = "/car";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
            var redirectUri = getTestResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            Assert.NotNull(redirectUri);

            await Get_Login_Page(redirectUri!);
            // we need to follow the redirect
            // get the login page
            // get the registration page
            // fill up the reg form
            // submit form
            // try to access auth resource again - we should have new user and be logged in
        }

        public async Task Get_Login_Page(string uri)
        {
            var getLoginPageResponse = await _client.GetAsync(uri);
            getLoginPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getLoginDocument = await HtmlHelpers.GetDocumentAsync(getLoginPageResponse);

            var registerAnchorElement = (IHtmlAnchorElement) getLoginDocument.QuerySelector("#register");
            var registerUrl = registerAnchorElement.Href;
            _testOutputHelper.WriteLine("Register url: " + registerUrl);

            await Get_Register_Page(registerUrl);
        }

        public async Task Get_Register_Page(string uri)
        {
            var getRegisterPageResponse = await _client.GetAsync(uri);
            getRegisterPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getRegisterDocument = await HtmlHelpers.GetDocumentAsync(getRegisterPageResponse);

            var regForm = (IHtmlFormElement) getRegisterDocument.QuerySelector("#register-form");
            var regFormValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "test@user.ee",
                ["Input_Password"] = "Foo.bar1",
                ["Input_ConfirmPassword"] = "Foo.bar1",
                ["Input_DisplayName"] = "Test",
            };

            var regPostResponse = await _client.SendAsync(regForm, regFormValues);

            Assert.Equal(HttpStatusCode.Redirect, regPostResponse.StatusCode);

            var redirectUri = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            Assert.NotNull(redirectUri);

            await Get_TestAuthAction_Authenticated(redirectUri!);
        }

        public async Task Get_TestAuthAction_Authenticated(string uri)
        {
            var getTestResponse = await _client.GetAsync(uri);
            getTestResponse.EnsureSuccessStatusCode();

            _testOutputHelper.WriteLine(
                $"Uri '{uri}' was accessed with response status code '{getTestResponse.StatusCode}'.");
            await Get_CarsIndex();
        }
        
        public async Task Get_CarsIndex()
        {
            var getTestResponse = await _client.GetAsync("/car");
            getTestResponse.EnsureSuccessStatusCode();

            var getCarIndexDocument = await HtmlHelpers.GetDocumentAsync(getTestResponse);

            var carsTable = (IHtmlTableElement) getCarIndexDocument.QuerySelector("#car-table");
            
            Assert.Equal(1, carsTable.Rows.Length);
            await Get_CarsCreate();
        }
        
        public async Task Get_CarsCreate()
        {
            var getTestResponse = await _client.GetAsync("/car/create");
            getTestResponse.EnsureSuccessStatusCode();

            var getCarIndexDocument = await HtmlHelpers.GetDocumentAsync(getTestResponse);

            var carFrom = (IHtmlFormElement) getCarIndexDocument.QuerySelector("#car-create");
            var carTypeSelect = (IHtmlSelectElement) getCarIndexDocument.QuerySelector("#car-type-select");
            
            var createCarVals = new Dictionary<string, string>()
            {
                ["Car.CarTypeId"] = carTypeSelect.Options.GetOptionAt(0).Value,
            };

            var regPostResponse = await _client.SendAsync(carFrom, createCarVals);
            
            Assert.Equal(HttpStatusCode.Redirect, regPostResponse.StatusCode);

            var redirectUri = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            Assert.NotNull(redirectUri);
            await Get_CarsIndexV2();
        }
        
        public async Task Get_CarsIndexV2()
        {
            var getTestResponse = await _client.GetAsync("/car");
            getTestResponse.EnsureSuccessStatusCode();

            var getCarIndexDocument = await HtmlHelpers.GetDocumentAsync(getTestResponse);

            var carsTable = (IHtmlTableElement) getCarIndexDocument.QuerySelector("#car-table");
            
            Assert.Equal(2, carsTable.Rows.Length);
            await Logout();
        }
        
        public async Task Logout()
        {
            var getTestResponse = await _client.GetAsync("/");
            getTestResponse.EnsureSuccessStatusCode();

            var getCarIndexDocument = await HtmlHelpers.GetDocumentAsync(getTestResponse);

            var logoutForm = (IHtmlFormElement) getCarIndexDocument.QuerySelector("#logout");
            
            var regPostResponse = await _client.SendAsync(logoutForm, new Dictionary<string, string>());
            
            Assert.Equal(HttpStatusCode.Redirect, regPostResponse.StatusCode);

            var redirectUri = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            Assert.NotNull(redirectUri);
        }
    }
}