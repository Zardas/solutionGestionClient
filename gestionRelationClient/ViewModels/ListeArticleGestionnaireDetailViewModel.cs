using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class ListeArticleGestionnaireDetailViewModel
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        // Utiliser pour pouvoir retourner en arrière
        private int idGestionnaire;

        private Models.Article Article;
        public string NomArticle { get; set; }
        public string ImageArticle { get; set; }
        public string TypeArticle { get; set; }
        public string PrixArticle { get; set; }
        public string DescriptionArticle { get; set; }
        public string ManuelArticle { get; set; }


        public ListeArticleGestionnaireDetailViewModel(Window window, int idGestionnaire, int idArticle)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.idGestionnaire = idGestionnaire;
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
            GoToListeArticlesCommand = new RelayCommand(
                o => true,
                o => OpenListeArticle()
            );
            GoToPageGestionnaireCommand = new RelayCommand(
                o => true,
                o => OpenPageGestionnaire()
            );
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToListeArticlesCommand { get; private set; }
        public ICommand GoToPageGestionnaireCommand { get; private set; }



        /* Navigation */
        private void OpenAccueil()
        {
            Models.Utilisateur gestionnaire = DBContext.Utilisateurs.Where(c => (c.Id.Equals(this.idGestionnaire))).FirstOrDefault();
            gestionnaire.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenListeArticle()
        {
            Views.ListeArticleGestionnaire listeArticleGestionnaire = new Views.ListeArticleGestionnaire(idGestionnaire);
            listeArticleGestionnaire.Show();
            _window.Close();
        }
        private void OpenPageGestionnaire()
        {
            Views.PageGestionnaire pageGestionnaire = new Views.PageGestionnaire(idGestionnaire);
            pageGestionnaire.Show();
            _window.Close();
        }
    }
}
