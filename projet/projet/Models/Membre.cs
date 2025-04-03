namespace projet.Models
{
    public class Membre
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public Membre() { }
        public Membre(int id, string nom, string courriel, string telephone, DateTime dateCreation, string apikey)
        {
            Id = id;
            Nom = nom;
            Courriel = courriel;
            Telephone = telephone;
            DateCreation = dateCreation;
            ApiKey = apikey;
        }
    }
}
