using Loggy.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Loggy.Pages
{
    /// <summary>
    /// Logique d'interaction pour LogsPage.xaml
    /// </summary>
    public partial class LogsPage : Page
    {
        public ObservableCollection<LogItemViewModel> ListLog { get; set; }
        private LogViewModel logViewModel;
        private int nbItemsPerPage = 100;
        private int currentPage = 1;
        private int nbPage = 0;

        public LogsPage(LogViewModel logViewModel)
        {
            InitializeComponent();
            DataContext = this;

            this.logViewModel = logViewModel;
            this.nbPage = CountNbPage();
            this.ListLog = GetLogsByPage(currentPage);
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
                Console.WriteLine($"{i} {logViewModel.LogItems[i].Processus} {logViewModel.LogItems[i].Description}");
                pageItems.Add(logViewModel.LogItems[i]);
            }

            return pageItems;
        }


    }
}
