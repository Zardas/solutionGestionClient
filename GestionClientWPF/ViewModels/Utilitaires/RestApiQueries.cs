using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GestionClientWPF.Models;
using System.Diagnostics;

namespace GestionClientWPF.ViewModels
{
    class RestApiQueries
    {
        private HttpClient _client;

        private string Token;

        public RestApiQueries(string token)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Setting Authorization.  
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            Token = token;
        }

        private async Task<List<Client>> GetClientsAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(data);
                return clients;
            }

            return new List<Client>();
        }

        public class ResultConnexion
        {
            public string Token;
            public string Type;
            public int Id;
        }

        private async Task<ResultConnexion> GetUtilisateurPourConnexionAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                ResultConnexion utilisateur = JsonConvert.DeserializeObject<ResultConnexion>(data);
                return utilisateur;
            }

            return null;
        }



        private async Task<bool> AddClientAsync(string path, Client client)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }


        /* Methode générique */
        private async Task<bool> RemoveAsync(string path)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path);

            return response.IsSuccessStatusCode;
        }











        // Actuals methods
        public List<Client> GetClients(string path)
        {
            List<Client> clients = new List<Client>();

            try
            {
                Task<List<Client>> task = Task.Run(async () => await GetClientsAsync(path));
                task.Wait();
                clients = task.Result;
            }
            catch (Exception) { }

            return clients;
        }

        // Pour la connexion
        public ResultConnexion GetUtilisateurPourConnexion(string path)
        {
            ResultConnexion utilisateur = new ResultConnexion();

            try
            {
                Task<ResultConnexion> task = Task.Run(async () => await GetUtilisateurPourConnexionAsync(path));
                task.Wait();
                utilisateur = task.Result;
            }
            catch (Exception) { }

            return utilisateur;
        }


        public void AddClient(string path, Client client)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddClientAsync(path, client));
                task.Wait();
            }
            catch (Exception) { }

        }


        // Suppression d'un élément
        public void Remove(string path)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await RemoveAsync(path));
                task.Wait();
            }
            catch (Exception) { }

        }
    }
}
