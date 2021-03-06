﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Library;

namespace Interfaces
{
    public interface IMainWindowViewModel
    {
        ICommand ClockInCommand { get; set; }
        ICommand ClockOutCommand { get; set; }
        ICommand ChangeTaskCommand { get; set; }
        ICommand ReportsCommand { get; set; }
        ICommand CloseCommand { get; set; }
        ICommand SettingsCommand { get; set; }
        string Description { get; set; }
        ObservableCollection<TimeEntry> TimeEntries { get; set; }
        string TotalElapsed { get; set; }
        ICommand MinimizeCommand { get; set; }
        void Save();
        Settings Settings { get; set; }
    }
}