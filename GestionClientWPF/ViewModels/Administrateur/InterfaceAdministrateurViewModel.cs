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
    class InterfaceAdministrateurViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdAdministrateur;

        /* Token */
        private string Token;


        public string LoginAdministrateur {get; set;}


        /* Liste des clients */
        public ObservableCollection<Client> Clients { get; set; }
        public Client SelectedClient { get; set; }

        /* Liste des gestionnaires */
        public ObservableCollection<Gestionnaire> Gestionnaires { get; set; }
        public Gestionnaire SelectedGestionnaire { get; set; }



        /* constructor and initialization */
        public InterfaceAdministrateurViewModel(Window window, int IdAdministrateur, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            Clients = new ObservableCollection<Client>(_restApiQueries.GetClients("Client"));
            Gestionnaires = new ObservableCollection<Gestionnaire>(_restApiQueries.GetGestionnaires("Gestionnaire"));


            Debug.WriteLine("Token flag b : " + Token);

            this.IdAdministrateur = IdAdministrateur;
            this.Token = Token;


            GoToInterfaceAdministrateur = new RelayCommand(
                o => true,
                o => _router.GoToInterfaceAdministrateur(_window, IdAdministrateur, Token)
            );

            GoToAjoutClient = new RelayCommand(
                o => true,
                o => _router.GoToAjoutClient(_window, IdAdministrateur, Token)
            );

            GoToAjoutGestionnaire = new RelayCommand(
                o => true,
                o => _router.GoToAjoutGestionnaire(_window, IdAdministrateur, Token)
            );

            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            ModifierClient = new RelayCommand(
                o => (SelectedClient != null),
                o => _router.GoToModificationClient(_window, IdAdministrateur, Token, SelectedClient)
            );

            SupprimerClient = new RelayCommand(
                o => (SelectedClient != null),
                o => RemoveClient()
            );

            ModifierGestionnaire = new RelayCommand(
                o => (SelectedGestionnaire != null),
                o => _router.GoToModificationGestionnaire(_window, IdAdministrateur, Token, SelectedGestionnaire)
            );

            SupprimerGestionnaire = new RelayCommand(
                o => (SelectedGestionnaire != null),
                o => RemoveGestionnaire()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceAdministrateur { get; private set; }
        public ICommand GoToAjoutClient { get; private set; }
        public ICommand GoToAjoutGestionnaire { get; private set; }
        public ICommand GoToConnexion { get; private set; }
        
        /* Boutons */
        public ICommand ModifierClient { get; private set; }
        public ICommand SupprimerClient { get; private set; }
        public ICommand ModifierGestionnaire { get; private set; }
        public ICommand SupprimerGestionnaire { get; private set; }





        public void SynchroniserClients()
        {
            Clients.Clear();
            
            foreach(Client client in _restApiQueries.GetClients("Client"))
            {
                Clients.Add(client);
            }
        }
        public void SynchroniserGestionnaires()
        {
            Gestionnaires.Clear();

            foreach (Gestionnaire gestionnaire in _restApiQueries.GetGestionnaires("Gestionnaire"))
            {
                Gestionnaires.Add(gestionnaire);
            }
        }


        public void RemoveClient()
        {
            string path = "Client/" + SelectedClient.UtilisateurId;
            Debug.WriteLine("Path : " + path);
            _restApiQueries.Remove(path);
            SynchroniserClients();
        }

        public void RemoveGestionnaire()
        {
            string path = "Gestionnaire/" + SelectedGestionnaire.UtilisateurId;
            Debug.WriteLine("Path : " + path);
            _restApiQueries.Remove(path);
            SynchroniserGestionnaires();
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
