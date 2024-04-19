using Loggy.Models;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Loggy.Pages
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private LogsModel log = new LogsModel();
        private string _filePath;

        public HomePage()
        {
            InitializeComponent();
            DataContext = this;
            FilePath = "Path: /";
        }

        private void LoadFileOnclick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadLogFile(sender, e);
            }
            catch
            {
                Console.WriteLine("Load File Error");
            }
        }

        private void LoadLogFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers de journal (*.log)|*.log";
            if (openFileDialog.ShowDialog() == true)
            {
                log.Path = openFileDialog.FileName;
                log.Content = File.ReadAllText(log.Path);
                FilePath = log.Path;
            }
        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("---- drag enter ----");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void dragOver(object sender, DragEventArgs e)
        {
            Console.WriteLine("---- drag over ----");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                dropZone.Stroke = (Brush)FindResource("scbYellow");
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void dragLeave(object sender, DragEventArgs e)
        {
            dropZone.Stroke = (Brush)FindResource("scbBorderColor");
        }

        private void dragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("---- drag drop ----");

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                dropZone.Stroke = (Brush)FindResource("scbBorderColor");
                string file = files[0];

                if (Path.GetExtension(file).Equals(".log", StringComparison.OrdinalIgnoreCase))
                {
                    log.Path = file;
                    log.Content = File.ReadAllText(log.Path);
                    FilePath = log.Path;
                }
                else
                {
                    MessageBox.Show("Veuillez déposer uniquement des fichiers .log", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value != _filePath)
                {
                    _filePath = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
