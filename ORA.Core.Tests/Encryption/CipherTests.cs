using System.Text;
using ORA.API;
using ORA.API.Encryption;
using Xunit;
using FluentAssertions;
using ORA.Core.Encryption;

namespace ORA.Core.Tests.Encryption
{
    public class CipherTests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void RsaCipherTests()
        {
            ICipher c = Ora.GetCipher();
            byte[] message = Encoding.ASCII.GetBytes("Hello World");

            c.Decrypt(c.Encrypt(message)).Should().Equal(message);

            if (!(c is RsaCipher)) return;
            ICipher cipher = new RsaCipher(((RsaCipher) c).KeySize, ((RsaCipher) c).GetPublicKey(),
                ((RsaCipher) c).GetPrivateKey());
            cipher.Decrypt(c.Encrypt(message)).Should().Equal(message);
        }
    }
}
