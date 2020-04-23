using ORA.API.Http;
using ORA.Core.Http;
using Xunit;
using FluentAssertions;

namespace ORA.Core.Tests.Http
{
    public class HttpClientTests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void GetRequestTests()
        {
            IHttpClient testee = new UnirestHttpClient();
            var httpResponse = testee.Get("http://httpbin.org/get");

            httpResponse.Should().NotBeNull();
            httpResponse.Body.Should().NotBeEmpty();
        }
    }
}
