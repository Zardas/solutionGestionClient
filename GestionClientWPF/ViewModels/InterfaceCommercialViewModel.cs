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
    class InterfaceCommercialViewModel : INotifyPropertyChanged
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


        public string LoginGestionnaire { get; set; }


        /* Liste des clients liés */
        public ObservableCollection<Client> ClientsLiés { get; set; }
        public Client SelectedClient { get; set; }

        /* Liste des produits liés */
        public ObservableCollection<Produit> ProduitsLiés { get; set; }
        public Produit SelectedProduit { get; set; }

        /* Liste des services */
        public ObservableCollection<Service> Services { get; set; }
        public Service SelectedService { get; set; }


        /* constructor and initialization */
        public InterfaceCommercialViewModel(Window window, int IdGestionnaire, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdGestionnaire = IdGestionnaire;

            // Initialisation des listes
            string path;


            // Liste des clients associés au gestionnaire
            path = "Client/GestionnaireAssocie/" + IdGestionnaire;
            ClientsLiés = new ObservableCollection<Client>(_restApiQueries.GetClients(path));

            // Liste des produits associés au gestionnaire
            path = "Produit/GestionnaireAssocie/" + IdGestionnaire;
            ProduitsLiés = new ObservableCollection<Produit>(_restApiQueries.GetProduit(path));

            path = "Service";
            Services = new ObservableCollection<Service>(_restApiQueries.GetService(path));

            this.Token = Token;


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


            /* Commandes d'action */
            RetirerClient = new RelayCommand(
                o => (SelectedClient != null),
                o => Debug.WriteLine("TODO")
            );

            ModifierProduit = new RelayCommand(
                o => (SelectedProduit != null),
                o => _router.GoToModificationProduit(_window, IdGestionnaire, Token, SelectedProduit)
            );

            SupprimerProduit = new RelayCommand(
                o => (SelectedProduit != null),
                o => RetirerUnProduit()
            );

            ModifierService = new RelayCommand(
                o => (SelectedService != null),
                o => _router.GoToModificationService(_window, IdGestionnaire, Token, SelectedService)
            );

            SupprimerService = new RelayCommand(
                o => (SelectedService != null),
                o => RemoveService()
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
        public ICommand RetirerClient { get; private set; }
        public ICommand ModifierProduit { get; private set; }
        public ICommand SupprimerProduit { get; private set; }
        public ICommand ModifierService { get; private set; }
        public ICommand SupprimerService { get; private set; }





        public void SynchroniserClients()
        {
            string path = "Client/GestionnaireAssocie/" + IdGestionnaire;
            ClientsLiés.Clear();

            foreach (Client client in _restApiQueries.GetClients(path))
            {
                ClientsLiés.Add(client);
            }
        }
        public void SynchroniserProduits()
        {
            string path = "Produit/GestionnaireAssocie/" + IdGestionnaire;
            ProduitsLiés.Clear();

            foreach (Produit produit in _restApiQueries.GetProduit(path))
            {
                ProduitsLiés.Add(produit);
            }
        }
        public void SynchroniserServices()
        {
            string path = "Service";
            Services.Clear();

            foreach (Service service in _restApiQueries.GetService(path))
            {
                Services.Add(service);
            }
        }





        public void RetirerUnProduit()
        {
            string path = "Produit/RetirerUn/" + SelectedProduit.ArticleId;
            _restApiQueries.RetirerUnProduit(path);
            SynchroniserProduits();
        }

        public void RemoveService()
        {
            string path = "Service/" + SelectedService.ArticleId;
            _restApiQueries.Remove(path);
            SynchroniserServices();
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
