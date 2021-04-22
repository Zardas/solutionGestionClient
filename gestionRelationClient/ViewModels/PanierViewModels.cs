using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class PanierViewModels
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;


        private Models.Panier Panier;

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

        public string ValeurTotale {get; set;}

        public PanierViewModels(Window window, int idCompte)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Panier = DBContext.Paniers.Where(p => (p.CompteId.Equals(idCompte))).FirstOrDefault();

            this.Articles = Models.Utilitaire.ToObservableCollection(DBContext.Articles.Where(a => (a.PanierId.Equals(this.Panier.PanierId))));

            this.ValeurTotale = "Valeur totale : " + this.Panier.getPrixTotal() + " $";





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
            GoToListeArticle = new RelayCommand(
                o => true,
                o => OpenListeArticle()
            );
            SelectionnerArticleCommand = new RelayCommand(
                o => true,
                o => OpenDetailsArticle()
            );
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationCompteCommand { get; private set; }
        public ICommand GoToPageCompteCommand { get; private set; }
        public ICommand GoToListeComptesClient { get; private set; }
        public ICommand GoToListeArticle { get; private set; }
        public ICommand SelectionnerArticleCommand { get; private set; }







        /* Navigation */
        private void OpenAccueil()
        {
            Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(this.Panier.CompteId))).FirstOrDefault();
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(compte.ClientId))).FirstOrDefault();
            client.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenModificationCompte()
        {
            Views.ModificationCompte modificationCompte = new Views.ModificationCompte(Panier.CompteId);
            modificationCompte.Show();
            _window.Close();
        }
        private void OpenListeCompteClient()
        {
            Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(this.Panier.CompteId))).FirstOrDefault();
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(compte.ClientId))).FirstOrDefault();
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(client);
            listeCompteClient.Show();
            _window.Close();
        }
        private void OpenPageClient()
        {
            Views.PageCompteClient pageCompteClient = new Views.PageCompteClient(Panier.CompteId);
            pageCompteClient.Show();
            _window.Close();
        }
        private void OpenListeArticle()
        {
            Views.Achat_ListeArticle achat_ListeArticle = new Views.Achat_ListeArticle(Panier.CompteId);
            achat_ListeArticle.Show();
            _window.Close();
        }


        private void OpenDetailsArticle()
        {
            Views.Consultation_DetailsArticle consultation_DetailsArticle = new Views.Consultation_DetailsArticle(Panier.CompteId, p_SelectedItem.Id);
            consultation_DetailsArticle.Show();
            _window.Close();
        }
    }
}
