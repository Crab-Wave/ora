using ReactiveUI;
using ORA.API;

namespace ORA.App.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
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

        public SettingsViewModel()
        {
            this.baseUrl = Ora.GetHttpClient().GetBaseUrl();
            this.username = this.usernameInput = "ora-user";
        }

        public void SetBaseUrl()
        {
            if (Ora.Get().IsOraTracker(this.baseUrl))
            {
                Ora.GetHttpClient().SetBaseUrl(this.baseUrl);
                Ora.GetAuthManager().Authenticate();
            }
            else
            {
                this.baseUrl = "INVALID URL";
            }
        }

        public void SetUsername()
        {
            this.username = this.usernameInput;
        }
    }
}
