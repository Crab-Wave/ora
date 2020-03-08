using ORA.API;

namespace ORA.Core.Tests
{
    public class CoreInitializationFixture
    {
        static CoreInitializationFixture()
        {
            OraCore.Initialize();
        }
    }
}
