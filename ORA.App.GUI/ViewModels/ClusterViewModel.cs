﻿using System;
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


        public ClusterViewModel(Cluster cluster)
        {
            this.Cluster = cluster;
            // this.Members = new ObservableCollection<MemberItem>(members);
        }
    }
}
