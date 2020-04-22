﻿using System;
using ORA.API.Compression;
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

        public static IIdentityManager GetIdentityManager() => Get().IdentityManager();

        public static IClusterManager GetClusterManager() => Get().ClusterManager();

        public static INodeManager GetNodeManager() => Get().NodeManager();

        public static ICompressor GetCompressor() => Get().Compressor();

        public static IAuthManager GetAuthManager() => Get().AuthManager();

        public abstract ILogger Logger();

        public abstract IHttpClient HttpClient();

        public abstract ICipher Cipher();

        public abstract IIdentityManager IdentityManager();

        public abstract IClusterManager ClusterManager();

        public abstract INodeManager NodeManager();

        public abstract ICompressor Compressor();

        public abstract IAuthManager AuthManager();
    }
}
