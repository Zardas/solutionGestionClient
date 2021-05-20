using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionClientAPI.Models;
using GestionClientAPI.Models.Shared;

namespace GestionClientAPI.Data
{
    public class DBInitializer
    {
        public static void Initialize(gestionAPI_DBContext context)
        {
            //On s'assure que la BDD a bien été créée
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            // Ajout de rôles
            Role role1 = new Role() { Title = "Role1" };
            Role role2 = new Role() { Title = "Role2" };
            Role role3 = new Role() { Title = "Role3" };
            Role role4 = new Role() { Title = "Role4" };
            context.Roles.Add(role1);
            context.Roles.Add(role2);
            context.Roles.Add(role3);
            context.Roles.Add(role4);






            // Ajout de gestionnaires
            Gestionnaire gestionnaire1 = new Gestionnaire()
            {
                Login = "Kenny",
                Email = "kMc@sp.ca",
                NomGestionnaire = "McCormick",
                Role = role2,
                MotDePasse = Utilitaire.HashPassword("gsnPsw1")
            };
            Gestionnaire gestionnaire2 = new Gestionnaire()
            {
                Login = "Stan",
                Email = "sm@sp.ca",
                NomGestionnaire = "Marsh",
                Role = role3,
                MotDePasse = Utilitaire.HashPassword("gsnPsw2")
            };
            Gestionnaire gestionnaire3 = new Gestionnaire()
            {
                Login = "Kyle",
                Email = "kBr@sp.ca",
                NomGestionnaire = "Broslovsky",
                Role = role1,
                MotDePasse = Utilitaire.HashPassword("gsnPsw3")
            };
            context.Gestionnaires.Add(gestionnaire1);
            context.Gestionnaires.Add(gestionnaire2);
            context.Gestionnaires.Add(gestionnaire3);



            // Ajout de stocks
            Stock stock1 = new Stock() { Titre = "stockClassique", ResponsableStock = gestionnaire2 };
            Stock stock2 = new Stock() { Titre = "stockNouveautes", ResponsableStock = gestionnaire3 };
            Stock stock3 = new Stock() { Titre = "stockAncien", ResponsableStock = gestionnaire1 };
            context.Stocks.Add(stock1);
            context.Stocks.Add(stock2);
            context.Stocks.Add(stock3);


            // Ajout d'administrateur
            Administrateur administrateur1 = new Administrateur()
            {
                Login = "Butters",
                NomAdministrateur = "Butters",
                Mail = "bt@sp.ca",
                MotDePasse = Utilitaire.HashPassword("adminPsw1")
            };
            context.Administrateurs.Add(administrateur1);


            // Ajout d'un abonnement nul
            Abonnement abonnement1 = new Abonnement()
            {
                DureeAbonnement = 0
            };
            context.Abonnements.Add(abonnement1);

            // Ajout d'un client nul
            Client client1 = new Client()
            {
                Login = "ClientDefaut",
                Nom = "Defaut",
                Mail = "er@ezr.cq",
                MotDePasse = Utilitaire.HashPassword("azegaze684qs3dqsd!:sdfàçzqè_"),
            };
            context.Clients.Add(client1);

            // Ajout d'un compte nul
            Compte compte1 = new Compte()
            {
                NomCompte = "CompteDefaut",
                ClientId = 2,
            };
            context.Comptes.Add(compte1);

            // Ajout d'un panier nul
            Panier panier1 = new Panier()
            {
                Compte = compte1
            };
            context.Paniers.Add(panier1);






            context.SaveChanges();




        }
    }
}
