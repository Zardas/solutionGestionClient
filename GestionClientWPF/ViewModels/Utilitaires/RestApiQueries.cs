using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GestionClientWPF.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            Token = token;
            Debug.WriteLine("Token mis dasn l'header : " + Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            Token = token;
        }


        /* Methode générique */
        private async Task<bool> RemoveAsync(string path)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path);

            return response.IsSuccessStatusCode;
        }



        /* Getters basiques */

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
        private async Task<List<Gestionnaire>> GetGestionnairesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Gestionnaire> gestionnaires = JsonConvert.DeserializeObject<List<Gestionnaire>>(data);
                return gestionnaires;
            }

            return new List<Gestionnaire>();
        }
        private async Task<List<Role>> GetRolesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(data);
                return roles;
            }

            return new List<Role>();
        }




        /* Getter spéciale pour la connexion */

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




        /* Client */

        private async Task<bool> AddClientAsync(string path, Client client)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ModifierClientAsync(string path, Client client)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }


        



        /* Gestionnaire */

        private async Task<bool> AddGestionnaireAsync(string path, Gestionnaire gestionnaire)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(gestionnaire), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ModifierGestionnaireAsync(string path, Gestionnaire gestionnaire)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(gestionnaire), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }




        /* Role */
        private async Task<bool> AddRoleAsync(string path, Role role)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }





























        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/








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
        public List<Gestionnaire> GetGestionnaires(string path)
        {
            List<Gestionnaire> gestionnaires = new List<Gestionnaire>();

            try
            {
                Task<List<Gestionnaire>> task = Task.Run(async () => await GetGestionnairesAsync(path));
                task.Wait();
                gestionnaires = task.Result;
            }
            catch (Exception) { }

            return gestionnaires;
        }
        public List<Role> GetRoles(string path)
        {
            List<Role> role = new List<Role>();

            try
            {
                Task<List<Role>> task = Task.Run(async () => await GetRolesAsync(path));
                task.Wait();
                role = task.Result;
            }
            catch (Exception) { }

            return role;
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




        /* Client */

        public void AddClient(string path, Client client)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddClientAsync(path, client));
                task.Wait();
            }
            catch (Exception) { }

        }

        // Modification d'un client
        public void ModifierClient(string path, Client client)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierClientAsync(path, client));
                task.Wait();
            }
            catch (Exception) { }

        }


        /* Gestionnaire */

        public void AddGestionnaire(string path, Gestionnaire gestionnaire)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddGestionnaireAsync(path, gestionnaire));
                task.Wait();
            }
            catch (Exception) { }

        }

        // Modification d'un cligestionnaireent
        public void ModifierGestionnaire(string path, Gestionnaire gestionnaire)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierGestionnaireAsync(path, gestionnaire));
                task.Wait();
            }
            catch (Exception) { }

        }




        /* Role */
        public void AddRole(string path, Role role)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddRoleAsync(path, role));
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
