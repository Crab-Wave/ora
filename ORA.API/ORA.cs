using System;
using ORA.API.Loggers;

namespace ORA.API
{
    public abstract class ORA
    {
        private static ORA _instance;

        protected static ORA SetInstance(ORA instance)
        {
            if (ORA._instance != null)
            {
                throw new InvalidOperationException("Cannot redefine ORA instance !");
            }

            instance.Logger().Info("API initialized !");
            return ORA._instance = instance;
        }

        public static ORA Get()
        {
            return _instance;
        }

        protected ORA()
        {
        }

        public abstract ILogger Logger();
    }
}
