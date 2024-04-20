using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Loggy.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Vérifiez si le contenu est une chaîne
            if (value is string content)
            {
                Console.WriteLine(content);

                // Comparez le contenu et retournez le style correspondant
                switch (content.ToLower())
                {
                    case "debug":
                        return Application.Current.FindResource("alertDebug");
                    case "info":
                        return Application.Current.FindResource("alertInfo");
                    case "alert":
                        return Application.Current.FindResource("alertAlert");
                    case "error":
                        return Application.Current.FindResource("alertError");
                    default:
                        return Application.Current.FindResource("alertDebug");
                }
            }

            // Retournez null si le contenu n'est pas une chaîne ou si aucun style correspondant n'est trouvé
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
