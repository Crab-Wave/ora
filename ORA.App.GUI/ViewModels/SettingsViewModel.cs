using ReactiveUI;
using ORA.API;

namespace ORA.App.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string baseUrl;
        public string BaseUrl
        {
            get => this.baseUrl;
            set => this.RaiseAndSetIfChanged(ref this.baseUrl, value);
        }

        public SettingsViewModel()
        {
            this.baseUrl = Ora.GetHttpClient().GetBaseUrl();
        }

        public void SetBaseUrl()
        {
            Ora.GetHttpClient().SetBaseUrl(this.BaseUrl);
            Ora.GetAuthManager().Authenticate();
        }
    }
}
