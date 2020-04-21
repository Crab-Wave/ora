using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using ORA.API;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core;
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

            IServiceCollection services = ConfigureServices(new ServiceCollection());
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<ILogger>("logger", "ora-logger")
                .AddNamedPipeEndpoint<IHttpClient>("httpClient", "ora-http")
                .AddNamedPipeEndpoint<ICipher>("cipher", "ora-cipher")
                .AddNamedPipeEndpoint<IClusterManager>("cluster", "ora-cluster")
                .Build()
                .Run();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services) =>
            services
                .AddIpc(builder => builder.AddNamedPipe().AddService<ILogger, SimpleLogger>(provider => (SimpleLogger) Ora.GetLogger()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IHttpClient, UnirestHttpClient>(provider => (UnirestHttpClient) Ora.GetHttpClient()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<ICipher, RsaCipher>(provider => (RsaCipher) Ora.GetCipher()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<IClusterManager, ClusterManager>(provider => (ClusterManager) Ora.GetClusterManager()));
    }
}
