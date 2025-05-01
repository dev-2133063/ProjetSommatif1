using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.ApiModels;

namespace WpfApp1.Model
{
    public class Livre
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Titre { get; set; }
        public int NbPages { get; set; }
        public int AuteurId { get; set; }
        public int CategorieId { get; set; }
        public Auteur Auteur { get; set; }
        public Livre(LivreAPI livre)
        {
            Id = livre.id;
            Isbn = livre.isbn;
            Titre = livre.titre;
            NbPages = livre.nbPages;
            AuteurId = livre.auteurId;
            CategorieId = livre.categorieId;
        }

    }
}
