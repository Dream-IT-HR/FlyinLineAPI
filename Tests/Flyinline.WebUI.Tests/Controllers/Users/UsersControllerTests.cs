using Flyinline.Application.Users.Commands.Registration;
using Flyinline.WebUI.Tests.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Flyinline.Persistance.Seeding;
using Flyinline.WebUI.Models;
using Flyinline.Application.Users.Queries.GetUserDetailByUsername;
using System;

namespace Flyinline.WebUI.Tests.Controllers.Users
{
    public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenRegisterUserCommand_ReturnsSuccessStatusCode()
        {
            string userName = $"{SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[2])}_{Guid.NewGuid().ToString()}";

            var command = new RegisterUserCommand
            {
                Email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[2]),
                FullName = SeedHelpers.Fullnames[2],
                IsBusinessOwner = false,
                Nickname = "Te",
                Username = userName
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/users/register", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenGetUserDetailByUsernameCommandRetursGetUserDetailByUsernameViewModel()
        {
            string userName = $"{SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[0])}_{Guid.NewGuid().ToString()}";

            var registerCommand = new RegisterUserCommand
            {
                Email = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[0]),
                FullName = SeedHelpers.Fullnames[0],
                IsBusinessOwner = false,
                Nickname = SeedHelpers.Fullnames[0].Split(' ')[0],
                Username = userName
            };

            var registerContent = Utilities.GetRequestContent(registerCommand);

            var registerResponse = await _client.PostAsync($"/api/users/register", registerContent);

            registerResponse.EnsureSuccessStatusCode();


            var command = new TokenRequest
            {
                Username = userName,
                Password = "1234"
            };

            var content = Utilities.GetRequestContent(command);

            var tokenResponse = await _client.PostAsync($"/api/authentication/request", content);
            var token = await tokenResponse.Content.ReadAsStringAsync();

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/api/users/{userName}");

            response.EnsureSuccessStatusCode();

            var res = await Utilities.GetResponseContent<GetUserDetailByUsernameViewModel>(response);

            Assert.NotEmpty(res.Data);
        }
    }
}