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
    class AjoutSoldeViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdCompte;

        /* Token */
        private string Token;

        private Compte Compte { get; set; }

        public int Solde { get; set; }
        public int Montant { get; set; }



        /* constructor and initialization */
        public AjoutSoldeViewModel(Window window, int IdCompte, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            string path = "Compte/" + IdCompte;
            Compte = _restApiQueries.GetSpecificCompte(path);

            // Initialisation des listes (%20 = " ")
            path = "Client/Solde/" + Compte.CompteId;
            Solde = _restApiQueries.GetClientSolde(path);


            this.IdCompte = IdCompte;
            this.Token = Token;

            /* Routing */
            GoToInterfaceClient = new RelayCommand(
                o => true,
                o => _router.GoToInterfaceClient(_window, IdCompte, Token)
            );

            GoToListeFactures = new RelayCommand(
                o => true,
                o => _router.GoToListeFactures(_window, IdCompte, Token)
            );

            GoToListeTicketsClient = new RelayCommand(
                o => true,
                o => _router.GoToListeTicketsClient(_window, IdCompte, Token)
            );

            GoToSolde = new RelayCommand(
                o => true,
                o => _router.GoToSoldeClient(_window, IdCompte, Token)
            );

            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            /* Action */
            AjoutSoldeCommand = new RelayCommand(
                o => (Montant != 0),
                o => AjoutSolde()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceClient { get; private set; }
        public ICommand GoToListeFactures { get; private set; }
        public ICommand GoToListeTicketsClient { get; private set; }
        public ICommand GoToSolde { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand AjoutSoldeCommand { get; private set; }




        public void AjoutSolde()
        {
            string path = "Client/Solde/" + Compte.CompteId;
            _restApiQueries.ModifierClientSolde(path, Montant);
            Solde += Montant;
            OnPropertyChanged("Solde");
        }





        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
