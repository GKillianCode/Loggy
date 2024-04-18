using Loggy.Models;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Loggy.Pages
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
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
                log.setPath(openFileDialog.FileName);
                log.setContent(File.ReadAllText(log.getPath()));

                FilePath = log.getPath();
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
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void dragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("---- drag drop ----");

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string file = files[0];

                if (Path.GetExtension(file).Equals(".log", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(file);
                    log.setPath(file);
                    log.setContent(File.ReadAllText(log.getPath()));

                    FilePath = log.getPath();

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
