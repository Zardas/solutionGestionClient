using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class ListeComptesViewModel
    {

        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;

        private Models.Utilisateur Client;

        public string nomClient { get; set; }



        public ObservableCollection<Models.Compte> Comptes { get; set; }

        private Models.Compte p_SelectedItem;
        public Models.Compte SelectedItem
        {
            get { return p_SelectedItem; }

            set
            {
                p_SelectedItem = value;
                //base.FirePropertyChangedEvent("SelectedItem");
            }
        }


        private Models.Compte _addedCompte;
        private bool _isValid_addedCompte;


        



        public ListeComptesViewModel(Window window, Models.Utilisateur client)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Client = client;
            this.nomClient = this.Client.Login;

            this._addedCompte = new Models.Compte();



            // Initialisation du compte
            ObservableCollection<Models.Compte> comptesATrouver = Models.Utilitaire.ToObservableCollection<Models.Compte>(DBContext.Comptes.Where(c => c.ClientId.Equals(Client.Id)));

            if (comptesATrouver == null)
            {
                this.Comptes = new ObservableCollection<Models.Compte>();
            }
            else
            {
                this.Comptes = comptesATrouver;
            }

            



            // Inscription
            AjoutCompteCommand = new RelayCommand(
                o => _isValid_addedCompte,
                o => AddCompte()
            );

            // Sélection compte
            SelectionnerCompteCommand = new RelayCommand(
                o => (p_SelectedItem != null),
                o => SelectionnerCompte()
            );

            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            GoToModificationClientCommand = new RelayCommand(
                o => true,
                o => OpenModificationClient()
            );
            GoToListeCompteClientCommand = new RelayCommand(
                o => true,
                o => OpenListeCompteClient(this.Client)
            );
        }



        /* definition of the commands */
        public ICommand AjoutCompteCommand { get; private set; }
        public ICommand SelectionnerCompteCommand { get; private set; }
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToModificationClientCommand { get; private set; }
        public ICommand GoToListeCompteClientCommand { get; private set; }


        





        // Ajout nouveau compte
        //Il y a plus simple pour un formulaire à un seul champ, mais autant faire partout pareil
        public string NomNouveauCompte
        {
            get
            {
                return _addedCompte.NomCompte;
            }
            set
            {
                if (_addedCompte.NomCompte != value)
                {
                    _addedCompte.NomCompte = value;
                    SetIsValid_AddedCompte();
                }
            }
        }
        private void SetIsValid_AddedCompte()
        {
            _isValid_addedCompte = !string.IsNullOrEmpty(NomNouveauCompte);
        }


        private void AddCompte()
        {


            if (Comptes.Any(c => c.NomCompte == _addedCompte.NomCompte))
            {
                MessageBox.Show("Le compte existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {

                this.Comptes.Add(new Models.Compte() {
                    CompteId = Comptes.Count,
                    ClientId = this.Client.Id,
                    DateCreation = System.DateTime.Now,
                    NomCompte = this._addedCompte.NomCompte
                });

                // On ne peut pas ajouter le Compte au Client dans le modèle vu qu'on se balade avec un Utilisateur et pas un client à cause de ce satané Table per Type

                DBContext.Comptes.Add(new Models.Compte()
                {
                    ClientId = this.Client.Id,
                    DateCreation = System.DateTime.Now,
                    NomCompte = this._addedCompte.NomCompte
                });
                DBContext.SaveChanges();

                MessageBox.Show("Le compte " + _addedCompte.NomCompte + " a été ajouté");
            }

            
                
        }



        // Sélection du compte
        public void SelectionnerCompte()
        {
            Views.PageCompteClient pageCompteClient = new Views.PageCompteClient(Client, p_SelectedItem);
            pageCompteClient.Show();
            _window.Close();
        }






        /* Navigation */
        public void OpenAccueil()
        {
            this.Client.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        //TODO
        public void OpenModificationClient()
        {

        }
        private void OpenListeCompteClient(Models.Utilisateur client)
        {
            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(client);
            listeCompteClient.Show();
            _window.Close();
        }






        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
