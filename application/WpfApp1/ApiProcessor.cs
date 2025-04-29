using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WpfApp1.Model;

namespace WpfApp1
{
    public class ApiProcessor
    {
        public static async Task<IAsyncResult>? Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            string url = $"/Login/{username}/{password}";
            using (Task<HttpResponseMessage> response = ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    Task<IAsyncResult> result = response.Result.Content.ReadAsAsync<IAsyncResult>();
                    return result;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public static async Task<IAsyncResult>? ModifierLivre(Livre livre)
        {
            //todo
            if (livre is null || livre.Id <= 0) return null;

            string url = $"/api/Livre/{livre.Id}";
            using (Task<HttpResponseMessage> response = ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    Task<IAsyncResult> result = response.Result.Content.ReadAsAsync<IAsyncResult>();
                    return result;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }
    }
}
