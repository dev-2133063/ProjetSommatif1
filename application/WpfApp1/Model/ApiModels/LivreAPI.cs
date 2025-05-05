using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp1.Model.ApiModels
{
    public class LivreAPI
    {
        //Results
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "isbn")]
        public string isbn { get; set; }

        [JsonProperty(PropertyName = "titre")]
        public string titre { get; set; }

        [JsonProperty(PropertyName = "nbPages")]
        public int nbPages { get; set; }

        [JsonProperty(PropertyName = "auteurId")]
        public int auteurId { get; set; }

        [JsonProperty(PropertyName = "categorieId")]
        public int categorieId { get; set; }

        [JsonProperty(PropertyName = "auteur")]
        public AuteurAPI auteur { get; set; }

        [JsonProperty(PropertyName = "categorie")]
        public CategorieAPI categorie { get; set; }
    }
}
