using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using gestionRelationClient.Models;

namespace gestionRelationClient.ViewModels
{
    class AccueilViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private RestApiQueries _restApiQueries;



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
        public AccueilViewModel(Window window)
        {
            _window = window;

            _restApiQueries = new RestApiQueries();

            _clients = new ObservableCollection<Client>(_restApiQueries.GetClients("Client"));


            GoToClientCommand = new RelayCommand(
                o => true,
                o => OpenClientConnexion()
            );

            GoToGestionnaireCommand = new RelayCommand(
                o => true,
                o => OpenGestionnaire()
            );
        }

        /* definition of the commands */
        public ICommand GoToClientCommand { get; private set; }
        public ICommand GoToGestionnaireCommand { get; private set; }



        private void OpenClientConnexion()
        {
            Views.ConnexionClient connexionClient = new Views.ConnexionClient();
            connexionClient.Show();
            _window.Close();
        }

        private void OpenGestionnaire()
        {
            Views.ConnexionGestionnaire connexionGestionnaire = new Views.ConnexionGestionnaire();
            connexionGestionnaire.Show();
            _window.Close();
        }

        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
