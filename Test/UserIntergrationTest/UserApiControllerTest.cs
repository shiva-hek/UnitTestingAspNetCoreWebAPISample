using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi;

namespace UserIntergrationTest
{
    public class UserApiControllerTest : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;

        public UserApiControllerTest(WebApplicationFactory<IApiMarker> appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        //[Fact]
        //public async Task Get_ReturnNotFound_WhenCustomerDoesNotExist()
        //{
        //    // Act
        //    var respond = await _httpClient.GetAsync($"User/");
        //    // Assert

        //}
    }
}
