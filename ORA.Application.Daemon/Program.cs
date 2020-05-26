using System.Net.NetworkInformation;
using System.Timers;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using ORA.API;
using ORA.API.Compression;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.API.Utils;
using ORA.Core;

namespace ORA.Application.Daemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OraCore.Initialize();
            Ora.GetLogger().ShouldPrint(true);
            Ora.GetAuthManager().Authenticate();

            NetworkChange.NetworkAvailabilityChanged +=
                (sender, eventArgs) =>
                    Ora.GetHttpClient().Post("/refreship",
                        new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            NetworkChange.NetworkAddressChanged +=
                (sender, eventArgs) =>
                    Ora.GetHttpClient().Post("/refreship",
                        new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));

            var refreshTokenTimer = new Timer();
            refreshTokenTimer.Elapsed += (sender, eventArgs) => Ora.GetAuthManager().RefreshToken();
            refreshTokenTimer.Interval = 1000 * 60 * 60;
            refreshTokenTimer.Enabled = true;

            var synchronizeTimer = new Timer();
            synchronizeTimer.Elapsed += (sender, eventArgs) =>
            {
                foreach (Cluster cluster in Ora.GetClusterManager().GetClusters())
                    foreach (string file in Ora.GetFileManager().GetFiles(cluster))
                        Ora.GetFileManager().GetFile(cluster, file);
            };
            synchronizeTimer.Interval = 1000 * 60;
            synchronizeTimer.Enabled = true;

            IServiceCollection services = ConfigureServices(new ServiceCollection());
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<StringProvider>("program-directory", "ora-program-directory")
                .AddNamedPipeEndpoint<StringProviderWithStringArray>("directory", "ora-directory")
                .AddNamedPipeEndpoint<ILogger>("logger", "ora-logger")
                .AddNamedPipeEndpoint<ICipher>("cipher", "ora-cipher")
                .AddNamedPipeEndpoint<ICompressor>("compressor", "ora-compressor")
                .AddNamedPipeEndpoint<IHttpClient>("httpClient", "ora-http")
                .AddNamedPipeEndpoint<IAuthManager>("auth", "ora-auth")
                .AddNamedPipeEndpoint<IIdentityManager>("identity", "ora-identity")
                .AddNamedPipeEndpoint<IClusterManager>("cluster", "ora-cluster")
                .AddNamedPipeEndpoint<INetworkManager>("network", "ora-network")
                .AddNamedPipeEndpoint<IFileManager>("file", "ora-file")
                .Build()
                .Run();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services) =>
            services
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<StringProvider, StringProvider>(provider => new ProgramDirectoryProvider()))
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<StringProviderWithStringArray, StringProviderWithStringArray>(provider =>
                            new DirectoryProvider()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<ILogger, ILogger>(provider => Ora.GetLogger()))
                .AddIpc(builder => builder.AddNamedPipe().AddService<ICipher, ICipher>(provider => Ora.GetCipher()))
                .AddIpc(builder =>
                    builder.AddNamedPipe().AddService<ICompressor, ICompressor>(provider => Ora.GetCompressor()))
                .AddIpc(builder =>
                    builder.AddNamedPipe().AddService<IHttpClient, IHttpClient>(provider => Ora.GetHttpClient()))
                .AddIpc(builder =>
                    builder.AddNamedPipe().AddService<IAuthManager, IAuthManager>(provider => Ora.GetAuthManager()))
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<IIdentityManager, IIdentityManager>(provider => Ora.GetIdentityManager()))
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<IClusterManager, IClusterManager>(provider => Ora.GetClusterManager()))
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<INetworkManager, INetworkManager>(provider => Ora.GetNetworkManager()))
                .AddIpc(builder =>
                    builder.AddNamedPipe()
                        .AddService<IFileManager, IFileManager>(provider => Ora.GetFileManager()));
    }

    internal class ProgramDirectoryProvider : StringProvider
    {
        public string Provide() => Ora.GetProgramDirectory();
    }

    internal class DirectoryProvider : StringProviderWithStringArray
    {
        public string Provide(string[] strings) => Ora.GetDirectory(strings);
    }
}
