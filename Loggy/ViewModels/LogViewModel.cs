using System.Collections.Generic;

namespace Loggy.ViewModels
{
    public class LogViewModel
    {
        List<LogItemViewModel> logItems = new List<LogItemViewModel>();
        int nbAlertDebug = 0;
        int nbAlertInfo = 0;
        int nbAlertWarning = 0;
        int nbAlertError = 0;

        public LogViewModel() { }
        public LogViewModel(List<LogItemViewModel> logItemViewModels, int nbAlertDebug, int nbAlertInfo, int nbAlertWarn, int nbAlertError)
        {
            this.logItems = logItemViewModels;
            this.nbAlertDebug = nbAlertDebug;
            this.nbAlertInfo = nbAlertInfo;
            this.nbAlertWarning = nbAlertWarn;
            this.nbAlertError = nbAlertError;
        }

        public List<LogItemViewModel> LogItems { get { return logItems; } }
        public int NbAlertDebug { get { return nbAlertDebug; } }
        public int NbAlertInfo { get { return nbAlertInfo; } }
        public int NbAlertWarning { get { return nbAlertWarning; } }
        public int NbAlertError { get { return nbAlertError; } }

    }
}
