using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GestionClientWPF.Models;

namespace GestionClientWPF.ViewModels
{
    class RestApiQueries
    {
        private HttpClient _client;

        public RestApiQueries()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Client>> GetClientsAsync(string path)
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
            public int Id;
            public string Type;
        }

        public async Task<ResultConnexion> GetUtilisateurPourConnexionAsync(string path)
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
    }
}
