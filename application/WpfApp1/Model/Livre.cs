using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Categorie Categorie { get; set; }
        public Livre(int id, string isbn, string titre, int nbPages, int auteurId, int categorieId)
        {
            Id = id;
            Isbn = isbn;
            Titre = titre;
            NbPages = nbPages;
            AuteurId = auteurId;
            CategorieId = categorieId;
        }

    }
}
