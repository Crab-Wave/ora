using System;
using ORA.API;
using ORA.API.Http;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace ORA.Core.Tests
{
    public class Tests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void HttpTests()
        {
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }
    }
}
