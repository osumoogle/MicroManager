﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Library;
using Microsoft.Win32;
using PropertyChanged;
using Path = System.IO.Path;

namespace MicroManager.ViewModels
{
    [ImplementPropertyChanged]
    public class ReportsViewModel : IReportsViewModel
    {
        public ICommand ChooseFilesCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<string> Files { get; set; } 

        public ReportsViewModel()
        {
            ChooseFilesCommand = new RelayCommand(ChooseFiles);
            PrintReportCommand = new RelayCommand(PrintReport);
        }

        private void ChooseFiles()
        {
            Files = new ObservableCollection<string>();
            var dlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "(*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = FileHelper.GetPath()
            };
            dlg.ShowDialog();
            if (!dlg.FileNames.Any()) return;
            foreach (var file in dlg.FileNames)
            {
                Files.Add(file);
            }
        }

        private void PrintReport()
        {
            if (!Files.Any()) return;
            var fileHelper = new FileHelper();
            var printDialog = new PrintDialog();
            var result = printDialog.ShowDialog();
            if (!result.Value) return;

            var days = Files.Select(fileHelper.GetEntries).ToList();
            var lines = new List<string>();
            var runningTotal = new TimeSpan(0,0,0,0);
            foreach (var d in days)
            {
                lines.AddRange(d.Select(entry => entry.ToString()));
                var hours = d.Sum(te => te.Hours);
                var minutes = d.Sum(te => te.Minutes);
                var seconds = d.Sum(te => te.Seconds);
                var ts = new TimeSpan(0, hours, minutes, seconds);
                runningTotal = runningTotal.Add(ts);
                lines.Add(string.Format("---- Total Time For Day ---- {0}:{1}:{2} ---------------", ts.Hours, ts.Minutes,
                    ts.Seconds));
            }
            lines.Add(string.Format("---- Total Time For All Days Selected ---- {0}:{1}:{2} ---------------",
                runningTotal.Hours, runningTotal.Minutes,
                runningTotal.Seconds));

            var reportFile = string.Format("{0}-Report.txt", DateTime.Now.ToString("yyyy-MM-dd"));
            FileHelper.WriteFile(lines, reportFile);
            PrintReportFile(string.Format("{0}\\{1}", FileHelper.GetPath(), reportFile));
        }

        //Modified from source provided at http://stackoverflow.com/questions/18287153/printing-a-note-pad-text-file
        private static void PrintReportFile(string file)
        {
            var printFont = new Font("Arial", 10);
            using (var sr = new StreamReader(file))
            {
                var pd = new PrintDocument { DocumentName = Path.GetFileName(file) };
                pd.PrintPage += (sender, args) =>
                {
                    float linesPerPage = 0;
                    var count = 0;
                    float topMargin = args.MarginBounds.Top;
                    string line = null;

                    // Calculate the number of lines per page.
                    linesPerPage = args.MarginBounds.Height / printFont.GetHeight(args.Graphics);

                    // Print each line of the file. 
                    while (count < linesPerPage && ((line = sr.ReadLine()) != null))
                    {
                        var yPos = topMargin + (count * printFont.GetHeight(args.Graphics));
                        args.Graphics.DrawString(line, printFont, Brushes.Black, 10.0f, yPos, new StringFormat());
                        count++;
                    }

                    // If more lines exist, print another page. 
                    args.HasMorePages = line != null;
                };
                pd.Print();
            }            
        }
    }
}