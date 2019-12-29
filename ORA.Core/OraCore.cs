﻿using ORA.API;
using ORA.API.Loggers;
using ORA.Core.Loggers;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        public static void Initialize()
        {
            SetInstance(new OraCore());
        }

        private readonly ILogger _logger;

        private OraCore()
        {
            _logger = new SimpleLogger("OraCore");
        }

        public override ILogger Logger()
        {
            return _logger;
        }
    }
}