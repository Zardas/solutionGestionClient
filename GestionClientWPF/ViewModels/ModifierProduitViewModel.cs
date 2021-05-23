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
    class ModifierProduitViewModel : INotifyPropertyChanged
    {
        /* reference to the current window */
        private readonly Window _window;

        /* RestAPI */
        private readonly RestApiQueries _restApiQueries;

        /* Router */
        private readonly Router _router;


        /* Administrateur */
        private int IdGestionnaire;

        /* Token */
        private string Token;


        /* Produit */
        private int IdProduit;
        public string NomProduit { get; set; }
        public string DescriptionProduit { get; set; }
        public int PrixProduit { get; set; }
        public string ImageProduit { get; set; }
        public string TypeProduit { get; set; }
        public string ManuelProduit { get; set; }
        public string FabricantProduit { get; set; }
        public int QuantiteProduit { get; set; }
        public int CapaciteProduit { get; set; }

        private bool isValid_produit()
        {
            return (!string.IsNullOrEmpty(NomProduit) &&
                    !string.IsNullOrEmpty(DescriptionProduit) &&
                    PrixProduit > 0 &&
                    !string.IsNullOrEmpty(ImageProduit) &&
                    !string.IsNullOrEmpty(TypeProduit) &&
                    !string.IsNullOrEmpty(ManuelProduit) &&
                    !string.IsNullOrEmpty(FabricantProduit) &&
                    QuantiteProduit > 0 &&
                    CapaciteProduit > 0);
        }


        /* constructor and initialization */
        public ModifierProduitViewModel(Window window, int IdGestionnaire, string Token, Produit produit)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            this.IdGestionnaire = IdGestionnaire;

            this.Token = Token;


            /* Initilisation des valeurs de base du produit */
            IdProduit = produit.ArticleId;
            NomProduit = produit.Nom;
            DescriptionProduit = produit.Description;
            PrixProduit = produit.Prix;
            ImageProduit = produit.Image;
            TypeProduit = produit.Type;
            ManuelProduit = produit.Manuel;
            FabricantProduit = produit.Fabricant;
            QuantiteProduit = produit.Quantite;
            CapaciteProduit = produit.Capacite;


            /* Commandes de routing */
            GoToInterfaceCommercial = new RelayCommand(
                o => true,
                o => _router.GoToInterfaceCommercial(_window, IdGestionnaire, Token)
            );

            GoToAssociationClient = new RelayCommand(
                o => true,
                o => _router.GoToAssociationClient(_window, IdGestionnaire, Token)
            );

            GoToAjoutProduit = new RelayCommand(
                o => true,
                o => _router.GoToAjoutProduit(_window, IdGestionnaire, Token)
            );

            GoToAjoutService = new RelayCommand(
                o => true,
                o => _router.GoToAjoutService(_window, IdGestionnaire, Token)
            );

            GoToAjoutAbonnement = new RelayCommand(
                o => true,
                o => _router.GoToAjoutAbonnement(_window, IdGestionnaire, Token)
            );

            GoToListeTickets = new RelayCommand(
                o => true,
                o => _router.GoToListeTicketsGestionnaire(_window, IdGestionnaire, Token)
            );

            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            /* Boutons */
            ModifierProduitCommand = new RelayCommand(
                o => isValid_produit(),
                o => ModifierProduit()
            );


        }

        /* Menu */
        public ICommand GoToInterfaceCommercial { get; private set; }
        public ICommand GoToAssociationClient { get; private set; }
        public ICommand GoToAjoutProduit { get; private set; }
        public ICommand GoToAjoutService { get; private set; }
        public ICommand GoToAjoutAbonnement { get; private set; }
        public ICommand GoToListeTickets { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand ModifierProduitCommand { get; private set; }






        private void ModifierProduit()
        {

            Produit produit = new Produit()
            {
                Nom = NomProduit,
                Image = ImageProduit,
                Fabricant = FabricantProduit,
                Type = TypeProduit,
                Prix = PrixProduit,
                Quantite = QuantiteProduit,
                Capacite = CapaciteProduit,
                Description = DescriptionProduit,
                Manuel = ManuelProduit,
            };

            string path = "Produit/" + IdProduit;
            _restApiQueries.ModifierProduit(path, produit);
            _router.GoToInterfaceCommercial(_window, IdGestionnaire, Token);
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
