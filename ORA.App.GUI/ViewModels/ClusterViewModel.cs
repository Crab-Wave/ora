using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class ClusterViewModel : ViewModelBase
    {
        public Cluster Cluster { get; }
        public ObservableCollection<MemberItem> Members { get; }
        public ObservableCollection<FileItem> Files { get; }

        public ClusterViewModel(Cluster cluster)
        {
            this.Cluster = cluster;
            this.Members = new ObservableCollection<MemberItem>(
                Ora.GetClusterManager().GetMembers(this.Cluster.Identifier)
                    .Select(member => new MemberItem(this, member)));
            this.Files = new ObservableCollection<FileItem>(
                Ora.GetFileManager().GetFiles(this.Cluster)
                    .Select(hash => new FileItem(this, Ora.GetFileManager().GetFile(cluster, hash))));
        }
    }
}
