using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.ApiModels;

namespace WpfApp1.Model
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public Categorie() { }

        public Categorie(CategorieAPI categorie)
        {
            Id = categorie.id;
            Nom = categorie.nom;
        }
    }
}
