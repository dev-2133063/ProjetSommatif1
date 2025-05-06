using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.ApiModels;

namespace WpfApp1.Model
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Auteur() { }
        public Auteur(AuteurAPI auteur)
        {
            Id = auteur.id;
            Nom = auteur.nom;
        }
    }
}
