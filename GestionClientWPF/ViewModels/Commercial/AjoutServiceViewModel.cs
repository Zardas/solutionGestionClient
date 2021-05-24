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
    class AjoutServiceViewModel : INotifyPropertyChanged
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


        /* Liste des abonnements */
        public ObservableCollection<Abonnement> Abonnements { get; set; }

        /* Produit */
        public string NomService { get; set; }
        public string DescriptionService { get; set; }
        public int PrixService { get; set; }
        public string ImageService { get; set; }
        public string TypeService { get; set; }
        public string ManuelService { get; set; }
        public string ConditionsService { get; set; }
        public Abonnement AbonnementService { get; set; }

        private bool isValid_addedService()
        {
            return (!string.IsNullOrEmpty(NomService) &&
                    !string.IsNullOrEmpty(DescriptionService) &&
                    PrixService > 0 &&
                    !string.IsNullOrEmpty(ImageService) &&
                    !string.IsNullOrEmpty(TypeService) &&
                    !string.IsNullOrEmpty(ManuelService) &&
                    !string.IsNullOrEmpty(ConditionsService) &&
                    AbonnementService != null);
        }


        /* constructor and initialization */
        public AjoutServiceViewModel(Window window, int IdGestionnaire, string Token)
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
            AjouterServiceCommand = new RelayCommand(
                o => isValid_addedService(),
                o => AjoutService()
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
        public ICommand AjouterServiceCommand { get; private set; }






        private void AjoutService()
        {

            Service service = new Service()
            {
                Nom = NomService,
                Image = ImageService,
                Type = TypeService,
                Prix = PrixService,
                Description = DescriptionService,
                Manuel = ManuelService,
                Conditions = ConditionsService,
                AbonnementId = AbonnementService.AbonnementId
            };

            string path = "Service";
            _restApiQueries.AddService(path, service);
            _router.GoToInterfaceCommercial(_window, IdGestionnaire, Token);
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
