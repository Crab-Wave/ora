﻿using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using ReactiveUI;

using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public HomeViewModel Home { get; }
        public SettingsViewModel Settings { get; }

        public MainWindowViewModel()
        {
            Content = Home = new HomeViewModel(new ClusterItem[] { });
            Settings = new SettingsViewModel();
        }

        public void NavigateToHome()
        {
            Content = Home;
        }

        public void NavigateToSettings()
        {
            Content = Settings;
        }

        public void AddClusterItem()
        {
            var vm = new AddClusterItemViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (ClusterItem) null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        Home.Clusters.Add(model);
                    }

                    Content = Home;
                });

            Content = vm;
        }
    }
}
