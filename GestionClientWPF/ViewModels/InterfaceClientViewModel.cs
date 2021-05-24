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
    class InterfaceClientViewModel : INotifyPropertyChanged
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
        public string NomCompte { get; set; }

        public int TotalPanier { get; set; }

        /* Recherche */
        public string StringRecherchee { get; set; }

        /* Liste des articles dans le panier */
        public ObservableCollection<Article> ArticlesPanier { get; set; }
        public Article SelectedArticlePanier { get; set; }


        /* Liste des articles à acheter */
        public ObservableCollection<Article> ArticlesDisponibles { get; set; }
        public Article SelectedArticleDisponible { get; set; }



        /* constructor and initialization */
        public InterfaceClientViewModel(Window window, int IdCompte, string Token)
        {
            _window = window;

            _restApiQueries = new RestApiQueries(Token);

            _router = new Router();

            string path = "Compte/" + IdCompte;
            Compte = _restApiQueries.GetSpecificCompte(path);

            // Initialisation des listes (%20 = " ")
            path = "Article/Panier/" + Compte.CompteId + "/%20";
            ArticlesPanier = new ObservableCollection<Article>(_restApiQueries.GetArticle(path));

            path = "Article/Disponibles/%20";
            ArticlesDisponibles = new ObservableCollection<Article>(_restApiQueries.GetArticle(path));

            SynchroniserTotalPanier();

            this.IdCompte = IdCompte;
            this.Token = Token;

            NomCompte = Compte.NomCompte;

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

            /* Action */
            GoToConnexion = new RelayCommand(
                o => true,
                o => _router.GoToConnexion(_window)
            );

            RechercheCommand = new RelayCommand(
                o => !string.IsNullOrEmpty(StringRecherchee),
                o => Recherche(StringRecherchee)
            );

            OuvrirTicket = new RelayCommand(
                o => (SelectedArticlePanier != null),
                o => Debug.WriteLine("TODO")
            );

            RetirerArticle = new RelayCommand(
                o => (SelectedArticlePanier != null),
                o => EnlevementArticlePanier()
            );

            AjouterArticle = new RelayCommand(
                o => (SelectedArticleDisponible != null),
                o => AjoutArticlePanier()
            );

            GenererFacture = new RelayCommand(
                o => true,
                o => GenerationFacture()
            );

        }

        /* Menu */
        public ICommand GoToInterfaceClient { get; private set; }
        public ICommand GoToListeFactures { get; private set; }
        public ICommand GoToListeTicketsClient { get; private set; }
        public ICommand GoToSolde { get; private set; }
        public ICommand GoToConnexion { get; private set; }

        /* Boutons */
        public ICommand RechercheCommand { get; private set; }
        public ICommand OuvrirTicket { get; private set; }
        public ICommand RetirerArticle { get; private set; }
        public ICommand AjouterArticle { get; private set; }
        public ICommand GenererFacture { get; private set; }



        public void SynchroniserTotalPanier()
        {
            TotalPanier = 0;

            foreach(Article article in ArticlesPanier)
            {
                TotalPanier += article.Prix;
            }
            // On dit au système que la variable a été modifiée
            OnPropertyChanged("TotalPanier");
        }


        public void SynchroniserPanier()
        {
            ArticlesPanier.Clear();

            string path = "Article/Panier/" + IdCompte + "/%20";
            foreach (Article article in _restApiQueries.GetArticle(path))
            {
                ArticlesPanier.Add(article);
            }
            SynchroniserTotalPanier();
        }
        public void SynchroniserPanier(string StringRecherchee)
        {
            ArticlesPanier.Clear();

            string path = "Article/Panier/" + IdCompte + "/" + StringRecherchee;
            foreach (Article article in _restApiQueries.GetArticle(path))
            {
                ArticlesPanier.Add(article);
            }
        }

        public void SynchroniserDisponibles()
        {
            ArticlesDisponibles.Clear();

            string path = "Article/Disponibles/%20";
            foreach (Article article in _restApiQueries.GetArticle(path))
            {
                ArticlesDisponibles.Add(article);
            }
        }
        public void SynchroniserDisponibles(string StringRecherchee)
        {
            ArticlesDisponibles.Clear();

            string path = "Article/Disponibles/" + StringRecherchee;
            foreach (Article article in _restApiQueries.GetArticle(path))
            {
                ArticlesDisponibles.Add(article);
            }
        }





        
        public void Recherche(string recherche)
        {
            SynchroniserPanier(recherche);
            SynchroniserDisponibles(recherche);
            SynchroniserTotalPanier();
        }


        public void AjoutArticlePanier()
        {
            string path = "Panier/" + Compte.CompteId;
            Panier panier = _restApiQueries.GetSpecificPanier(path);
            path = "Article/AjoutPanier/" + SelectedArticleDisponible.ArticleId;
            _restApiQueries.ModifierArticle(path, panier.PanierId);
            SynchroniserPanier();
            SynchroniserDisponibles();
        }
        public void EnlevementArticlePanier()
        {
            string path = "Panier/" + Compte.CompteId;
            Panier panier = _restApiQueries.GetSpecificPanier(path);
            path = "Article/EnlevementPanier/" + SelectedArticlePanier.ArticleId;
            _restApiQueries.ModifierArticle(path, panier.PanierId);
            SynchroniserPanier();
            SynchroniserDisponibles();
        }


        public void GenerationFacture()
        {
            string path = "Client/Solde/" + Compte.CompteId;
            int solde = _restApiQueries.GetClientSolde(path);

            if(solde < TotalPanier)
            {
                MessageBox.Show("Solde insuffisant (" + solde + " cad actuellement)", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                path = "Facture/Generer";
                _restApiQueries.GenererFacture(path, Compte.CompteId);
                Debug.WriteLine("Facture générée");
            }
            SynchroniserPanier();
            SynchroniserDisponibles();
        }


        /* definition of PropertyChanged */
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
