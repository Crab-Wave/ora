using System;
using System.Linq;

using ReactiveUI;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        private string baseUrl;
        private string username;
        private string usernameInput;

        public string BaseUrl
        {
            get => this.baseUrl;
            set => this.RaiseAndSetIfChanged(ref this.baseUrl, value);
        }
        public string UsernameInput
        {
            get => this.usernameInput;
            set => this.RaiseAndSetIfChanged(ref this.usernameInput, value);
        }

        public string Username { get => this.username; }

        public SettingsViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.baseUrl = Ora.GetHttpClient().GetBaseUrl();
            if (!System.IO.File.Exists(Ora.GetDirectory("username")))
                System.IO.File.WriteAllText(Ora.GetDirectory("username"), "ora-user");
            this.username = this.usernameInput = System.IO.File.ReadAllText(Ora.GetDirectory("username"));
        }

        public void SetBaseUrl()
        {
            Ora.GetAuthManager().Disconnect();
            System.IO.File.WriteAllText(Ora.GetDirectory("ora-tracker"), this.baseUrl);
            Ora.GetHttpClient().SetBaseUrl(this.baseUrl);
            Ora.GetAuthManager().Authenticate();
            Ora.GetNodeManager().Initialize();
            this.mainWindowViewModel.Home = new HomeViewModel(Ora.GetClusterManager().GetClustersOfUser(
                    Ora.GetIdentityManager().GetIdentity().GetIdentifier()
                ).Select(cluster => new ClusterItem(this.mainWindowViewModel, cluster)));
        }

        public void SetUsername()
        {
            this.username = this.usernameInput;
            System.IO.File.WriteAllText(Ora.GetDirectory("username"), this.username);
        }
    }
}
