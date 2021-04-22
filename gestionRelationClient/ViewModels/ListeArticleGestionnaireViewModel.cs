using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class ListeArticleGestionnaireViewModel
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private int idGestionnaire;


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




        public ListeArticleGestionnaireViewModel(Window window, int idGestionnaire)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.idGestionnaire = idGestionnaire;


            this.Articles = Models.Utilitaire.ToObservableCollection(DBContext.Articles);





            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            SelectionnerArticle = new RelayCommand(
                o => true,
                o => OpenDetailsArticles()
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
        public ICommand SelectionnerArticle { get; private set; }
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
        private void OpenDetailsArticles()
        {
            Views.Consultation_DetailsArticleGestionnaire consultation_DetailsArticleGestionnaire = new Views.Consultation_DetailsArticleGestionnaire(idGestionnaire, SelectedItem.Id);
            consultation_DetailsArticleGestionnaire.Show();
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
