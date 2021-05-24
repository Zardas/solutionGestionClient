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
    class ListeTicketsClientViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Compte */
        private int IdCompte;

        /* Token */
        private string Token;


        public string MessageResolution { get; set; }
        public string Description { get; set; }

        public ObservableCollection<Support> Supports { get; set; }

        public Support SelectedTicket { get; set; }



        /* constructor and initialization */
        public ListeTicketsClientViewModel(Window window, int IdCompte, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            string path = "Support/Compte/" + IdCompte;
            Supports = new ObservableCollection<Support>(_restApiQueries.GetSupports(path));


            this.IdCompte = IdCompte;
            this.Token = Token;

            MessageResolution = "Résolu par le client";

            /* Routing */
            GoToInterfaceClient = new RelayCommand(
                o => true,
                o => _router.GoToInterfaceClient(_window, IdCompte, Token)
            );

            GoToListeFactures = new RelayCommand(
                o => true,
                o => _router.GoToListeFactures(_window, IdCompte, Token)
            );

            GoToListeTicketsClient = new RelayCommand(
                o => true,
                o => _router.GoToListeTicketsClient(_window, IdCompte, Token)
            );

            GoToSolde = new RelayCommand(
                o => true,
                o => _router.GoToSoldeClient(_window, IdCompte, Token)
            );

            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            /* Actions */
            FermerTicketCommand = new RelayCommand(
                o => (SelectedTicket != null && SelectedTicket.Status != "Resolu"),
                o => FermerTicket()
            );

            AfficherDescriptionCommand = new RelayCommand(
                o => (SelectedTicket != null),
                o => AfficherDescription()
            );


        }

        /* Menu */
        public ICommand GoToInterfaceClient { get; private set; }
        public ICommand GoToListeFactures { get; private set; }
        public ICommand GoToListeTicketsClient { get; private set; }
        public ICommand GoToSolde { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Actions */
        public ICommand FermerTicketCommand { get; private set; }
        public ICommand AfficherDescriptionCommand { get; private set; }



        public void SynchroniserSupports()
        {
            Supports.Clear();

            string path = "Support/Compte/" + IdCompte;
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
