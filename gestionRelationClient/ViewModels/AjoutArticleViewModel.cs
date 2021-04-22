using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace gestionRelationClient.ViewModels
{
    class AjoutArticleViewModel
    {
        /* reference to the current window */
        private readonly Window _window;

        DatabaseContext.GestionRelationClient_DBContext DBContext;



        private bool _isValid_addedService;
        private bool _isValid_addedProduit;

        private Models.Utilisateur Gestionnaire;


        /* Ajout service */
        private Models.Service _addedService;
        public string NomService
        {
            get
            {
                return _addedService.Nom;
            }
            set
            {
                if (_addedService.Nom != value)
                {
                    _addedService.Nom = value;
                    SetIsValidAddedService();
                }
            }
        }
        public string DescriptionService
        {
            get
            {
                return _addedService.Description;
            }
            set
            {
                if (_addedService.Description != value)
                {
                    _addedService.Description = value;
                    SetIsValidAddedService();
                }
            }
        }
        public int PrixService
        {
            get
            {
                return _addedService.Prix;
            }
            set
            {
                if (_addedService.Prix != value)
                {
                    _addedService.Prix = value;
                    SetIsValidAddedService();
                }
            }
        }
        public string ImageService
        {
            get
            {
                return _addedService.Image;
            }
            set
            {
                if (_addedService.Image != value)
                {
                    _addedService.Image = value;
                    SetIsValidAddedService();
                }
            }
        }
        public string TypeService
        {
            get
            {
                return _addedService.Type;
            }
            set
            {
                if (_addedService.Type != value)
                {
                    _addedService.Type = value;
                    SetIsValidAddedService();
                }
            }
        }
        public string ManuelService
        {
            get
            {
                return _addedService.Manuel;
            }
            set
            {
                if (_addedService.Manuel != value)
                {
                    _addedService.Manuel = value;
                    SetIsValidAddedService();
                }
            }
        }

        /*public string ConditionService
        {
            get
            {
                return _addedService.Conditions;
            }
            set
            {
                if (_addedService.Conditions != value)
                {
                    _addedService.Conditions = value;
                    SetIsValidAddedService();
                }
            }
        }*/



        private void SetIsValidAddedService()
        {
            _isValid_addedService = (!string.IsNullOrEmpty(NomService) &&
                                     !string.IsNullOrEmpty(DescriptionService) &&
                                     PrixService > 0 &&
                                     !string.IsNullOrEmpty(ImageService) &&
                                     !string.IsNullOrEmpty(TypeService) &&
                                     !string.IsNullOrEmpty(ManuelService)
                                     /*!string.IsNullOrEmpty(ConditionService)*/);
        }







        /* Ajout produit */
        private Models.Produit _addedProduit;
        public string NomProduit
        {
            get
            {
                return _addedProduit.Nom;
            }
            set
            {
                if (_addedProduit.Nom != value)
                {
                    _addedProduit.Nom = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public string DescriptionProduit
        {
            get
            {
                return _addedProduit.Description;
            }
            set
            {
                if (_addedProduit.Description != value)
                {
                    _addedProduit.Description = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public int PrixProduit
        {
            get
            {
                return _addedProduit.Prix;
            }
            set
            {
                if (_addedProduit.Prix != value)
                {
                    _addedProduit.Prix = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public string ImageProduit
        {
            get
            {
                return _addedProduit.Image;
            }
            set
            {
                if (_addedProduit.Image != value)
                {
                    _addedProduit.Image = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public string TypeProduit
        {
            get
            {
                return _addedProduit.Type;
            }
            set
            {
                if (_addedProduit.Type != value)
                {
                    _addedProduit.Type = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public string ManuelProduit
        {
            get
            {
                return _addedProduit.Manuel;
            }
            set
            {
                if (_addedProduit.Manuel != value)
                {
                    _addedProduit.Manuel = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public string FabricantProduit
        {
            get
            {
                return _addedProduit.Fabricant;
            }
            set
            {
                if (_addedProduit.Fabricant != value)
                {
                    _addedProduit.Fabricant = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public int QuantiteProduit
        {
            get
            {
                return _addedProduit.Quantite;
            }
            set
            {
                if (_addedProduit.Quantite != value)
                {
                    _addedProduit.Quantite = value;
                    SetIsValidAddedProduit();
                }
            }
        }
        public int CapaciteProduit
        {
            get
            {
                return _addedProduit.Capacite;
            }
            set
            {
                if (_addedProduit.Capacite != value)
                {
                    _addedProduit.Capacite = value;
                    SetIsValidAddedProduit();
                }
            }
        }

        private void SetIsValidAddedProduit()
        {
            _isValid_addedProduit = (!string.IsNullOrEmpty(NomProduit) &&
                                     !string.IsNullOrEmpty(DescriptionProduit) &&
                                     PrixProduit > 0 &&
                                     !string.IsNullOrEmpty(ImageProduit) &&
                                     !string.IsNullOrEmpty(TypeProduit) &&
                                     !string.IsNullOrEmpty(ManuelProduit) &&
                                     !string.IsNullOrEmpty(FabricantProduit) &&
                                     QuantiteProduit > 0 &&
                                     CapaciteProduit > 0);
        }











        public AjoutArticleViewModel(Window window, int idGestionnaire)
        {
            this._window = window;
            DBContext = new DatabaseContext.GestionRelationClient_DBContext();

            this.Gestionnaire = DBContext.Utilisateurs.Where(u => (u.Id.Equals(idGestionnaire))).FirstOrDefault();


            // Service à ajouter, on ne gère pas encore les abonnements donc on lui affecte celui par défaut ; on lui affecte aussi un panier bidon
            this._addedService = new Models.Service()
            {
                AbonnementId = 1,
                PanierId = 1
            };
            // Produit à ajouter, on ne gère pas encore les abonnements donc on lui affecte celui par défaut ; on lui affecte aussi un panier bidon
            this._addedProduit = new Models.Produit()
            {
                AbonnementId = 1,
                PanierId = 1,
                //StockId = // On ne peut pas récupérer le stock Id puisque il n'est que dans la table Gestionnaire, hors nous n'avons accès qu'à la table Utilisateur
            };

            // Navigation
            GoToAccueilCommand = new RelayCommand(
                o => true,
                o => OpenAccueil()
            );
            GoToPageGestionnaireCommand = new RelayCommand(
                o => true,
                o => OpenPageGestionnaire()
            );
            AjouterServiceCommand = new RelayCommand(
                o => _isValid_addedService,
                o => AddService()
            );
            AjouterProduitCommand = new RelayCommand(
                o => _isValid_addedProduit,
                o => AddProduit()
            );
        }


        /* definition of the commands */
        public ICommand GoToAccueilCommand { get; private set; }
        public ICommand GoToPageGestionnaireCommand { get; private set; }
        public ICommand AjouterServiceCommand { get; private set; }
        public ICommand AjouterProduitCommand { get; private set; }



        



        /* Ajout Service */
        private void AddService()
        {
            DBContext.Add(_addedService);
            DBContext.SaveChanges();
            MessageBox.Show("Service " + _addedService.Nom + " ajouté");
        }

        /* Ajout Service */
        private void AddProduit()
        {
            /*
            DBContext.Add(_addedProduit);
            DBContext.SaveChanges();
            MessageBox.Show("Produit " + _addedProduit.Nom + " ajouté");*/
            MessageBox.Show("Non encore implémenté");
        }




        /* Navigation */
        private void OpenAccueil()
        {
            Gestionnaire.Deconnexion();
            DBContext.SaveChanges();

            Views.Accueil accueil = new Views.Accueil();
            accueil.Show();
            _window.Close();
        }
        private void OpenPageGestionnaire()
        {
            Views.PageGestionnaire pageGestionnaire = new Views.PageGestionnaire(Gestionnaire.Id);
            pageGestionnaire.Show();
            _window.Close();
        }

    }
}
