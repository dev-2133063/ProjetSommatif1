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
    public partial class ModLivreVM : ObservableObject
    {
        public event Action<string>? OnNotify;
        [ObservableProperty]
        private ObservableCollection<Livre>? _livres;

        [ObservableProperty]
        private ObservableCollection<Auteur>? _auteurs;

        [ObservableProperty]
        private string? _isbn, _titre;

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
        private async Task EnregistrerMod()
        {
            //securite
            if (LivreSelectionne == null || AuteurSelectionne == null)
            {
                //messaage
                return;
            }
            if (Isbn == null || Isbn == "" || Titre == null || Titre == "")
            {
                //Message
                return;
            }
            if (NbPages < 0)
            {
                //Message
                return;
            }

            //effectuer changements
            LivreSelectionne.Isbn = Isbn.Trim();
            LivreSelectionne.Titre = Titre.Trim();
            LivreSelectionne.NbPages = NbPages;
            LivreSelectionne.Auteur = AuteurSelectionne;
            LivreSelectionne.AuteurId = AuteurSelectionne.Id;

            //todo
            if (await ApiProcessor.ModifierLivre(LivreSelectionne))
            {
                OnNotify?.Invoke(Ressources.Ressources.msg_modifReussi);

                //reset
                LivreSelectionne = null;
                GetAllInformation();
            }

        }

        partial void OnLivreSelectionneChanged(Livre? value)
        {
            if (value == null)
            {
                Isbn = "";
                Titre = "";
                NbPages = 0;
                AuteurSelectionne = null;
            }
            else
            {
                Isbn = value.Isbn;
                Titre = value.Titre;
                NbPages = value.NbPages;
                AuteurSelectionne = value.Auteur;
            }
        }

        private async void GetAllInformation()
        {
            Livres = new ObservableCollection<Livre>(await ApiProcessor.ConvertLivres(await ApiProcessor.GetAllLivres()));
            Auteurs = new ObservableCollection<Auteur>(await ApiProcessor.ConvertAuteurs(await ApiProcessor.GetAllAuteurs()));
        }

    }
}
