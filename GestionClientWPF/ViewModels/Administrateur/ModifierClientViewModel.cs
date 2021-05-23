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
    class ModifierClientViewModel : INotifyPropertyChanged
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


        private Client client { get; set; }


        /* Variables pour l'ajout du client */
        public string LoginModification { get; set; }
        public string MailModification { get; set; }
        public string NomModification { get; set; }
        public string PrenomModification { get; set; }
        public string TelephoneModification { get; set; }
        public string AgeModification { get; set; }
        public bool isValidModificationClient()
        {
            return (!string.IsNullOrEmpty(LoginModification) &&
                    !string.IsNullOrEmpty(MailModification) &&
                    Utilitaires.IsValidEmail(MailModification) &&
                    !string.IsNullOrEmpty(NomModification) &&
                    !string.IsNullOrEmpty(PrenomModification) &&
                    !string.IsNullOrEmpty(TelephoneModification) &&
                    !string.IsNullOrEmpty(AgeModification) &&
                    Int32.Parse(AgeModification) > 16);
        }



        /* constructor and initialization */
        public ModifierClientViewModel(Window window, int IdAdministrateur, string Token, Client client)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();


            this.IdAdministrateur = IdAdministrateur;
            this.Token = Token;

            Debug.WriteLine("FLAG C : " + client.UtilisateurId);

            this.client = client;

            LoginModification = client.Login;
            MailModification = client.Mail;
            NomModification = client.Nom;
            PrenomModification = client.Prenom;
            TelephoneModification = client.Telephone;
            AgeModification = client.Age.ToString();



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
                o => isValidModificationClient(),
                o => ModificationClient()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceAdministrateur { get; private set; }
        public ICommand GoToAjoutClient { get; private set; }
        public ICommand GoToAjoutGestionnaire { get; private set; }
        public ICommand GoToConnexion { get; private set; }
        public ICommand ModifierClient { get; private set; }



        public void ModificationClient()
        {
            Client clientModif = new Client()
            {
                Login = LoginModification,
                Mail = MailModification,
                Nom = NomModification,
                Prenom = PrenomModification,
                Telephone = TelephoneModification,
                Age = Int32.Parse(AgeModification)
            };

            string path = "Client/" + this.client.UtilisateurId;
            Debug.WriteLine("Path : " + path);
            _restApiQueries.ModifierClient(path, clientModif);
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
