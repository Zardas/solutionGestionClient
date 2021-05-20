using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionClientWPF.Models;


namespace GestionClientWPF.ViewModels
{
    class AccueilViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private RestApiQueries _restApiQueries;

        public string LoginUtilisateur { get; set; }

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

            LoginUtilisateur = _restApiQueries.GetUtilisateurPourConnexion("Utilisateur/Quack,test").Login;
        }






        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
