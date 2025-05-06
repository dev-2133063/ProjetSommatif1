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
using System.Net.Http;

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
                OnNotify?.Invoke("ERR : " + Ressources.Ressources.erreur_champsVides);
                return;
            }
            if (Isbn == null || Isbn == "" || Titre == null || Titre == "")
            {
                OnNotify?.Invoke("ERR : " + Ressources.Ressources.erreur_champsVides);
                return;
            }
            if (NbPages < 0)
            {
                OnNotify?.Invoke("ERR : " + Ressources.Ressources.erreur_badRequest);
                return;
            }

            //effectuer changements
            LivreSelectionne.Isbn = Isbn.Trim();
            LivreSelectionne.Titre = Titre.Trim();
            LivreSelectionne.NbPages = NbPages;
            LivreSelectionne.Auteur = AuteurSelectionne;
            LivreSelectionne.AuteurId = AuteurSelectionne.Id;

            HttpResponseMessage? response;

            //effectuer appel
            try
            {
                response = await ApiProcessor.ModifierLivre(LivreSelectionne);
            }
            catch (Exception ex)
            {
                //message Ressources
                OnNotify?.Invoke("ERR : " + ex.Message);
                return;
            }

            if (response == null)
            {
                OnNotify?.Invoke("ERR : " + Ressources.Ressources.erreur_serveur);
            }
            else if (response.IsSuccessStatusCode)
            {
                //si ça marche
                OnNotify?.Invoke(Ressources.Ressources.msg_modifReussi);

                //reset
                LivreSelectionne = null;
                GetAllInformation();
            }
            else
            {
                //erreur
                OnNotify?.Invoke("ERR : " + await response.Content.ReadAsStringAsync());
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
