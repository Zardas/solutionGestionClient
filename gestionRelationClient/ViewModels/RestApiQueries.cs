using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using gestionRelationClient.Models;

namespace gestionRelationClient.ViewModels
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

            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(data);
                return clients;
            }

            return new List<Client>();
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
            } catch(Exception) { }

            return clients;
        }

    }
}
