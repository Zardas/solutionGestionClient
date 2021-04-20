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


        public PageCompteClientViewModel(Window window, Models.Utilisateur client, Models.Compte compte)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Client = client;
            this.Compte = compte;

            this.NomClient = this.Client.Login;
            this.NomCompte = this.Compte.NomCompte;





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
        }



        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationCompteCommand { get; private set; }
        public ICommand GoToListeCompteClientCommand { get; private set; }








        /* Navigation */
        public void OpenAccueil()
        {

            Client.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        //TODO
        public void OpenModificationCompte()
        {

        }
        private void OpenListeCompteClient()
        {
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(Client);
            listeCompteClient.Show();
            _window.Close();
        }
    }
}
