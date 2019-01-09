using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;

namespace UserCenterDemoTest
{
    public class HomeControllerTest
    {
        public static TestServer serverHost;
        public static HttpClient client;
        public HomeControllerTest()
        {
            if (serverHost == null)
            {
                serverHost = new TestServer(new WebHostBuilder().UseStartup<UserCenterDemo.Startup>());
                client = serverHost.CreateClient();
            }
        }

        class TestResult
        {
            public int Code { get; set; }
            public string UserName { get; set; }
        }

        [Fact]
        public async void GetUserNameTest()
        {
            var data = await client.GetAsync("/api/home/100");
            var result = await data.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<TestResult>(result);
            Assert.Equal(0, obj.Code);
        }

        [Fact]
        public async void GetGuestTest()
        {
            var data = await client.GetAsync("/api/home/0");
            var result = await data.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<TestResult>(result);
            Console.WriteLine(result);
            Assert.Equal(403, obj.Code);
        }
    }
}
