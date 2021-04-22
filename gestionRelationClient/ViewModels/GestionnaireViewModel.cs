using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class GestionnaireViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;


        

        /* Le client qui s'inscrit */
        private ObservableCollection<Models.Gestionnaire> _gestionnaires;
        public ObservableCollection<Models.Gestionnaire> Gestionnaire
        {
            get
            {
                return _gestionnaires;
            }
        }

        private Models.Gestionnaire _addedGestionnaire;
        private bool _isValid_addedGestionnaire;





        /* La liste des rôles */
        public ObservableCollection<Models.Role> Roles { get; set; }
        public string NouveauRole { get; set; }



        // Lien à tout les champs de l'inscription, mais avec une seul conditin de validité (qu'ils soient TOUS valides)
        public string LoginInscription
        {
            get
            {
                return _addedGestionnaire.Login;
            }
            set
            {
                if (_addedGestionnaire.Login != value)
                {
                    _addedGestionnaire.Login = value;
                    SetIsValid_addedGestionnaire();
                }
            }
        }
        public string MailInscription
        {
            get
            {
                return _addedGestionnaire.Email;
            }
            set
            {
                if (_addedGestionnaire.Email != value)
                {
                    _addedGestionnaire.Email = value;
                    SetIsValid_addedGestionnaire();
                }
            }
        }
        public string NomInscription
        {
            get
            {
                return _addedGestionnaire.NomGestionnaire;
            }
            set
            {
                if (_addedGestionnaire.NomGestionnaire != value)
                {
                    _addedGestionnaire.NomGestionnaire = value;
                    SetIsValid_addedGestionnaire();
                }
            }
        }
        public Models.Role RoleInscription
        {
            get
            {
                return _addedGestionnaire.Role;
            }
            set
            {
                if (_addedGestionnaire.Role != value)
                {
                    _addedGestionnaire.Role = value;
                    SetIsValid_addedGestionnaire();
                }
            }
        }
        public string MotDePasseInscription
        {
            get
            {
                return _addedGestionnaire.MotDePasse;
            }
            set
            {
                if (_addedGestionnaire.MotDePasse != value)
                {
                    _addedGestionnaire.MotDePasse = value;
                    SetIsValid_addedGestionnaire();
                }
            }
        }


        private void SetIsValid_addedGestionnaire()
        {
            _isValid_addedGestionnaire = (!string.IsNullOrEmpty(LoginInscription) &&
                                    !string.IsNullOrEmpty(MailInscription) &&
                                    Models.Utilitaire.IsValidEmail(MailInscription) &&
                                    !string.IsNullOrEmpty(NomInscription) &&
                                    !string.IsNullOrEmpty(MotDePasseInscription) &&
                                    !(RoleInscription.Title == "Role bidon") &&
                                    !string.IsNullOrEmpty(RoleInscription.Title));
        }








        /* Le client qui se connecte */
        private Models.Gestionnaire _connectingGestionnaire;
        private bool _isValid_connectingGestionnaire;

        public string LoginConnexion
        {
            get
            {
                return _connectingGestionnaire.Login;
            }
            set
            {
                if (_connectingGestionnaire.Login != value)
                {
                    _connectingGestionnaire.Login = value;
                    SetIsValid_connectingGestionnaire();
                }
            }
        }
        public string MotDePasseConnexion
        {
            get
            {
                return _connectingGestionnaire.MotDePasse;
            }
            set
            {
                if (_connectingGestionnaire.MotDePasse != value)
                {
                    _connectingGestionnaire.MotDePasse = value;
                    SetIsValid_connectingGestionnaire();
                }
            }
        }
        private void SetIsValid_connectingGestionnaire()
        {
            _isValid_connectingGestionnaire = (!string.IsNullOrEmpty(LoginConnexion) &&
                                    !string.IsNullOrEmpty(MotDePasseConnexion));
        }











        public GestionnaireViewModel(Window window)
        {
            _window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            _gestionnaires = new ObservableCollection<Models.Gestionnaire>();


            _addedGestionnaire = new Models.Gestionnaire();
            _connectingGestionnaire = new Models.Gestionnaire();

            Roles = Models.Utilitaire.ToObservableCollection(DBContext.Roles);

            // On initialise le rôle sinon ça bug lors du test
            _addedGestionnaire.Role = new Models.Role()
            {
                Title = "Role bidon"
            };
            
            // Inscription
            InscriptionGestionnaireCommand = new RelayCommand(
                o => _isValid_addedGestionnaire,
                o => AddGestionnaire()
            );

            // Connexion
            ConnexionGestionnaireCommand = new RelayCommand(
                o => _isValid_connectingGestionnaire,
                o => Connectgestionnaire()
            );

            // Ajout rôle
            AjoutRoleCommand = new RelayCommand(
                o => (NouveauRole != null),
                o => AjoutRole()
            );


            // Navigation
            GoToGestionnaireInscription = new RelayCommand(
                o => true,
                o => OpenGestionnaireInscription()
            );
            GoToGestionnaireConnexion = new RelayCommand(
                o => true,
                o => OpenGestionnaireConnexion()
            );

        }

        /* definition of the commands */
        public ICommand InscriptionGestionnaireCommand { get; private set; }
        public ICommand ConnexionGestionnaireCommand { get; private set; }
        public ICommand GoToGestionnaireInscription { get; private set; }
        public ICommand GoToGestionnaireConnexion { get; private set; }
        public ICommand AjoutRoleCommand { get; private set; }


        // Inscription
        private void AddGestionnaire()
        {
            Models.Stock stock = new Models.Stock()
            {
                Titre = ("Stock de " + _addedGestionnaire.NomGestionnaire)
            };
            DBContext.SaveChanges();

            _addedGestionnaire.Stock = stock;
            _addedGestionnaire.LoginStatus = "initial";

            DBContext.Add(_addedGestionnaire);
            DBContext.SaveChanges();

            this.OpenGestionnaireConnexion();
        }


        // Connexion
        public void Connectgestionnaire()
        {
            // TODO : trouver un moyen pour faire un Where sur la table Gestionnaire (qui n'est pas dans le DBContext)
            Models.Utilisateur gestionnaireATrouver = DBContext.Utilisateurs.Where(c => (c.Login.Equals(_connectingGestionnaire.Login) && c.MotDePasse.Equals(_connectingGestionnaire.MotDePasse))).FirstOrDefault();

            if (gestionnaireATrouver == null)
            {
                MessageBox.Show("Information de connexion invalides", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.OpenPageGestionnaire(gestionnaireATrouver);
            }

        }


        // AjoutRole
        public void AjoutRole()
        {
            Models.Role roleA_ajouter = new Models.Role()
            {
                Title = NouveauRole
            };

            DBContext.Roles.Add(roleA_ajouter);
            DBContext.SaveChanges();

            Roles.Add(roleA_ajouter);
        }



        // Navigation
        private void OpenGestionnaireInscription()
        {
            Views.InscriptionGestionnaire inscriptionGestionnaire = new Views.InscriptionGestionnaire();
            inscriptionGestionnaire.Show();
            _window.Close();
        }

        private void OpenGestionnaireConnexion()
        {
            Views.ConnexionGestionnaire connexionGestionnaire = new Views.ConnexionGestionnaire();
            connexionGestionnaire.Show();
            _window.Close();
        }

        private void OpenPageGestionnaire(Models.Utilisateur gestionnaire)
        {

            // On change le status Login de l'Utilisateur
            gestionnaire.Connexion();
            DBContext.SaveChanges();


            Views.PageGestionnaire pageGestionnaire = new Views.PageGestionnaire(gestionnaire.Id);
            pageGestionnaire.Show();
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
