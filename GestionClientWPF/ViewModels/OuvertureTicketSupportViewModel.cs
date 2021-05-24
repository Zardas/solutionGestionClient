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
    class OuvertureTicketSupportViewModel : INotifyPropertyChanged
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

        /* Article concerné */
        private int IdArticle;

        public string Objet { get; set; }
        public string Description { get; set; }

        private bool isValid_addedSupport()
        {
            return (!string.IsNullOrEmpty(Objet) &&
                    !string.IsNullOrEmpty(Description)
                    );
        }


        /* constructor and initialization */
        public OuvertureTicketSupportViewModel(Window window, int IdCompte, string Token, int IdArticle)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdCompte = IdCompte;

            this.Token = Token;

            this.IdArticle = IdArticle;

            Description = "Expliquez de manière concise votre problème";

            Debug.WriteLine("Demande d'ouverture d'un ticket pour l'article " + this.IdArticle);

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
            OuvrirTicketCommand = new RelayCommand(
                o => isValid_addedSupport(),
                o => OuvrirTicket()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceClient { get; private set; }
        public ICommand GoToListeFactures { get; private set; }
        public ICommand GoToListeTicketsClient { get; private set; }
        public ICommand GoToSolde { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand OuvrirTicketCommand { get; private set; }



        public void OuvrirTicket()
        {
            Support support = new Support()
            {
                CompteId = IdCompte,
                Objet = Objet,
                Description = Description
            }; // La date de création est générée coté API

            string path = "Support/Ouvrir/" + IdArticle;
            _restApiQueries.AddSupport(path, support);

        }






        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
