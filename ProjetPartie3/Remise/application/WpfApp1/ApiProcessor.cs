using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WpfApp1.Model;
using System.Net.Http.Json;
using WpfApp1.Model.ApiModels;
using System.Windows.Shapes;
using WpfApp1.View;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1
{
    public class ApiProcessor
    {
        public static async Task<string>? Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return "";

            string url = $"Login/{username}/{password}";
            using (Task<HttpResponseMessage> response = ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    Task<string> result = response.Result.Content.ReadAsStringAsync();
                    return await result;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public static async Task<HttpResponseMessage>? ModifierLivre(Livre livre)
        {
            if (livre is null || livre.Id <= 0) return null;

            string url = $"api/Livre/{livre.Id}";
            string json = JsonSerializer.Serialize(livre);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync(url, content);
            return response; // return the full response so caller can inspect
        }

        public static async Task<List<LivreAPI>> GetAllLivres()
        {
            string url = $"api/Livre";
            using (Task<HttpResponseMessage> response = ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    Task<List<LivreAPI>> results = response.Result.Content.ReadFromJsonAsync<List<LivreAPI>>();
                    return await results;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public static async Task<List<AuteurAPI>> GetAllAuteurs()
        {
            string url = $"api/Auteur";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<List<AuteurAPI>> results = response.Content.ReadFromJsonAsync<List<AuteurAPI>>();
                    return await results;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<Livre>> ConvertLivres(List<LivreAPI> livresAPI)
        {
            List<Livre> newLivres = new List<Livre>();

            foreach (LivreAPI livre in livresAPI)
            {
                Auteur auteur = new Auteur(livre.auteur);
                Categorie categorie = new Categorie(livre.categorie);

                newLivres.Add(new Livre(livre, auteur, categorie));
            }

            return newLivres;
        }

        public static async Task<List<Auteur>> ConvertAuteurs(List<AuteurAPI> auteursAPI)
        {
            List<Auteur> newAuteurs = new List<Auteur>();

            foreach (AuteurAPI auteur in auteursAPI)
            {
                newAuteurs.Add(new Auteur(auteur));
            }

            return newAuteurs;
        }
    }
}
