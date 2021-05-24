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
    class ListeTicketsGestionnaireViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Commercial */
        private int IdGestionnaire;

        /* Token */
        private string Token;


        public string MessageResolution { get; set; }
        public string Description { get; set; }

        public ObservableCollection<Support> Supports { get; set; }

        public Support SelectedTicket { get; set; }



        /* constructor and initialization */
        public ListeTicketsGestionnaireViewModel(Window window, int IdGestionnaire, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            string path = "Support/Commercial/Ouvert/" + IdGestionnaire;
            Supports = new ObservableCollection<Support>(_restApiQueries.GetSupports(path));


            this.IdGestionnaire = IdGestionnaire;
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

            /* Actions */
            FermerTicketCommand = new RelayCommand(
                o => (SelectedTicket != null),
                o => FermerTicket()
            );

            AfficherDescriptionCommand = new RelayCommand(
                o => (SelectedTicket != null),
                o => AfficherDescription()
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

        /* Actions */
        public ICommand FermerTicketCommand { get; private set; }
        public ICommand AfficherDescriptionCommand { get; private set; }



        public void SynchroniserSupports()
        {
            Supports.Clear();

            string path = "Support/Commercial/Ouvert/" + IdGestionnaire;
            foreach (Support support in _restApiQueries.GetSupports(path))
            {
                Supports.Add(support);
            }
        }




        public void FermerTicket()
        {
            string path = "Support/Resoudre/" + SelectedTicket.SupportId;
            _restApiQueries.ModifierSupport(path, MessageResolution);
            SynchroniserSupports();
        }


        public void AfficherDescription()
        {
            Description = SelectedTicket.Description;
            OnPropertyChanged("Description");
        }



        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
