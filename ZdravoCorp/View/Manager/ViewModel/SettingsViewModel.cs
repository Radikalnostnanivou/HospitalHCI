using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using static ZdravoCorp.View.Manager.ViewModel.SettingsViewModel;

namespace ZdravoCorp.View.Manager.ViewModel
{
    public class Language
    {
        public Language(Languages language)
        {
            this.Languages = language;
            this.LanguageName = language.ToString();
        }

        public Languages Languages { get; set; }
        public String LanguageName { get; set; }
    }

    public class Theme
    {
        public Theme(Themes theme)
        {
            this.Themes = theme;
            this.ThemesName = theme.ToString();
        }

        public Themes Themes { get; set; }
        public String ThemesName { get; set; }
    }

    public class SettingsViewModel : ObservableObject, ViewModelInterface
    {
        public enum Languages
        {
            English,
            Serbian
        }

        public enum Themes
        {
            Dark,
            Light
        }

        private ObservableCollection<Language> languages;
        private ObservableCollection<Theme> themes;
        private Language language;
        private Theme theme;

        public RelayCommand LogoutCommand { get; set; }

        public UserControl CurrentView
        {
            get => ContentViewModel.Instance.CurrentView;
            set
            {
                if (value != ContentViewModel.Instance.CurrentView)
                {
                    ContentViewModel.Instance.WindowBrowser.AddWindow(value);
                    ContentViewModel.Instance.CurrentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Language> Languages1
        {
            get => languages;
            set
            {
                if (value != languages)
                {
                    languages = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Theme> Themes1
        {
            get => themes;
            set
            {
                if (value != themes)
                {
                    themes = value;
                    OnPropertyChanged();
                }
            }
        }
        public Language Language
        {
            get => language;
            set
            {
                if (value != language)
                {
                    var app = (App)Application.Current;
                    language = value;
                    if (language.Languages == Languages.English)
                    {
                        app.ChangeLanguage("en");
                    }
                    else if (language.Languages == Languages.Serbian)
                    {
                        app.ChangeLanguage("sr");
                    }
                    ContentViewModel.Instance.Title = GetTitle();
                    OnPropertyChanged();
                }
            }
        }
        public Theme Theme
        {
            get => theme;
            set
            {
                if (value != theme)
                {
                    
                    theme = value;
                    if(theme.Themes == Themes.Dark)
                    {
                        ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
                    } 
                    else if (theme.Themes == Themes.Light)
                    {
                        ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                    }
                    OnPropertyChanged();
                }
            }
        }

        public SettingsViewModel()
        {
            LogoutCommand = new RelayCommand(o =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Manager.Instance.Close();
            });

            Languages1 = new ObservableCollection<Language>() { new Language(Languages.English), new Language(Languages.Serbian) };
            Themes1 = new ObservableCollection<Theme>() { new Theme(Themes.Dark), new Theme(Themes.Light) };
            if(Properties.Settings.Default.Language.Equals("en"))
            {
                language = Languages1[0];
            } else if(Properties.Settings.Default.Language.Equals("sr"))
            {
                language= Languages1[1];
            }
            if (Properties.Settings.Default.ColorMode)
            {
                theme = Themes1[1];
            } else
            {
                theme = Themes1[0];
            }
        }

        public string GetTitle()
        {
            return "Settings";
        }

        public void Update()
        {

        }
    }

    

}
