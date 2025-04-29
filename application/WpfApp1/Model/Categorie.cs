using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Categorie(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}
