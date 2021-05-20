using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionClientAPI.Models.Shared
{
    public class gestionAPI_DBContext : DbContext
    {
        public gestionAPI_DBContext(DbContextOptions<gestionAPI_DBContext> options) : base(options)
        {

        }

        // 1 - Pour intégrer le schéma des données : add-migration {nomMigration} -Project GestionClientAPI
        // 2 - Pour update la base de données : Update-Database -Project GestionClientAPI -verbose

        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuilder)
        {
            dbContextOptionBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                                                    Initial Catalog=master;
                                                    Integrated Security=True;
                                                    Connect Timeout=30;Encrypt=False;
                                                    TrustServerCertificate=False;
                                                    ApplicationIntent=ReadWrite;
                                                    MultiSubnetFailover=False;
                                                    Database=GestionClientDatabaseWebApp_forAPI2;
                                                    Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //Gestion de l'héritage : modèle-per-hierarchy
            modelBuilder.Entity<Utilisateur>()
                .HasDiscriminator<string>("Type_Utilisateur")
                .HasValue<Client>("est_client")
                .HasValue<Gestionnaire>("est_gestionnaire")
                .HasValue<Administrateur>("est_administrateur")
                .HasValue<Utilisateur>("est_utilisateur");

            modelBuilder.Entity<Article>()
                .HasDiscriminator<string>("Type_Article")
                .HasValue<Service>("est_service")
                .HasValue<Produit>("est_produit")
                .HasValue<Article>("est_article");



            // Ajout de rôles
            Role role1 = new Role() { RoleId = 1, Title = "Role1" };
            Role role2 = new Role() { RoleId = 2, Title = "Role2" };
            Role role3 = new Role() { RoleId = 3, Title = "Role3" };
            Role role4 = new Role() { RoleId = 4, Title = "Role4" };

            modelBuilder.Entity<Role>().HasData(
                role1,
                role2,
                role3,
                role4
            );


            // Ajout de stocks
            Stock stock1 = new Stock() { StockId = 1, Titre = "stockClassique" };
            Stock stock2 = new Stock() { StockId = 2, Titre = "stockNouveautes" };
            Stock stock3 = new Stock() { StockId = 3, Titre = "stockAncien" };

            modelBuilder.Entity<Stock>().HasData(
                stock1,
                stock2,
                stock3
            );


            // Ajout de gestionnaires
            Gestionnaire gestionnaire1 = new Gestionnaire()
            {
                UtilisateurId = 1,
                Login = "Kenny",
                Email = "kMc@sp.ca",
                NomGestionnaire = "McCormick",
                RoleId = role1.RoleId,
                StockId = stock3.StockId,
                MotDePasse = Utilitaire.HashPassword("gsnPsw1")
            };
            Gestionnaire gestionnaire2 = new Gestionnaire()
            {
                UtilisateurId = 2,
                Login = "Stan",
                Email = "sm@sp.ca",
                NomGestionnaire = "Marsh",
                RoleId = role2.RoleId,
                StockId = stock1.StockId,
                MotDePasse = Utilitaire.HashPassword("gsnPsw2")
            };
            Gestionnaire gestionnaire3 = new Gestionnaire()
            {
                UtilisateurId = 3,
                Login = "Kyle",
                Email = "kBr@sp.ca",
                NomGestionnaire = "Broslovsky",
                RoleId = role2.RoleId,
                StockId = stock2.StockId,
                MotDePasse = Utilitaire.HashPassword("gsnPsw3")
            };

            modelBuilder.Entity<Gestionnaire>().HasData(
                gestionnaire1,
                gestionnaire2,
                gestionnaire3
            );


            // Ajout d'administrateur
            Administrateur administrateur1 = new Administrateur()
            {
                UtilisateurId = 4,
                Login = "Butters",
                NomAdministrateur = "Butters",
                Mail = "bt@sp.ca",
                MotDePasse = Utilitaire.HashPassword("adminPsw1")
            };

            modelBuilder.Entity<Administrateur>().HasData(
                administrateur1
            );


            // Ajout d'un abonnement nul
            Abonnement abonnement1 = new Abonnement()
            {
                AbonnementId = 1,
                DureeAbonnement = 0
            };

            modelBuilder.Entity<Abonnement>().HasData(
                abonnement1
            );

            // Ajout d'un client nul
            Client client1 = new Client()
            {
                UtilisateurId = 5,
                Login = "ClientDefaut",
                Nom = "Defaut",
                Mail = "er@ezr.cq",
                GestionnaireAssocieId = gestionnaire1.UtilisateurId,
                MotDePasse = Utilitaire.HashPassword("azegaze684qs3dqsd!:sdfàçzqè_"),
            };

            modelBuilder.Entity<Client>().HasData(
                client1
            );


            // Ajout d'un compte nul
            Compte compte1 = new Compte()
            {
                CompteId = 1,
                NomCompte = "CompteDefaut",
                ClientId = client1.UtilisateurId
            };

            modelBuilder.Entity<Compte>().HasData(
                compte1
            );


            // Ajout d'un panier nul
            Panier panier1 = new Panier()
            {
                PanierId = 1,
                CompteId = compte1.CompteId
            };

            modelBuilder.Entity<Panier>().HasData(
                panier1
            );
        }

    }
}
