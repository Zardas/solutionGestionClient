using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace gestionRelationClient.DatabaseContext
{
    class GestionRelationClient_DBContext : DbContext
    {
        // 1 - Pour intégrer le schéma des données : add-migration {nomMigration}
        // 2 - Pour update la base de données : Update-Database -verbose

        public DbSet<Models.Abonnement> Abonnements { get; set; }
        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.Compte> Comptes { get; set; }
        public DbSet<Models.Facture> Factures { get; set; }
        public DbSet<Models.Panier> Paniers { get; set; }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.Stock> Stocks { get; set; }
        public DbSet<Models.Support> Supports { get; set; }
        public DbSet<Models.Utilisateur> Utilisateurs { get; set; }

        //public DbSet<Models.TestClass> TestClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuilder)
        {
            dbContextOptionBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                                                    Initial Catalog=master;
                                                    Integrated Security=True;
                                                    Connect Timeout=30;Encrypt=False;
                                                    TrustServerCertificate=False;
                                                    ApplicationIntent=ReadWrite;
                                                    MultiSubnetFailover=False;
                                                    Database=GestionClientDatabase4;
                                                    Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Gestion de l'héritage - Table per Type
            modelBuilder.Entity<Models.Client>().ToTable("Client");
            modelBuilder.Entity<Models.Gestionnaire>().ToTable("Gestionnaire");

            modelBuilder.Entity<Models.Service>().ToTable("Service");
            modelBuilder.Entity<Models.Produit>().ToTable("Produit");

            // Gestion de l'héritage - Table per Concrete (pas dispo dans Entity Framework Core)
            /*modelBuilder.Entity<Models.Client>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Client");
            });*/

            modelBuilder.Entity<Models.Abonnement>().HasData(
                new Models.Abonnement() { AbonnementId=1, DureeAbonnement=0} // Abonnement 1 est l'abonnement null
            );

            modelBuilder.Entity<Models.Stock>().HasData(
                new Models.Stock() { StockId=1, Titre="Stock par défaut"} // Stock 1 est le stock par défaut
            );


            modelBuilder.Entity<Models.Produit>().HasData(
                new Models.Produit() { Id = 1, Description = "Logiciel caisse simple", Nom="Caisse_basic", Capacite=3, Prix=60, AbonnementId=1, StockId=1, Image="ressources/img/articleImg_caisse.jpg" },
                new Models.Produit() { Id = 2, Description = "Logiciel cassie medium", Nom="Caisse_medium", Capacite=3, Prix=100, AbonnementId=1, StockId=1, Image="ressources/img/articleImg_caisse.jpg" }
            );

            modelBuilder.Entity<Models.Service>().HasData(
                new Models.Service() { Id = 3, Description = "Service de WiFi simple", Nom = "Wifi_simple", Prix = 20, AbonnementId=1, Image="ressources/img/articleImg_wifi.png" }
            );

        }
    }
}
