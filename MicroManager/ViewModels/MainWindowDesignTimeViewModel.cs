﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Library;

namespace MicroManager.ViewModels
{
    public class MainWindowDesignTimeViewModel : IMainWindowViewModel
    {
        public ICommand ClockInCommand { get; set; }
        public ICommand ClockOutCommand { get; set; }
        public ICommand ChangeTaskCommand { get; set; }
        public ICommand ReportsCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TimeEntry> TimeEntries { get; set; }
        public string TotalElapsed { get => "4 Hours 30 Minutes 20 Seconds";
            set{ }}
        public void Save() { }
        public Settings Settings { get; set; }

        public MainWindowDesignTimeViewModel()
        {
            ClockInCommand = new RelayCommand(() => { });
            ClockOutCommand = new RelayCommand(() => { });
            CloseCommand = new RelayCommand(() => { });
            ChangeTaskCommand = new RelayCommand(() => { });
            ReportsCommand = new RelayCommand(() => { });
            MinimizeCommand = new RelayCommand(() => { });
            SettingsCommand = new RelayCommand(() => { });
            Description = string.Empty;
            TimeEntries = new ObservableCollection<TimeEntry>
            {
                new TimeEntry { Description = "Test1", Start = DateTime.Now, Stop = DateTime.Now.AddHours(2)},
                new TimeEntry { Description = "Test2", Start = DateTime.Now.AddHours(2), Stop = DateTime.Now.AddHours(4)},
            };
        }
    }
}
