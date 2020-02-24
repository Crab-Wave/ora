using System;
using System.Text;
using ORA.API;
using ORA.API.Http;
using ORA.Core.Encryption;
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
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }

        [Fact]
        public void CipherTests()
        {
            byte[] enc = Ora.GetCipher().Encrypt(Encoding.ASCII.GetBytes("Hello World"));
            Assert.Equal(Ora.GetCipher().Decrypt(enc),Encoding.ASCII.GetBytes("Hello World"));
        }
    }
}
