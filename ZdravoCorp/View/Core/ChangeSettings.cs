using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.View.Core
{
    public class ChangeSettings
    {
        public static void ChangeLanguage(string currLang)
        {
            if (currLang.Equals("sr"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr");
                Properties.Settings.Default.Language = currLang;
                Properties.Settings.Default.Save();
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en");
                Properties.Settings.Default.Language = currLang;
                Properties.Settings.Default.Save();
            }
        }
    }
}
