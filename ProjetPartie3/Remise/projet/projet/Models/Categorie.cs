namespace projet.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Categorie() { }
        public Categorie(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }

    }
}
