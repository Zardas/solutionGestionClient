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
    class AjoutGestionnaireViewModel : INotifyPropertyChanged
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


        public ObservableCollection<Role> Roles { get; set; }

        public string NouveauRole { get; set; }


        /* Variables pour l'ajout du gestionnaire */
        public string LoginInscription { get; set; }
        public string MailInscription { get; set; }
        public string NomInscription { get; set; }
        public string MotDePasseInscription { get; set; }
        public Role RoleInscription { get; set; }
        public bool isValidAjoutGestionnaire()
        {
            return (!string.IsNullOrEmpty(LoginInscription) &&
                    !string.IsNullOrEmpty(MailInscription) &&
                    Utilitaires.IsValidEmail(MailInscription) &&
                    !string.IsNullOrEmpty(NomInscription) &&
                    !string.IsNullOrEmpty(MotDePasseInscription) &&
                    !(RoleInscription == null)
                   );
        }



        /* constructor and initialization */
        public AjoutGestionnaireViewModel(Window window, int IdAdministrateur, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();


            this.IdAdministrateur = IdAdministrateur;
            this.Token = Token;

            Roles = new ObservableCollection<Role>(_restApiQueries.GetRoles("Role")); 

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

            AjouterGestionnaire = new RelayCommand(
                o => isValidAjoutGestionnaire(),
                o => AjoutGestionnaire()
            );

            AjouterRoleCommand = new RelayCommand(
                o => (!string.IsNullOrEmpty(NouveauRole)),
                o => AjoutRole()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceAdministrateur { get; private set; }
        public ICommand GoToAjoutClient { get; private set; }
        public ICommand GoToAjoutGestionnaire { get; private set; }
        public ICommand GoToConnexion { get; private set; }
        public ICommand AjouterGestionnaire { get; private set; }

        public ICommand AjouterRoleCommand { get; private set; }


        public void AjoutGestionnaire()
        {
            Gestionnaire gestionnaire = new Gestionnaire()
            {
                Login = LoginInscription,
                Email = MailInscription,
                NomGestionnaire = NomInscription,
                MotDePasse = MotDePasseInscription,
                RoleId = RoleInscription.RoleId
            };

            string path = "Gestionnaire/";
            _restApiQueries.AddGestionnaire(path, gestionnaire);
            _router.GoToInterfaceAdministrateur(_window, IdAdministrateur, Token);
        }


        public void AjoutRole()
        {
            Role role = new Role()
            {
                Title = NouveauRole
            };

            string path = "Role/";
            _restApiQueries.AddRole(path, role);
            SynchroniserRoles();
            
        }

        public void SynchroniserRoles()
        {
            Roles.Clear();

            foreach (Role role in _restApiQueries.GetRoles("Role"))
            {
                Roles.Add(role);
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
