using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using ReactiveUI;

namespace ORA.App.GUI.ViewModels
{
    public class HomeViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "/home";

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public string TestProp = "Salut test prop";

        public ReactiveCommand<Unit, IRoutableViewModel> GoSettings { get; }

        public HomeViewModel(IScreen screen)
        {
            HostScreen = screen;

            GoSettings = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new SettingsViewModel(HostScreen))
            );
        }
    }
}
