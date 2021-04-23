using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class PageCompteClientViewModel
    {

        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private Models.Utilisateur Client;
        private Models.Compte Compte;

        public string NomClient { get; set; }
        public string NomCompte { get; set; }


        /* Pour la modification du nom du Compte (on pourrait aussi passer par un isValid comme pour la connexion/inscription... */
        public string LoginModificationCompte { get; set; }
        public string MotDePasseModificationCompte { get; set; }
        public string NouveauNomModificationCompte { get; set; }

        public bool InformationsModificationNomCompteValides()
        {
            return (!string.IsNullOrEmpty(LoginModificationCompte) &&
                    !string.IsNullOrEmpty(MotDePasseModificationCompte) &&
                    !string.IsNullOrEmpty(NouveauNomModificationCompte));
        }





        public PageCompteClientViewModel(Window window, int idCompte)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();


            this.Compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(idCompte))).FirstOrDefault();

            this.Client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(this.Compte.ClientId))).FirstOrDefault();
            

            this.NomClient = this.Client.Login;
            this.NomCompte = this.Compte.NomCompte;



            // Modification nom Compte
            ModificationNomCompteCommand = new RelayCommand(
                o => InformationsModificationNomCompteValides(),
                o => ModifieNomClient(LoginModificationCompte, MotDePasseModificationCompte, NouveauNomModificationCompte)
            );

            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            GoToModificationCompteCommand = new RelayCommand(
                o => true,
                o => OpenModificationCompte()
            );
            GoToListeCompteClientCommand = new RelayCommand(
                o => true,
                o => OpenListeCompteClient()
            );
            GoToListArticleCommand = new RelayCommand(
                o => true,
                o => OpenListeArticle()
            );
            GoToPanierCommand = new RelayCommand(
                o => true,
                o => OpenPanier()
            );
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationCompteCommand { get; private set; }
        public ICommand GoToListeCompteClientCommand { get; private set; }
        public ICommand ModificationNomCompteCommand { get; private set; }
        public ICommand GoToListArticleCommand { get; private set; }
        public ICommand GoToPanierCommand { get; private set; }














        // Modification nom client
        public void ModifieNomClient(string login, string motDePasse, string nouveauNom)
        {
            if(login != this.Client.Login)
            {
                MessageBox.Show("Mauvais login", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if(motDePasse != this.Client.MotDePasse)
            {
                MessageBox.Show("Mauvais mot de passe", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                Compte.ModifierNomCompte(nouveauNom);
                DBContext.SaveChanges();
                OpenPageClient();
            }
        }









        /* Navigation */
        private void OpenAccueil()
        {

            Client.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenModificationCompte()
        {
            Views.ModificationCompte modificationCompte = new Views.ModificationCompte(Compte.CompteId);
            modificationCompte.Show();
            _window.Close();
        }
        private void OpenListeCompteClient()
        {
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(Client);
            listeCompteClient.Show();
            _window.Close();
        }

        private void OpenPageClient()
        {
            Views.PageCompteClient pageCompteClient = new Views.PageCompteClient(Compte.CompteId);
            pageCompteClient.Show();
            _window.Close();
        }
        private void OpenListeArticle()
        {
            Views.Achat_ListeArticle achat_ListeArticle = new Views.Achat_ListeArticle(Compte.CompteId);
            achat_ListeArticle.Show();
            _window.Close();
        }
        private void OpenPanier()
        {
            Views.Panier panier = new Views.Panier(Compte.CompteId);
            panier.Show();
            _window.Close();
        }
    }
}
