using System;
using System.Text;
using ORA.API;
using ORA.API.Http;
using ORA.API.Encryption;
using Xunit;
using Xunit.Abstractions;

namespace ORA.Core.Tests
{
    public class Tests : IClassFixture<CoreInitializationFixture>
    {
        private readonly ITestOutputHelper TestOutputHelper;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper;
        }

        [Fact]
        public void HttpTests()
        {
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }

        [Fact]
        public void CipherTests()
        {
            ICipher cip = Ora.GetCipher();
            byte[] enc = cip.Encrypt(Encoding.ASCII.GetBytes("Hello World"));
            Assert.Equal(cip.Decrypt(enc),Encoding.ASCII.GetBytes("Hello World"));
        }
    }

    public class CoreInitializationFixture
    {
        public CoreInitializationFixture()
        {
            OraCore.Initialize();
        }
    }
}
