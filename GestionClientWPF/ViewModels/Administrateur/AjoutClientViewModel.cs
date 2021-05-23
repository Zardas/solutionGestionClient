using System;
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
    class AjoutClientViewModel : INotifyPropertyChanged
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





        /* Variables pour l'ajout du client */
        public string LoginInscription { get; set; }
        public string MailInscription { get; set; }
        public string NomInscription { get; set; }
        public string PrenomInscription { get; set; }
        public string MotDePasseInscription { get; set; }
        public string TelephoneInscription { get; set; }
        public string AgeInscription { get; set; }
        public bool isValidAjoutClient()
        {
            return (!string.IsNullOrEmpty(LoginInscription) &&
                    !string.IsNullOrEmpty(MailInscription) &&
                    Utilitaires.IsValidEmail(MailInscription) &&
                    !string.IsNullOrEmpty(NomInscription) &&
                    !string.IsNullOrEmpty(PrenomInscription) &&
                    !string.IsNullOrEmpty(MotDePasseInscription) &&
                    !string.IsNullOrEmpty(TelephoneInscription) &&
                    !string.IsNullOrEmpty(AgeInscription) &&
                    Int32.Parse(AgeInscription) > 16 );
        }



        /* constructor and initialization */
        public AjoutClientViewModel(Window window, int IdAdministrateur, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();


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

            AjouterClient = new RelayCommand(
                o => isValidAjoutClient(),
                o => AjoutClient()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceAdministrateur { get; private set; }
        public ICommand GoToAjoutClient { get; private set; }
        public ICommand GoToAjoutGestionnaire { get; private set; }
        public ICommand GoToConnexion { get; private set; }
        public ICommand AjouterClient { get; private set; }



        public void AjoutClient()
        {
            Client client = new Client()
            {
                Login = LoginInscription,
                Mail = MailInscription,
                Nom = NomInscription,
                Prenom = PrenomInscription,
                MotDePasse = MotDePasseInscription,
                Telephone = TelephoneInscription,
                Age = Int32.Parse(AgeInscription)
            };

            string path = "Client/";
            _restApiQueries.AddClient(path, client);
            _router.GoToInterfaceAdministrateur(_window, IdAdministrateur, Token);
        }



        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
