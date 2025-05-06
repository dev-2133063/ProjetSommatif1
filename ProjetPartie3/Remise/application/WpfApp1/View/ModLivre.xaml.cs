using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModel;
using WpfApp1.Ressources;

namespace WpfApp1.View
{
    /// <summary>
    /// Logique d'interaction pour ModLivre.xaml
    /// </summary>
    public partial class ModLivre : UserControl
    {
        public ModLivre()
        {
            InitializeComponent();
            UpdateUI();
        }

        public void UpdateUI()
        {
            labelAuteur.Content = Ressources.Ressources.labelAuteur;
            labelIsbn.Content = Ressources.Ressources.labelIsbn;
            labelNbPages.Content = Ressources.Ressources.labelNbPages;
            labelTitre.Content = Ressources.Ressources.labelTitre;
            groupboxInfos.Header = Ressources.Ressources.groupboxInfos;
            groupboxLivres.Header = Ressources.Ressources.groupboxLivres;
            btnEnregistrer.Content = Ressources.Ressources.btnEnregistrer;
            listColNom.Header = Ressources.Ressources.listHeader_nom;
            listCol_livreIsbn.Header = Ressources.Ressources.listHeader_isbn;
            listCol_livreTitre.Header = Ressources.Ressources.listHeader_titre;
        }
    }
}
