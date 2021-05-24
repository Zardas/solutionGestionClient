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
    class ListeFacturesViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdCompte;

        /* Token */
        private string Token;


        /* Liste des articles à acheter */
        public ObservableCollection<Facture> Factures { get; set; }



        /* constructor and initialization */
        public ListeFacturesViewModel(Window window, int IdCompte, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();


            // Liste des factures
            string path = "Facture/" + IdCompte;
            Factures = new ObservableCollection<Facture>(_restApiQueries.GetFactures(path));


            this.IdCompte = IdCompte;
            this.Token = Token;


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


        }

        /* Menu */
        public ICommand GoToInterfaceClient { get; private set; }
        public ICommand GoToListeFactures { get; private set; }
        public ICommand GoToListeTicketsClient { get; private set; }
        public ICommand GoToSolde { get; private set; }
        public ICommand GoToConnexion { get; private set; }



       


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
