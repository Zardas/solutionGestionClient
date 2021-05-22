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

    class ConnexionViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private RestApiQueries _restApiQueries;

        /* Router */
        private Router _router;

        /* Champs pour la connexion */
        public string LoginConnexion { get; set; }
        public string MotDePasseConnexion { get; set; }


        private bool is_identififantsValides()
        {
            return (!string.IsNullOrEmpty(LoginConnexion) &&
                    !string.IsNullOrEmpty(MotDePasseConnexion));
        }



        /* Tests */
        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                return _clients;
            }
        }


        /* constructor and initialization */
        public ConnexionViewModel(Window window)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(null);

            _router = new Router();

            _clients = new ObservableCollection<Client>(_restApiQueries.GetClients("Client"));

            

            ConnexionClientCommand = new RelayCommand(
                o => is_identififantsValides(),
                o => Connexion()
            );

            GoToClientInscription = new RelayCommand(
                o => true,
                o => _router.GoToInscription(_window)
            );
        }

        public ICommand ConnexionClientCommand { get; private set; }
        public ICommand GoToClientInscription { get; private set; }


        private void Connexion()
        {
            string path = "Utilisateur/" + LoginConnexion + "," + MotDePasseConnexion;
            Debug.WriteLine("Chemin : " + path);

            RestApiQueries.ResultConnexion resultsConnexion = _restApiQueries.GetUtilisateurPourConnexion(path);

            if (resultsConnexion != null) {

                // TODO update statut connexion

                Debug.WriteLine("Type : " + resultsConnexion.Type);
                //GestionClientWPF.Models.Utilisateur

                switch (resultsConnexion.Type)
                {
                    case "Client":

                        _router.GoToListeCompteClient(_window, resultsConnexion.Id, resultsConnexion.Token);
                        break;

                    case "Gestionnaire":

                        _router.GoToInterfaceCommercial(_window, resultsConnexion.Id, resultsConnexion.Token);
                        break;

                    case "Administrateur":

                        _router.GoToInterfaceAdministrateur(_window, resultsConnexion.Id, resultsConnexion.Token);
                        break;

                    default:
                        _router.GoToConnexion(_window);
                        break;
                }

            } else
            {
                _router.GoToConnexion(_window);
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
