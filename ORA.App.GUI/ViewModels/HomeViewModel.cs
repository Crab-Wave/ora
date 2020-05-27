using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<ClusterItem> Clusters { get; }

        public HomeViewModel(IEnumerable<ClusterItem> clusters)
        {
            this.Clusters = new ObservableCollection<ClusterItem>(clusters);
        }
    }
}
