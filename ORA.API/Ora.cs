using System;
using ORA.API.Loggers;

namespace ORA.API
{
    public abstract class Ora
    {
        private static Ora _instance;

        protected static Ora SetInstance(Ora instance)
        {
            if (Ora._instance != null)
            {
                throw new InvalidOperationException("Cannot redefine ORA instance !");
            }

            instance.Logger().Info("API initialized !");
            return Ora._instance = instance;
        }

        public static Ora Get()
        {
            return _instance;
        }

        public static ILogger GetLogger()
        {
            return Get().Logger();
        }

        protected Ora()
        {
        }

        public abstract ILogger Logger();
    }
}
