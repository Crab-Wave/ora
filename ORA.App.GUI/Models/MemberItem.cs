using System.Linq;
using ORA.API;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Models
{
    public class MemberItem
    {
        public Member Member { get; }

        private ClusterViewModel clusterViewModel;

        public MemberItem(ClusterViewModel clusterViewModel, Member member)
        {
            this.clusterViewModel = clusterViewModel;
            this.Member = member;
        }

        public void MemberRemove()
        {
            // Ora.GetClusterManager().RemoveMember(this.cluster.Identifier, this.member.Identifier);
            // this.MainWindowViewModel.Content = this.MainWindowViewModel.Cluster = new ClusterViewModel(Ora.GetClusterManager().GetMembers(this.cluster.Identifier).Select(c => new MemberItem(this.MainWindowViewModel, this.cluster, c)));
        }
    }
}
