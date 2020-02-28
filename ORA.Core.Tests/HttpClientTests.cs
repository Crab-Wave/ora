using ORA.API;
using ORA.API.Http;
using Xunit;

namespace ORA.Core.Tests
{
    public class Tests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void GetRequestTests()
        {
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }
    }
}
