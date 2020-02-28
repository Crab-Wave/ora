using ORA.API;
using ORA.API.Http;
using Xunit;

namespace ORA.Core.Tests
{
    public class Tests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void HttpClientTests()
        {
            HttpResponse httpResponse = Ora.CreateHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }
    }
}
