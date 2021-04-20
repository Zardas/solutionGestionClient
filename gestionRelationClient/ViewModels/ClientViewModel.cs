using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class ClientViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;


        /* Le client qui s'inscrit */
        private ObservableCollection<Models.Client> _clients;
        public ObservableCollection<Models.Client> Clients
        {
            get
            {
                return _clients;
            }
        }

        private Models.Client _addedClient;
        private bool _isValid_addedClient;










        // Lien à tout les champs de l'inscription, mais avec une seul conditin de validité (qu'ils soient TOUS valides)
        public string LoginInscription
        {
            get
            {
                return _addedClient.Login;
            }
            set
            {
                if (_addedClient.Login != value)
                {
                    _addedClient.Login = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public string MailInscription
        {
            get
            {
                return _addedClient.Mail;
            }
            set
            {
                if (_addedClient.Mail != value)
                {
                    _addedClient.Mail = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public string NomInscription
        {
            get
            {
                return _addedClient.Nom;
            }
            set
            {
                if (_addedClient.Nom != value)
                {
                    _addedClient.Nom = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public string PrenomInscription
        {
            get
            {
                return _addedClient.Prenom;
            }
            set
            {
                if (_addedClient.Prenom != value)
                {
                    _addedClient.Prenom = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public string MotDePasseInscription
        {
            get
            {
                return _addedClient.MotDePasse;
            }
            set
            {
                if (_addedClient.MotDePasse != value)
                {
                    _addedClient.MotDePasse = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public string TelephoneInscription
        {
            get
            {
                return _addedClient.Telephone;
            }
            set
            {
                if (_addedClient.Telephone != value)
                {
                    _addedClient.Telephone = value;
                    SetIsValid_AddedClient();
                }
            }
        }
        public int AgeInscription
        {

            get
            {
                return _addedClient.Age;
            }
            set
            {

                /* int newAge = 0;
                bool isANumber = int.TryParse(value, out newAge); TODO */
                if (_addedClient.Age != value)
                {
                    _addedClient.Age = value;
                    SetIsValid_AddedClient();
                }
            }
        }


        private void SetIsValid_AddedClient()
        {
            _isValid_addedClient = (!string.IsNullOrEmpty(LoginInscription) &&
                                    !string.IsNullOrEmpty(MailInscription) &&
                                    Models.Utilitaire.IsValidEmail(MailInscription) &&
                                    !string.IsNullOrEmpty(NomInscription) &&
                                    !string.IsNullOrEmpty(PrenomInscription) &&
                                    !string.IsNullOrEmpty(MotDePasseInscription) &&
                                    !string.IsNullOrEmpty(TelephoneInscription) &&
                                    AgeInscription > 6);
        }








        /* Le client qui se connecte */
        private Models.Client _connectingClient;
        private bool _isValid_connectingClient;

        public string LoginConnexion
        {
            get
            {
                return _connectingClient.Login;
            }
            set
            {
                if (_connectingClient.Login != value)
                {
                    _connectingClient.Login = value;
                    SetIsValid_ConnectingClient();
                }
            }
        }
        public string MotDePasseConnexion
        {
            get
            {
                return _connectingClient.MotDePasse;
            }
            set
            {
                if (_connectingClient.MotDePasse != value)
                {
                    _connectingClient.MotDePasse = value;
                    SetIsValid_ConnectingClient();
                }
            }
        }
        private void SetIsValid_ConnectingClient()
        {
            _isValid_connectingClient = (!string.IsNullOrEmpty(LoginConnexion) &&
                                    !string.IsNullOrEmpty(MotDePasseConnexion));
        }











        public ClientViewModel(Window window)
        {
            _window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            _clients = new ObservableCollection<Models.Client>();
            _addedClient = new Models.Client();

            _connectingClient = new Models.Client();

            // Inscription
            InscriptionClientCommand = new RelayCommand(
                o => _isValid_addedClient,
                o => AddClient()
            );

            //Connexion
            ConnexionClientCommand = new RelayCommand(
                o => _isValid_connectingClient,
                o => ConnectClient()
            );


            // Navigation
            GoToClientInscription = new RelayCommand(
                o => true,
                o => OpenClientInscription()
            );
            GoToClientConnexion = new RelayCommand(
                o => true,
                o => OpenClientConnexion()
            );

        }

        /* definition of the commands */
        public ICommand InscriptionClientCommand { get; private set; }
        public ICommand ConnexionClientCommand { get; private set; }
        public ICommand GoToClientInscription { get; private set; }
        public ICommand GoToClientConnexion { get; private set; }


        // Inscription
        private void AddClient()
        {
            DBContext.Add(new Models.Client()
            {
                Id = this.Clients.Count(),
                Login = _addedClient.Login,
                LoginStatus = "initial",
                Nom = _addedClient.Nom,
                Prenom = _addedClient.Prenom,
                Mail = _addedClient.Mail,
                Telephone = _addedClient.Telephone,
                MotDePasse = _addedClient.MotDePasse,
                Age = _addedClient.Age
            });
            DBContext.SaveChanges();

            this.OpenClientConnexion();
                // Version sans la BDD
            /*Clients.Add(new Models.Client()
            {
                Id = this.Clients.Count(),
                Login = _addedClient.Login,
                LoginStatus = "online",
                Nom = _addedClient.Nom,
                Prenom = _addedClient.Prenom,
                Mail = _addedClient.Mail,
                Telephone = _addedClient.Telephone,
                Age = _addedClient.Age
            });*/
        }


        // Connexion
        public void ConnectClient()
        {
            // TODO : trouver un moyen pour faire un Where sur la table Client (qui n'est pas dans le DBContext)
            Models.Utilisateur clientATrouver = DBContext.Utilisateurs.Where(c => (c.Login.Equals(_connectingClient.Login) && c.MotDePasse.Equals(_connectingClient.MotDePasse))).FirstOrDefault();

            if(clientATrouver == null)
            {
                MessageBox.Show("Information de connexion invalides", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                this.OpenListeCompteClient(clientATrouver);
            }
            
        }



        // Navigation
        private void OpenClientInscription()
        {
            Views.InscriptionClient inscriptionClient = new Views.InscriptionClient();
            inscriptionClient.Show();
            _window.Close();
        }

        private void OpenClientConnexion()
        {
            Views.ConnexionClient connexionClient = new Views.ConnexionClient();
            connexionClient.Show();
            _window.Close();
        }

        private void OpenListeCompteClient(Models.Utilisateur client)
        {
            // On change le status Login de l'Utilisateur
            client.Connexion();
            DBContext.SaveChanges();


            Views.ListeCompteClient listeCompteClient = new Views.ListeCompteClient(client);
            listeCompteClient.Show();
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
