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
    class ModifierGestionnaireViewModel : INotifyPropertyChanged
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


        private Gestionnaire Gestionnaire;


        public ObservableCollection<Role> Roles { get; set; }

        public string NouveauRole { get; set; }


        /* Variables pour l'ajout du gestionnaire */
        public string LoginModification { get; set; }
        public string MailModification { get; set; }
        public string NomModification { get; set; }
        public Role RoleModification { get; set; }
        public bool isValidModificationGestionnaire()
        {
            return (!string.IsNullOrEmpty(LoginModification) &&
                    !string.IsNullOrEmpty(MailModification) &&
                    Utilitaires.IsValidEmail(MailModification) &&
                    !string.IsNullOrEmpty(NomModification) &&
                    !(RoleModification == null)
                   );
        }



        /* constructor and initialization */
        public ModifierGestionnaireViewModel(Window window, int IdAdministrateur, string Token, Gestionnaire gestionnaire)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdAdministrateur = IdAdministrateur;
            this.Token = Token;

            this.Gestionnaire = gestionnaire;

            LoginModification = gestionnaire.Login;
            MailModification = gestionnaire.Email;
            NomModification = gestionnaire.NomGestionnaire;
            RoleModification = gestionnaire.Role;


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

            ModifierGestionnaire = new RelayCommand(
                o => isValidModificationGestionnaire(),
                o => ModificationGestionnaire()
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
        public ICommand ModifierGestionnaire { get; private set; }

        public ICommand AjouterRoleCommand { get; private set; }


        public void ModificationGestionnaire()
        {
            Gestionnaire gestionnaire = new Gestionnaire()
            {
                Login = LoginModification,
                Email = MailModification,
                NomGestionnaire = NomModification,
                RoleId = RoleModification.RoleId
            };

            string path = "Gestionnaire/" + this.Gestionnaire.UtilisateurId;
            _restApiQueries.ModifierGestionnaire(path, gestionnaire);
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
