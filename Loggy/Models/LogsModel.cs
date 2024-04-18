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

        public string getPath() { return _path; }
        public string getContent() { return _content; }

        public LogViewModel getLogs() { return logs; }

        public void setPath(string path) { this._path = path; }
        public void setContent(string content)
        {
            this._content = content;
            AnalyseContent();
        }

        public int getNbAlertDebug() { return _nbAlertDebug; }
        public int getNbAlertInfo() { return _nbAlertInfo; }
        public int getNbAlertAlert() { return _nbAlertWarning; }
        public int getNbAlertError() { return _nbAlertError; }
    }
}
