using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Auteur(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
    }
}
