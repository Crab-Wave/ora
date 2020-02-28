using System.Text;
using ORA.API;
using ORA.API.Encryption;
using Xunit;

namespace ORA.Core.Tests
{
    public class CipherTests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void RsaCipherTests()
        {
            ICipher cip = Ora.GetCipher();
            byte[] enc = cip.Encrypt(Encoding.ASCII.GetBytes("Hello World"));
            Assert.Equal(cip.Decrypt(enc), Encoding.ASCII.GetBytes("Hello World"));
        }
    }
}
