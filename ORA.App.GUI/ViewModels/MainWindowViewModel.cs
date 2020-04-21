using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using ReactiveUI;

namespace ORA.App.GUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoSettings { get; }

        public ReactiveCommand<Unit, Unit> GoBack { get; }

        public string UrlPathSegment { get; } = "Not working";

        public MainWindowViewModel()
        {
            Router = new RoutingState();

            var homeViewModel = new HomeViewModel(this);
            var settingsViewModel = new SettingsViewModel(this);
            // Router.Navigate.Execute(homeViewModel);

            GoNext = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new HomeViewModel(this))
            );

            GoSettings = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new SettingsViewModel(this))
            );

            GoBack = Router.NavigateBack;
        }
    }
}
