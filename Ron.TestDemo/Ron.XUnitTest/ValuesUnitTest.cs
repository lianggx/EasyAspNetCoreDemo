using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace Ron.XUnitTest
{
    public class ValuesUnitTest
    {
        private TestServer testServer;
        private HttpClient httpCLient;

        public ValuesUnitTest()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Ron.TestDemo.Startup>());
            httpCLient = testServer.CreateClient();
        }

        [Fact]
        public async void GetTest()
        {
            var data = await httpCLient.GetAsync("/api/values/100");
            var result = await data.Content.ReadAsStringAsync();

            Assert.Equal("300", result);
        }
    }
}
