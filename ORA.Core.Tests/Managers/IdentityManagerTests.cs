using FluentAssertions;
using ORA.API;
using ORA.Core.Managers;
using Xunit;

namespace ORA.Core.Tests.Managers
{
    public class IdentityManagerTests : IClassFixture<CoreInitializationFixture>
    {
        [Fact]
        public void GetIdentity()
        {
            Identity identity = Ora.GetIdentityManager().GetIdentity();
            identity.Should().NotBeNull();
        }
    }
}
