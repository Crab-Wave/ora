using ReactiveUI;

namespace ORA.App.GUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IRoutableViewModel
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; } = "/settings";

        public SettingsViewModel(IScreen screen) => HostScreen = screen;
    }
}
