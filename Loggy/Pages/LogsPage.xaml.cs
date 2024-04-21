using Loggy.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Loggy.Pages
{
    /// <summary>
    /// Logique d'interaction pour LogsPage.xaml
    /// </summary>
    public partial class LogsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<LogItemViewModel> _listLog;
        private LogViewModel logViewModel;
        private int nbItemsPerPage = 100;
        private int _currentPage;
        private int _nbPage = 0;

        public LogsPage(LogViewModel logViewModel)
        {
            InitializeComponent();
            DataContext = this;

            this.logViewModel = logViewModel;
            NbPage = CountNbPage();

            CurrentPage = 1;
            ListLog = GetLogsByPage(CurrentPage);
        }

        private int CountNbPage()
        {
            int nbPage = logViewModel.LogItems.Count / nbItemsPerPage;
            float restNbPage = logViewModel.LogItems.Count % nbItemsPerPage;

            if (restNbPage != 0)
                nbPage++;

            return nbPage;
        }

        private ObservableCollection<LogItemViewModel> GetLogsByPage(int page)
        {
            int startIndex = (logViewModel.LogItems.Count - 1) - (nbItemsPerPage * (page - 1));
            int endIndex = startIndex - nbItemsPerPage;

            Console.WriteLine($"{startIndex} {endIndex}");
            Console.WriteLine($"{logViewModel.LogItems[0].Processus} {logViewModel.LogItems[0].Description}");
            Console.WriteLine($"{logViewModel.LogItems[1998].Processus} {logViewModel.LogItems[1998].Description}");

            ObservableCollection<LogItemViewModel> pageItems = new ObservableCollection<LogItemViewModel>();

            for (int i = startIndex; i >= endIndex; i--)
            {
                try
                {
                    Console.WriteLine($"{i} {logViewModel.LogItems[i].Processus} {logViewModel.LogItems[i].Description}");
                    pageItems.Add(logViewModel.LogItems[i]);
                }
                catch
                {
                    break;
                }
            }

            return pageItems;
        }

        private void GoToPreviousPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine(CurrentPage);
            if (CurrentPage > 1)
            {
                CurrentPage = CurrentPage - 1;
                Console.WriteLine(CurrentPage);
                ListLog = GetLogsByPage(CurrentPage);
            }
        }

        private void GoToNextPage(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine(CurrentPage);
            if (CurrentPage < NbPage)
            {
                CurrentPage = CurrentPage + 1;
                ListLog = GetLogsByPage(CurrentPage);
            }
        }

        public ObservableCollection<LogItemViewModel> ListLog
        {
            get { return _listLog; }
            set
            {
                if (_listLog != value)
                {
                    _listLog = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NbPage
        {
            get { return _nbPage; }
            set
            {
                if (_nbPage != value)
                {
                    _nbPage = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
