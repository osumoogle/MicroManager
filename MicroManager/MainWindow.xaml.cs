﻿using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Library;
using MicroManager.ViewModels;

namespace MicroManager
{
    public partial class MainWindow
    {
        public IMainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;
            ViewModel.CloseCommand = new RelayCommand(() =>
            {
                ViewModel.Save();
                FileHelper.SaveSettings(ViewModel.Settings);
                Close();
            });
            ViewModel.MinimizeCommand = new RelayCommand(() =>
            {
                WindowState = WindowState.Minimized;
            });
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
