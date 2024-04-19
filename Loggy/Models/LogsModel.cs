using Loggy.ViewModels;
using static Loggy.Services.LogsService;

namespace Loggy.Models
{
    public class LogsModel
    {
        public static readonly string LOG_DATETIME_ALERT_SEPARATOR = "- ";

        string _path = "Path: /";
        string _content = "";

        int _nbAlertDebug = 0;
        int _nbAlertInfo = 0;
        int _nbAlertWarning = 0;
        int _nbAlertError = 0;
        LogViewModel logs = new LogViewModel();

        public LogsModel() { }

        public LogsModel(string path, string content)
        {
            this._path = path;
            this._content = content;
        }

        private void reset()
        {
            _nbAlertDebug = 0;
            _nbAlertInfo = 0;
            _nbAlertWarning = 0;
            _nbAlertError = 0;
        }

        private void AnalyseContent()
        {
            if (_content != "")
            {
                reset();

                logs = LogsServices.ParseLogFile(_path);
                _nbAlertDebug = logs.NbAlertDebug;
                _nbAlertInfo = logs.NbAlertInfo;
                _nbAlertWarning = logs.NbAlertWarning;
                _nbAlertError = logs.NbAlertError;
            }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                AnalyseContent();
            }
        }
        public LogViewModel Log { get { return logs; } }

        public int NbAlertDebug { get { return _nbAlertDebug; } }
        public int NbAlertInfo { get { return _nbAlertInfo; } }
        public int NbAlertAlert { get { return _nbAlertWarning; } }
        public int NbAlertError { get { return _nbAlertError; } }
    }
}
