using ORA.API.Http;
using ORA.Core.Http;
using Xunit;

namespace ORA.Core.Tests
{
    public class HttpClientTests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void GetRequestTests()
        {
            var httpClient = new UnirestHttpClient();
            HttpResponse httpResponse = httpClient.Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }
    }
}
