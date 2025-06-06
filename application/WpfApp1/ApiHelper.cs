﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WpfApp1
{
    internal class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static void InitializeClient()
        {
            //initialisation du httpClient
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://localhost:7088/");

            //Sert à préciser le wanted return type
            ApiClient.DefaultRequestHeaders.Accept.Clear();

            ApiClient.DefaultRequestHeaders.Add("X-API-Key", ConfigurationManager.AppSettings["apikey"]);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void SetApiKeyHeader(string apiKey)
        {
            if (ApiClient.DefaultRequestHeaders.Contains("X-API-Key"))
                ApiClient.DefaultRequestHeaders.Remove("X-API-Key");

            ApiClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        }
    }
}
