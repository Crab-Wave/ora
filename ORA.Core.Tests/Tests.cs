using System.Text;
using ORA.API;
using ORA.API.Encryption;
using ORA.API.Http;
using Xunit;
using Xunit.Abstractions;

namespace ORA.Core.Tests
{
    public class Tests : IClassFixture<CoreInitializationFixture>
    {
        public Tests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper;
        }

        private readonly ITestOutputHelper TestOutputHelper;

        [Fact]
        public void CipherTests()
        {
            ICipher cip = Ora.GetCipher();
            byte[] enc = cip.Encrypt(Encoding.ASCII.GetBytes("Hello World"));
            Assert.Equal(cip.Decrypt(enc), Encoding.ASCII.GetBytes("Hello World"));
        }

        [Fact]
        public void HttpTests()
        {
            HttpResponse httpResponse = Ora.CreateHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
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
