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
            Debug.WriteLine("Token mis dans l'header : " + Token);;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            Debug.WriteLine("FLAAA D : " + _client.DefaultRequestHeaders.Authorization);

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
        private async Task<List<Produit>> GetProduitsAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Produit> produits = JsonConvert.DeserializeObject<List<Produit>>(data);
                return produits;
            }

            return new List<Produit>();
        }
        private async Task<List<Service>> GetServicesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Service> services = JsonConvert.DeserializeObject<List<Service>>(data);
                return services;
            }

            return new List<Service>();
        }
        private async Task<List<Article>> GetArticlesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Article> articles = JsonConvert.DeserializeObject<List<Article>>(data);
                return articles;
            }

            return new List<Article>();
        }
        private async Task<List<Abonnement>> GetAbonnementsAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Abonnement> abonnements = JsonConvert.DeserializeObject<List<Abonnement>>(data);
                return abonnements;
            }

            return new List<Abonnement>();
        }
        private async Task<List<Panier>> GetPaniersAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Panier> paniers = JsonConvert.DeserializeObject<List<Panier>>(data);
                return paniers;
            }

            return new List<Panier>();
        }
        private async Task<List<Facture>> GetFacturesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Facture> factures = JsonConvert.DeserializeObject<List<Facture>>(data);
                return factures;
            }

            return new List<Facture>();
        }
        private async Task<List<Support>> GetSupportsAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Support> supports = JsonConvert.DeserializeObject<List<Support>>(data);
                return supports;
            }

            return new List<Support>();
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
        private async Task<Client> GetSpecificClientAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Client client = JsonConvert.DeserializeObject<Client>(data);
                return client;
            }

            return new Client();
        }
        private async Task<int> GetClientSoldeAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                int solde = JsonConvert.DeserializeObject<int>(data);
                return solde;
            }

            return 0;
        }
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
        private async Task<bool> ModifierClientSoldeAsync(string path, int montant)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(montant), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }
        private async Task<bool> ModifierClientGestionnaireAsync(string path, int IdGestionnaire)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(IdGestionnaire), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }




        /* Administrateur */
        private async Task<Administrateur> GetSpecificAdministrateurAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Administrateur administrateur = JsonConvert.DeserializeObject<Administrateur>(data);
                return administrateur;
            }

            return new Administrateur();
        }

        /* Gestionnaire */
        private async Task<Gestionnaire> GetSpecificGestionnaireAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Gestionnaire gestionnaire = JsonConvert.DeserializeObject<Gestionnaire>(data);
                return gestionnaire;
            }

            return new Gestionnaire();
        }
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

        /* Articles */
        private async Task<bool> ModifierArticleAsync(string path, int PanierId)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(PanierId), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        /* Produit */
        private async Task<bool> AddProduitAsync(string path, Produit produit)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(produit), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }
        private async Task<bool> ModifierProduitAsync(string path, Produit produit)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(produit), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }
        // Utilisé pour retirer 1 à la quantité du produit passé dans l'url
        private async Task<bool> RetirerUnProduitAsync(string path)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path);

            return response.IsSuccessStatusCode;
        }





        /* Service */
        private async Task<bool> AddServiceAsync(string path, Service service)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ModifierServiceAsync(string path, Service service)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        /* Abonnement */
        private async Task<bool> AddAbonnementAsync(string path, Abonnement abonnement)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(abonnement), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }





        /* Compte */
        private async Task<Compte> GetSpecificComptesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Compte compte = JsonConvert.DeserializeObject<Compte>(data);
                return compte;
            }

            return new Compte();
        }
        private async Task<List<Compte>> GetComptesAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Compte> comptes = JsonConvert.DeserializeObject<List<Compte>>(data);
                return comptes;
            }

            return new List<Compte>();
        }
        private async Task<bool> AddCompteAsync(string path, Compte compte)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(compte), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }
        private async Task<bool> ModifierCompteAsync(string path, Compte compte)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(compte), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

            return response.IsSuccessStatusCode;
        }


        /* Panier */
        private async Task<Panier> GetSpecificPanierAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Panier panier = JsonConvert.DeserializeObject<Panier>(data);
                return panier;
            }

            return new Panier();
        }





        /* Facture */
        private async Task<Facture> GetSpecificFactureAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Facture facture = JsonConvert.DeserializeObject<Facture>(data);
                return facture;
            }

            return new Facture();
        }
        private async Task<bool> GenererFactureAsync(string path, int CompteId)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(CompteId), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }



        /* Support */
        private async Task<bool> AddSupportAsync(string path, Support support)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(support), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(path, content);

            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ModifierSupportAsync(string path, string resolution)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(resolution), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(path, content);

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
            List<Role> roles = new List<Role>();

            try
            {
                Task<List<Role>> task = Task.Run(async () => await GetRolesAsync(path));
                task.Wait();
                roles = task.Result;
            }
            catch (Exception) { }

            return roles;
        }
        public List<Article> GetArticle(string path)
        {
            List<Article> articles = new List<Article>();

            try
            {
                Task<List<Article>> task = Task.Run(async () => await GetArticlesAsync(path));
                task.Wait();
                articles = task.Result;
            }
            catch (Exception) { }

            return articles;
        }
        public List<Produit> GetProduit(string path)
        {
            List<Produit> produits = new List<Produit>();

            try
            {
                Task<List<Produit>> task = Task.Run(async () => await GetProduitsAsync(path));
                task.Wait();
                produits = task.Result;
            }
            catch (Exception) { }

            return produits;
        }
        public List<Service> GetService(string path)
        {
            List<Service> services = new List<Service>();

            try
            {
                Task<List<Service>> task = Task.Run(async () => await GetServicesAsync(path));
                task.Wait();
                services = task.Result;
            }
            catch (Exception) { }

            return services;
        }
        public List<Abonnement> GetAbonnements(string path)
        {
            List<Abonnement> abonnements = new List<Abonnement>();

            try
            {
                Task<List<Abonnement>> task = Task.Run(async () => await GetAbonnementsAsync(path));
                task.Wait();
                abonnements = task.Result;
            }
            catch (Exception) { }

            return abonnements;
        }
        public List<Panier> GetPaniers(string path)
        {
            List<Panier> paniers = new List<Panier>();

            try
            {
                Task<List<Panier>> task = Task.Run(async () => await GetPaniersAsync(path));
                task.Wait();
                paniers = task.Result;
            }
            catch (Exception) { }

            return paniers;
        }
        public List<Facture> GetFactures(string path)
        {
            List<Facture> factures = new List<Facture>();

            try
            {
                Task<List<Facture>> task = Task.Run(async () => await GetFacturesAsync(path));
                task.Wait();
                factures = task.Result;
            }
            catch (Exception) { }

            return factures;
        }
        public List<Support> GetSupports(string path)
        {
            List<Support> supports = new List<Support>();

            try
            {
                Task<List<Support>> task = Task.Run(async () => await GetSupportsAsync(path));
                task.Wait();
                supports = task.Result;
            }
            catch (Exception) { }

            return supports;
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



        /* Administrateur */
        public Administrateur GetSpecificAdministrateur(string path)
        {
            Administrateur administrateur = new Administrateur();

            try
            {
                Task<Administrateur> task = Task.Run(async () => await GetSpecificAdministrateurAsync(path));
                task.Wait();
                administrateur = task.Result;
            }
            catch (Exception) { }

            return administrateur;
        }



        /* Client */
        public Client GetSpecificClient(string path)
        {
            Client client = new Client();

            try
            {
                Task<Client> task = Task.Run(async () => await GetSpecificClientAsync(path));
                task.Wait();
                client = task.Result;
            }
            catch (Exception) { }

            return client;
        }
        public int GetClientSolde(string path)
        {
            int Solde = 0;

            try
            {
                Task<int> task = Task.Run(async () => await GetClientSoldeAsync(path));
                task.Wait();
                Solde = task.Result;
            }
            catch (Exception) { }

            return Solde;
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
        public void ModifierClient(string path, Client client)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierClientAsync(path, client));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierClientSolde(string path, int montant)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierClientSoldeAsync(path, montant));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierClienGestionnaire(string path, int gestionnaireId)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierClientGestionnaireAsync(path, gestionnaireId));
                task.Wait();
            }
            catch (Exception) { }

        }


        /* Gestionnaire */
        public Gestionnaire GetSpecificGestionnaire(string path)
        {
            Gestionnaire gestionnaire = new Gestionnaire();

            try
            {
                Task<Gestionnaire> task = Task.Run(async () => await GetSpecificGestionnaireAsync(path));
                task.Wait();
                gestionnaire = task.Result;
            }
            catch (Exception) { }

            return gestionnaire;
        }
        public void AddGestionnaire(string path, Gestionnaire gestionnaire)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddGestionnaireAsync(path, gestionnaire));
                task.Wait();
            }
            catch (Exception) { }

        }
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


        /* Article */
        public void ModifierArticle(string path, int PanierId)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierArticleAsync(path, PanierId));
                task.Wait();
            }
            catch (Exception) { }

        }


        /* Produit */
        public void AddProduit(string path, Produit produit)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddProduitAsync(path, produit));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierProduit(string path, Produit produit)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierProduitAsync(path, produit));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void RetirerUnProduit(string path)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await RetirerUnProduitAsync(path));
                task.Wait();
            }
            catch (Exception) { }

        }



        /* Service */
        public void AddService(string path, Service service)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddServiceAsync(path, service));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierService(string path, Service service)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierServiceAsync(path, service));
                task.Wait();
            }
            catch (Exception) { }

        }


        /* Abonnement */
        public void AddAbonnement(string path, Abonnement abonnement)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddAbonnementAsync(path, abonnement));
                task.Wait();
            }
            catch (Exception) { }

        }




        /* Compte */
        public Compte GetSpecificCompte(string path)
        {
            Compte compte = new Compte();

            try
            {
                Task<Compte> task = Task.Run(async () => await GetSpecificComptesAsync(path));
                task.Wait();
                compte = task.Result;
            }
            catch (Exception) { }

            return compte;
        }
        public List<Compte> GetComptes(string path)
        {
            List<Compte> comptes = new List<Compte>();

            try
            {
                Task<List<Compte>> task = Task.Run(async () => await GetComptesAsync(path));
                task.Wait();
                comptes = task.Result;
            }
            catch (Exception) { }

            return comptes;
        }
        public void AddCompte(string path, Compte compte)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddCompteAsync(path, compte));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierCompte(string path, Compte compte)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierCompteAsync(path, compte));
                task.Wait();
            }
            catch (Exception) { }

        }



        /* Panier */
        public Panier GetSpecificPanier(string path)
        {
            Panier panier = new Panier();

            try
            {
                Task<Panier> task = Task.Run(async () => await GetSpecificPanierAsync(path));
                task.Wait();
                panier = task.Result;
            }
            catch (Exception) { }

            return panier;
        }


        /* Facture */
        public Facture GetSpecificFacture(string path)
        {
            Facture facture = new Facture();

            try
            {
                Task<Facture> task = Task.Run(async () => await GetSpecificFactureAsync(path));
                task.Wait();
                facture = task.Result;
            }
            catch (Exception) { }

            return facture;
        }
        public void GenererFacture(string path, int CompteId)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await GenererFactureAsync(path, CompteId));
                task.Wait();
            }
            catch (Exception) { }

        }


        /* Support */
        public void AddSupport(string path, Support support)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await AddSupportAsync(path, support));
                task.Wait();
            }
            catch (Exception) { }

        }
        public void ModifierSupport(string path, string resolution)
        {
            try
            {
                Task<bool> task = Task.Run(async () => await ModifierSupportAsync(path, resolution));
                task.Wait();
            }
            catch (Exception) { }

        }








        // Suppression d'un élément (n'importe lequel)
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
