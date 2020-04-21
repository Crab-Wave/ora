using System;
using System.Threading.Tasks;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;

namespace ORA.API
{
    public abstract class Ora
    {
        private static Ora _instance;

        protected static Ora SetInstance(Ora instance)
        {
            if (_instance != null)
                throw new InvalidOperationException("Cannot redefine ORA instance !");

            instance.Logger().Info("API initialized !");
            return _instance = instance;
        }

        public static Ora Get() => _instance;

        public static ILogger GetLogger() => Get().Logger();

        public static IHttpClient GetHttpClient() => Get().HttpClient();

        public static ICipher GetCipher() => Get().Cipher();

        public static IClusterManager GetClusterManager() => Get().ClusterManager();

        public static INodeManager GetNodeManager() => Get().NodeManager();

        public abstract ILogger Logger();

        public abstract IHttpClient HttpClient();

        public abstract ICipher Cipher();

        public abstract IClusterManager ClusterManager();

        public abstract INodeManager NodeManager();
    }
}
