using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Ressources;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModLivreVM modLivreVM;
        LoginVM loginVM;

        public MainWindow()
        {
            //reset key
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["apikey"] != null)
            {
                config.AppSettings.Settings["apikey"].Value = string.Empty;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

            ApiHelper.InitializeClient();

            modLivreVM = new ModLivreVM();
            loginVM = new LoginVM();

            InitializeComponent();
            this.DataContext = new MainWindowVM();

            modLivreV.DataContext = modLivreVM;
            loginV.DataContext = loginVM;

            loginVM.OnNotify += Notification;
            loginVM.OnLoginSuccess += SwitchToModLivreView;
            modLivreVM.OnNotify += Notification;
        }

        public void Notification(string text)
        {
            textBlockNotifs.Text = text;
        }

        public void SwitchToModLivreView()
        {
            loginV.Visibility = Visibility.Collapsed;
            modLivreV.Visibility = Visibility.Visible;
        }

        private void comboBoxLangue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = comboBoxLangue.SelectedItem as System.Windows.Controls.ComboBoxItem;

            if (selectedItem != null)
            {
                string? cultureCode = selectedItem.Tag.ToString();
                ResourceHelper.SetCulture(cultureCode);
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            modLivreV.UpdateUI();
            loginV.UpdateUI();
        }
    }
}
