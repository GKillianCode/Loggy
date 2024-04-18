using Loggy.Models;
using Loggy.Utils;
using Loggy.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Loggy.Services
{
    public class LogsService
    {
        public class LogsServices
        {
            public static int CountOccurrences(string path, string terme)
            {
                int count = 0;

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(terme))
                            count++;
                    }
                }

                return count;
            }

            public static LogViewModel ParseLogFile(string filePath)
            {
                List<LogItemViewModel> logItems = new List<LogItemViewModel>();
                int nbAlertDebug = 0;
                int nbAlertInfo = 0;
                int nbAlertWarning = 0;
                int nbAlertError = 0;

                foreach (string line in File.ReadLines(filePath))
                {
                    Match match = Regex.Match(line, @"^(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3}) - (\w+)\s+\[(.*?)\] - (.*)$");

                    if (match.Success)
                    {
                        string rawDate = match.Groups[1].Value;
                        string rawType = match.Groups[2].Value;
                        string rawService = match.Groups[3].Value;
                        string rawDescription = match.Groups[4].Value;

                        string type = "";

                        if (rawType.Equals(AlertModel.ALERT_DEBUG))
                        {
                            nbAlertDebug++;
                            type = AlertModel.ALERT_DEBUG;
                        }
                        else if (rawType.Equals(AlertModel.ALERT_INFO))
                        {
                            nbAlertInfo++;
                            type = AlertModel.ALERT_INFO;
                        }
                        else if (rawType.Equals(AlertModel.ALERT_WARN))
                        {
                            nbAlertWarning++;
                            type = AlertModel.ALERT_WARN;
                        }
                        else if (rawType.Equals(AlertModel.ALERT_ERROR))
                        {
                            nbAlertError++;
                            type = AlertModel.ALERT_ERROR;
                        }

                        DateTime date = DateUtils.convertStringDateToDateTime();
                        LogItemViewModel item = new LogItemViewModel(date, type, rawService, rawDescription);
                        logItems.Add(item);
                    }
                }


                return new LogViewModel(logItems, nbAlertDebug, nbAlertInfo, nbAlertWarning, nbAlertError);
            }
        }
    }
}
