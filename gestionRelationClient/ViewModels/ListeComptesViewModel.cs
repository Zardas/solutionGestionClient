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
            // Comment mettre le résultats d'un Where dans une obervableCollection
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
        }



        /* definition of the commands */
        public ICommand AjoutCompteCommand { get; private set; }



        private void InitializeComptes()
        {
            
        }





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
                







        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
