using ORA.API.Loggers;
using ORA.Core.Loggers;

namespace ORA.Core
{
    public class ORACore : API.ORA
    {
        public static void Initialize()
        {
            SetInstance(new ORACore());
        }

        private readonly ILogger _logger;

        private ORACore()
        {
            _logger = new SimpleLogger("ORACore");
        }

        public override ILogger Logger()
        {
            return _logger;
        }
    }
}
