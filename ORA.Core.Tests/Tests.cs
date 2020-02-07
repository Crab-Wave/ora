using System;
using ORA.API;
using ORA.API.Http;
using Xunit;
using Xunit.Abstractions;

namespace ORA.Core.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper TestOutputHelper;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper;
        }

        [Fact]
        public void HttpTests()
        {
            OraCore.Initialize();
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }
    }
}
