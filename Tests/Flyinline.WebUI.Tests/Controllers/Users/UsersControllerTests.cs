using Flyinline.Application.Users.Commands.Registration;
using Flyinline.WebUI.Tests.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Flyinline.Persistance.Seeding;
using Flyinline.WebUI.Models;

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
            var command = new RegisterUserCommand
            {
                Email = "test.testic@test.hr",
                FullName = "Test Testic",
                IsBusinessOwner = false,
                Nickname = "Te",
                Username = "test.testic@test.hr"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/users/register", content);

            response.EnsureSuccessStatusCode();
        }

        //[Fact]
        //public async Task ReturnsIEnumerableOfCategoryPreviewDto()
        //{
        //    var command = new TokenRequest
        //    {
        //        Username = SeedHelpers.GetEmailFromFullName(SeedHelpers.Fullnames[0]),
        //        Password = "1234"
        //    };

        //    var content = Utilities.GetRequestContent(command);

        //    var tokenResponse = await _client.PostAsync($"/api/authentication/request", content);
        //    var token = await tokenResponse.Content.ReadAsStringAsync();

        //    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        //    var response = await _client.GetAsync($"/api/testclean/claim-permissions/{SeedHelpers.Guids[0].ToString()}");

        //    response.EnsureSuccessStatusCode();

        //    GetClaimPermissionsViewModel res = await Utilities.GetResponseContent<GetClaimPermissionsViewModel>(response);

        //    Assert.NotEmpty(res.Data);
    }
}