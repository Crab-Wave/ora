﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class MemberViewModel : ViewModelBase
    {
        public ObservableCollection<MemberItem> Members { get; }

        public MemberViewModel(IEnumerable<MemberItem> members)
        {
            this.Members = new ObservableCollection<MemberItem>(members);
        }
    }
}
