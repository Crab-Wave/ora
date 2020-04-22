using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using ORA.API;
using ORA.API.Compression;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core;
using ORA.Core.Compression;
using ORA.Core.Encryption;
using ORA.Core.Http;
using ORA.Core.Loggers;
using ORA.Core.Managers;

namespace ORA.Application.Daemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OraCore.Initialize();
            Ora.GetLogger().ShouldPrint(true);
            Ora.GetAuthManager().Authenticate();

            IServiceCollection services = ConfigureServices(new ServiceCollection());
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<ILogger>("logger", "ora-logger")
                .AddNamedPipeEndpoint<ICipher>("cipher", "ora-cipher")
                .AddNamedPipeEndpoint<ICompressor>("compressor", "ora-compressor")
                .AddNamedPipeEndpoint<IHttpClient>("httpClient", "ora-http")
                .AddNamedPipeEndpoint<IAuthManager>("auth", "ora-auth")
                .AddNamedPipeEndpoint<IIdentityManager>("identity", "ora-identity")
                .AddNamedPipeEndpoint<IClusterManager>("cluster", "ora-cluster")
                .Build()
                .Run();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services) =>
            services
                .AddIpc(builder => builder.AddNamedPipe().AddService<ILogger, SimpleLogger>(provider => (SimpleLogger) Ora.GetLogger()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<ICipher, RsaCipher>(provider => (RsaCipher) Ora.GetCipher()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<ICompressor, ZipLibCompressor>(provider => (ZipLibCompressor) Ora.GetCompressor()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IHttpClient, UnirestHttpClient>(provider => (UnirestHttpClient) Ora.GetHttpClient()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IAuthManager, AuthManager>(provider => (AuthManager) Ora.GetAuthManager()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IIdentityManager, IdentityManager>(provider => (IdentityManager) Ora.GetIdentityManager()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IClusterManager, ClusterManager>(provider => (ClusterManager) Ora.GetClusterManager()));
    }
}
