﻿using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Library;

namespace MicroManager.ViewModels
{
    public class ReportsDesignTimeViewModel : IReportsViewModel
    {
        public ICommand ChooseFilesCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public Settings Settings { get; set; }
        public string ReportText { get; set; }

        public ReportsDesignTimeViewModel()
        {
            ChooseFilesCommand = new RelayCommand(() => { });
            PrintReportCommand = new RelayCommand(() => { });
            CloseCommand = new RelayCommand(() => { });
            ReportText = "Your report here.";
        }
    }
}
