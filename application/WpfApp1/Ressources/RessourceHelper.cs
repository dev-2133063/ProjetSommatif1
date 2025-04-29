using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Ressources
{
    class ResourceHelper
    {
        private static readonly List<string> availableLanguages = new List<string> { "fr", "en" };

        public static bool IsAvailableLanguage(string lang)
        {
            return availableLanguages.Where(l => l.Equals(lang)).FirstOrDefault() != null ? true : false;
        }

        public static string GetCurrentLanguage()
        {
            return Ressources.Culture.Name;
        }

        public static string GetDefaultLanguage()
        {
            return availableLanguages[0];
        }

        public static void SetInitialLanguage()
        {
            string lang = ConfigurationManager.AppSettings["lang"];

            if (!IsAvailableLanguage(lang))
                lang = GetDefaultLanguage();

            Ressources.Culture = new CultureInfo(lang);
        }

        public static void SetCulture(string lang)
        {
            if (!IsAvailableLanguage(lang))
                lang = GetDefaultLanguage();

            Ressources.Culture = new CultureInfo(lang);
        }
    }
}
