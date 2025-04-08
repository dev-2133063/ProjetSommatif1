using System.ComponentModel;

namespace projet.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string Titre { get; set; } = string.Empty;
        public int NbPages { get; set; }
        public int AuteurId { get; set; }
        public int CategorieId { get; set; }
        public Auteur Auteur { get; set; }
        public Categorie Categorie { get; set; }
        public Livre() { }
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
