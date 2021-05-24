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
    class ListeComptesClientViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdClient;

        /* Token */
        private string Token;

        public ObservableCollection<Compte> Comptes { get; set; }
        public Compte SelectedCompte { get; set; }


        public string LoginClient { get; set; }

        public string NomNouveauCompte { get; set; }

        /* constructor and initialization */
        public ListeComptesClientViewModel(Window window, int IdClient, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdClient = IdClient;


            this.Token = Token;

            string path = "Client/" + IdClient;
            Client client = _restApiQueries.GetSpecificClient(path);

            LoginClient = client.Login;

            // Liste des comptes
            path = "Compte/Client/" + IdClient;
            Comptes = new ObservableCollection<Compte>(_restApiQueries.GetComptes(path));



            /* Commandes de routing */
            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            /* Boutons */
            AjouterCompteCommand = new RelayCommand(
                o => (NomNouveauCompte != null),
                o => AjoutCompte()
            );

            SelectionCompteCommand = new RelayCommand(
                o => (SelectedCompte != null),
                o => _router.GoToInterfaceClient(_window, SelectedCompte.CompteId, Token)
            );


        }

        /* Menu */
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand AjouterCompteCommand { get; private set; }
        public ICommand SelectionCompteCommand { get; private set; }



        public void SynchroniserComptes()
        {
            Comptes.Clear();

            string path = "Compte/Client/" + IdClient;
            foreach (Compte compte in _restApiQueries.GetComptes(path))
            {
                Comptes.Add(compte);
            }
        }


        private void AjoutCompte()
        {

            Compte nouveauCompte = new Compte()
            {
                ClientId = IdClient,
                NomCompte = NomNouveauCompte,
            };

            string path = "Compte";
            _restApiQueries.AddCompte(path, nouveauCompte);
            SynchroniserComptes();
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
