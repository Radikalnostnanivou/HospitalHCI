using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZdravoCorp.View.Core
{
    public static class ThemesController
    {
        public enum ThemeTypes
        {
            Light,
            Dark
        }

        public static ThemeTypes CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }

        public static void SetTheme(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark:
                    themeName = "Dark";
                    Properties.Settings.Default.ColorMode = !Properties.Settings.Default.ColorMode;
                    Properties.Settings.Default.Save();
                    break;
                case ThemeTypes.Light:
                    themeName = "Light";
                    Properties.Settings.Default.ColorMode = !Properties.Settings.Default.ColorMode;
                    Properties.Settings.Default.Save();
                    break;
            }
            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"View/Styles/ManagerStyles{themeName}.xaml", UriKind.Relative));
            }
            catch
            {
            }
        }

        public static void SetThemeStartup(ThemeTypes theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeTypes.Dark:
                    themeName = "Dark";
                    break;
                case ThemeTypes.Light:
                    themeName = "Light";
                    break;
            }
            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"View/Styles/ManagerStyles{themeName}.xaml", UriKind.Relative));
            }
            catch
            {
            }
        }
    }
}
