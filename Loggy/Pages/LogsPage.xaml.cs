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
        private int nbItemsPerPage = 10;
        private int currentPage = 1;

        public LogsPage(LogViewModel logViewModel)
        {
            InitializeComponent();
            DataContext = this;

            this.logViewModel = logViewModel;
            this.ListLog = GetLogsByPage(currentPage);

            Console.WriteLine(ListLog.Count);
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
            int startIndex = (page - 1) * nbItemsPerPage;
            int endIndex = Math.Min(startIndex + nbItemsPerPage - 1, logViewModel.LogItems.Count - 1);

            Console.WriteLine("Start : " + startIndex);
            Console.WriteLine("End : " + endIndex);

            ObservableCollection<LogItemViewModel> pageItems = new ObservableCollection<LogItemViewModel>();

            for (int i = startIndex; i <= endIndex; i++)
            {
                pageItems.Add(logViewModel.LogItems[i]);
            }

            return pageItems;
        }

        public void NextPage()
        {
            if (currentPage < CountNbPage())
            {
                currentPage++;
                ListLog = GetLogsByPage(currentPage);
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                ListLog = GetLogsByPage(currentPage);
            }
        }
    }
}
