using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

using ORA.API;

namespace ORA.App.GUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Cluster> Clusters { get; }

        public HomeViewModel(IEnumerable<Cluster> clusters)
        {
            Clusters = new ObservableCollection<Cluster>(clusters);
        }
    }
}
