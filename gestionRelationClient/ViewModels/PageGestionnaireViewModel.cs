using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class PageGestionnaireViewModel
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private Models.Utilisateur Gestionnaire;

        public string LoginGestionnaire { get; set; }

        // ! Le STOCK ne peut pas être géré à cause de ce #!!?! de Table per Type : comme les Clients et Gestionnaires ne sont pas dans le DBContext, on ne peut se balader qu'avec des Utilisateurs, qui du coup n'ont pas forcément de Stocks associés
        public string AffichageStock { get; set; }

        public PageGestionnaireViewModel(Window window, int idGestionnaire)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Gestionnaire = DBContext.Utilisateurs.Where(u => (u.Id.Equals(idGestionnaire))).FirstOrDefault();
            this.LoginGestionnaire = Gestionnaire.Login;


            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            GoToPageGestionnaire = new RelayCommand(
                o => true,
                o => OpenPageGestionnaire()
            );
            GoToAjoutServiceCommand = new RelayCommand(
                o => true,
                o => OpenPageAjoutService()
            );
            GoToAjoutProduitCommand = new RelayCommand(
                o => true,
                o => OpenPageAjoutProduit()
            );
            GoToListeArticleGestionnaire = new RelayCommand(
                o => true,
                o => OpenListeArticleGestionnaire()
            );
        }


        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToPageGestionnaire { get; private set; }
        public ICommand GoToAjoutServiceCommand { get; private set; }
        public ICommand GoToAjoutProduitCommand { get; private set; }
        public ICommand GoToListeArticleGestionnaire { get; private set; }



        /* Navigation */
        private void OpenAccueil()
        {
            Gestionnaire.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenPageGestionnaire()
        {
            Views.PageGestionnaire pageGestionnaire = new Views.PageGestionnaire(Gestionnaire.Id);
            pageGestionnaire.Show();
            _window.Close();
        }
        private void OpenPageAjoutService()
        {
            Views.AjoutService ajoutService = new Views.AjoutService(Gestionnaire.Id);
            ajoutService.Show();
            _window.Close();
        }
        private void OpenPageAjoutProduit()
        {
            Views.AjoutProduit ajoutProduit = new Views.AjoutProduit(Gestionnaire.Id);
            ajoutProduit.Show();
            _window.Close();
        }
        private void OpenListeArticleGestionnaire()
        {
            Views.ListeArticleGestionnaire listeArticleGestionnaire = new Views.ListeArticleGestionnaire(Gestionnaire.Id);
            listeArticleGestionnaire.Show();
            _window.Close();
        }
    }
}
