using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionClientWPF.Models;

namespace GestionClientWPF.ViewModels
{
    class AjoutAbonnementViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdGestionnaire;

        /* Token */
        private string Token;

        public ObservableCollection<Abonnement> Abonnements { get; set; }


        /* Produit */
        public int dureeNouvelAbonnement { get; set; }

        private bool isValid_abonnement()
        {
            return (dureeNouvelAbonnement > 0);
        }


        /* constructor and initialization */
        public AjoutAbonnementViewModel(Window window, int IdGestionnaire, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdGestionnaire = IdGestionnaire;


            this.Token = Token;

            // Liste des abonnements
            string path = "Abonnement";
            Abonnements = new ObservableCollection<Abonnement>(_restApiQueries.GetAbonnements(path));



            /* Commandes de routing */
            GoToInterfaceCommercial = new RelayCommand(
                o => true,
                o => _router.GoToInterfaceCommercial(_window, IdGestionnaire, Token)
            );

            GoToAssociationClient = new RelayCommand(
                o => true,
                o => _router.GoToAssociationClient(_window, IdGestionnaire, Token)
            );

            GoToAjoutProduit = new RelayCommand(
                o => true,
                o => _router.GoToAjoutProduit(_window, IdGestionnaire, Token)
            );

            GoToAjoutService = new RelayCommand(
                o => true,
                o => _router.GoToAjoutService(_window, IdGestionnaire, Token)
            );

            GoToAjoutAbonnement = new RelayCommand(
                o => true,
                o => _router.GoToAjoutAbonnement(_window, IdGestionnaire, Token)
            );

            GoToListeTickets = new RelayCommand(
                o => true,
                o => _router.GoToListeTicketsGestionnaire(_window, IdGestionnaire, Token)
            );

            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            /* Boutons */
            AjouterAbonnementCommand = new RelayCommand(
                o => isValid_abonnement(),
                o => AjoutAbonnement()
            );


        }

        /* Menu */
        public ICommand GoToInterfaceCommercial { get; private set; }
        public ICommand GoToAssociationClient { get; private set; }
        public ICommand GoToAjoutProduit { get; private set; }
        public ICommand GoToAjoutService { get; private set; }
        public ICommand GoToAjoutAbonnement { get; private set; }
        public ICommand GoToListeTickets { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand AjouterAbonnementCommand { get; private set; }



        public void SynchroniserAbonnements()
        {
            string path = "Abonnement/";
            Abonnements.Clear();

            foreach (Abonnement abonnement in _restApiQueries.GetAbonnements(path))
            {
                Abonnements.Add(abonnement);
            }
        }


        private void AjoutAbonnement()
        {

            Abonnement abonnement = new Abonnement()
            {
                DureeAbonnement = dureeNouvelAbonnement
            };

            string path = "Abonnement";
            _restApiQueries.AddAbonnement(path, abonnement);
            SynchroniserAbonnements();
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
