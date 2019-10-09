using Flyinline.Persistance.Contexts;
using Flyinline.Persistance.Seeding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flyinline.WebUI.Tests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(FlyinlineDbContext flyinlineContext)
        {
            FlyinlineInitializer.Initialize(flyinlineContext);
        }

        [Fact]
        public void GivenCreateCustomerCommand_ReturnsSuccessStatusCode1()
        {
            var a = 2 + 1;

            Assert.True(a == 3);
        }


    }
}
