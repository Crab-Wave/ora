using System.Linq;
using ORA.API;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Models
{
    public class MemberItem
    {
        private Cluster cluster;

        public Member member { get; set; }

        public string MemberInformation { get => $"{this.member.Name} {this.member.Identifier}"; }

        private MainWindowViewModel MainWindowViewModel;

        public MemberItem(MainWindowViewModel mainWindowViewModel, Cluster cluster, Member member)
        {
            this.cluster = cluster;
            this.member = member;
            this.MainWindowViewModel = mainWindowViewModel;
        }

        public void MemberRemove()
        {
            Ora.GetClusterManager().RemoveMember(this.cluster.Identifier, this.member.Identifier);
            this.MainWindowViewModel.Content = this.MainWindowViewModel.Cluster = new ClusterViewModel(Ora.GetClusterManager().GetMembers(this.cluster.Identifier).Select(c => new MemberItem(this.MainWindowViewModel, this.cluster, c)));
        }
    }
}
