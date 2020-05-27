using System.Reactive;
using ReactiveUI;
using System;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    internal class AddMemberViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }


        private string identifier;
        public string Identifier
        {
            get => this.identifier;
            set => this.RaiseAndSetIfChanged(ref this.identifier, value);
        }

        public ReactiveCommand<Unit, bool> Invite { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public AddMemberViewModel(string clusterIdentifier)
        {
            var inviteEnabled = this.WhenAnyValue(
                x => x.Identifier,
                x => !String.IsNullOrWhiteSpace(x)
            );

            this.Invite = ReactiveCommand.Create(
                () => Ora.GetClusterManager().InviteMember(clusterIdentifier, this.Identifier),
                inviteEnabled);

            this.Cancel = ReactiveCommand.Create(() => { });
        }
    }
}
