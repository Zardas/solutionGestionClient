using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class AchatArticleDetailViewModel
    {

        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private int IdCompte;

        private Models.Article Article;
        public string NomArticle { get; set; }
        public string ImageArticle { get; set; }
        public string TypeArticle { get; set; }
        public string PrixArticle { get; set; }
        public string DescriptionArticle { get; set; }
        public string ManuelArticle { get; set; }


        public AchatArticleDetailViewModel(Window window, int idCompte, int idArticle)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            

            this.IdCompte = idCompte;
            this.Article = DBContext.Articles.Where(a => (a.Id.Equals(idArticle))).FirstOrDefault();

            this.NomArticle = this.Article.Nom;
            this.ImageArticle = this.Article.Image;
            this.TypeArticle = this.Article.Type;
            this.PrixArticle = this.Article.Prix + " $";
            this.DescriptionArticle = this.Article.GetArticleDetails();
            this.ManuelArticle = this.ManuelArticle;





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
            GoToListeCompteClientCommand = new RelayCommand(
                o => true,
                o => OpenListeCompteClient()
            );
            GoToListeArticles = new RelayCommand(
                o => true,
                o => OpenListeArticles()
            );
            GoToPanier = new RelayCommand(
                o => true,
                o => OpenPanier()
            );
            AjouterArticlePanierCommand = new RelayCommand(
                o => true,
                o => AjouterArticlePanier()
            );
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationCompteCommand { get; private set; }
        public ICommand GoToPageCompteCommand { get; private set; }
        public ICommand GoToListeCompteClientCommand { get; private set; }
        public ICommand GoToListeArticles { get; private set; }
        public ICommand GoToPanier { get; private set; }
        public ICommand AjouterArticlePanierCommand { get; private set; }






        // Ajout de l'article au panier
        private void AjouterArticlePanier()
        {
            Models.Panier panier = DBContext.Paniers.Where(p => (p.CompteId.Equals(this.IdCompte))).FirstOrDefault();

            panier.AjoutArticle(Article);
            DBContext.SaveChanges();
            MessageBox.Show("Article ajouté à votre panier");

            this.OpenListeArticles();
        }





        /* Navigation */
        private void OpenAccueil()
        {
            Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(IdCompte))).FirstOrDefault();
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(compte.ClientId))).FirstOrDefault();

            client.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenModificationCompte()
        {
            //Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(IdCompte))).FirstOrDefault();
            Views.ModificationCompte modificationCompte = new Views.ModificationCompte(IdCompte);
            modificationCompte.Show();
            _window.Close();
        }
        private void OpenListeCompteClient()
        {
            Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(IdCompte))).FirstOrDefault();
            Models.Utilisateur client = DBContext.Utilisateurs.Where(c => (c.Id.Equals(compte.ClientId))).FirstOrDefault();
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(client);
            listeCompteClient.Show();
            _window.Close();
        }
        private void OpenPageClient()
        {
            //Models.Compte compte = DBContext.Comptes.Where(c => (c.CompteId.Equals(IdCompte))).FirstOrDefault();
            Views.PageCompteClient pageCompteClient = new Views.PageCompteClient(IdCompte);
            pageCompteClient.Show();
            _window.Close();
        }
        private void OpenListeArticles()
        {
            Views.Achat_ListeArticle achat_ListeArticle = new Views.Achat_ListeArticle(IdCompte);
            achat_ListeArticle.Show();
            _window.Close();
        }
        private void OpenPanier()
        {
            Views.Panier panier = new Views.Panier(IdCompte);
            panier.Show();
            _window.Close();
        }
    }
}
