using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;


namespace gestionRelationClient.ViewModels
{
    class AccueilViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;



        /* constructor and initialization */
        public AccueilViewModel(Window window)
        {
            _window = window;



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
            //MessageBox.Show("flag");
        }

        private void OpenGestionnaire()
        {
            /*ConnexionClient connexionClient = new ConnexionClient();
            connexionClient.Show();
            _window.Close();*/
        }

        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
