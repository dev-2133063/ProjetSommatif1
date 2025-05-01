using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public partial class ModLivreVM: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Livre> _livres;

        [ObservableProperty]
        private ObservableCollection<Auteur> _auteurs;

        [ObservableProperty]
        private string _isbn, _titre;

        [ObservableProperty]
        private int _nbPages;

        [ObservableProperty]
        private Auteur? _auteurSelectionne;

        [ObservableProperty]
        private Livre? _livreSelectionne;

        public ModLivreVM()
        {
            GetAllInformation();
        }


        [RelayCommand]
        private async void EnregistrerMod()
        {
            //securite
            if (LivreSelectionne == null || AuteurSelectionne == null) return;

            //restrictions
            if (string.IsNullOrEmpty(Isbn.Trim())) return;
            if (string.IsNullOrEmpty(Titre.Trim())) return;
            if (NbPages > 0) return;

            //effectuer changements
            LivreSelectionne.Isbn = Isbn.Trim();
            LivreSelectionne.Titre = Isbn.Trim();
            LivreSelectionne.NbPages = NbPages;
            LivreSelectionne.Auteur = AuteurSelectionne;
            LivreSelectionne.AuteurId = AuteurSelectionne.Id;

            //todo
            //string result = await ApiProcessor.ModifierLivre(LivreSelectionne);
            //if (result == WORKED) reset
            LivreSelectionne = null;

            //else dont reset
        }

        partial void OnLivreSelectionneChanged(Livre? livre)
        {
            if (livre == null)
            {
                Isbn = "";
                Titre = "";
                NbPages = 0;
                AuteurSelectionne = null;
            }
            else
            {
                Isbn = livre.Isbn;
                Titre = livre.Titre;
                NbPages = livre.NbPages;
                AuteurSelectionne = livre.Auteur;
            }
        }

        private async void GetAllInformation()
        {
            Livres = new ObservableCollection<Livre>(await ApiProcessor.ConvertLivres(await ApiProcessor.GetAllLivres()));
            Auteurs = new ObservableCollection<Auteur>(await ApiProcessor.ConvertAuteurs(await ApiProcessor.GetAllAuteurs()));
        }

    }
}
