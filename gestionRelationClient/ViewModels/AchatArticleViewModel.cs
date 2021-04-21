using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class AchatArticleViewModel
    {

        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private Models.Compte Compte;


        // Liste des articles à afficher (pas ceux que l'utilisateur possède déjà)
        public ObservableCollection<Models.Article> Articles { get; set; }

        private Models.Article p_SelectedItem;
        public Models.Article SelectedItem
        {
            get { return p_SelectedItem; }

            set
            {
                p_SelectedItem = value;
            }
        }




        public AchatArticleViewModel(Window window, int idCompte)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(idCompte))).FirstOrDefault();

            // Pour l'instant, on ne s'embète pas à ne pas afficher les article qu'à déjà l'utilisateur
            this.Articles = Models.Utilitaire.ToObservableCollection(DBContext.Articles);






            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            GoToModificationCompteCommand = new RelayCommand(
                o => true,
                o => OpenModificationCompte()
            );
            GoToPageCompteCommand = new RelayCommand(
                o => true,
                o => OpenPageClient()
            );
            GoToListeComptesClient = new RelayCommand(
                o => true,
                o => OpenListeCompteClient()
            );
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationCompteCommand { get; private set; }
        public ICommand GoToPageCompteCommand { get; private set; }
        public ICommand GoToListeComptesClient { get; private set; }








        /* Navigation */
        private void OpenAccueil()
        {
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(this.Compte.ClientId))).FirstOrDefault();
            client.Deconnexion();
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
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(this.Compte.ClientId))).FirstOrDefault();
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(client);
            listeCompteClient.Show();
            _window.Close();
        }

        private void OpenPageClient()
        {
            Views.PageCompteClient pageCompteClient = new Views.PageCompteClient(Compte.CompteId);
            pageCompteClient.Show();
            _window.Close();
        }
    }
}
