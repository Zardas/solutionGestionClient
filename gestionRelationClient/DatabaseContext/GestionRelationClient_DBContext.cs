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
                                                    Database=GestionClientDatabase12;
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
            
            modelBuilder.Entity<Models.Client>().HasData(
                new Models.Client() { Id=1, Login="UtilisateurBidon", Mail="trash@t.c", MotDePasse = "zerz45sdsdzaz_eaze4", Nom="ClientTest", Prenom="ClientTest", Telephone="231231"}
                //new Models.Client() { Id=2, Login="Stan", Mail="staan@sp.ca", MotDePasse="mdpTest", Nom="Marsh", Prenom="Stan", Telephone="23151321"}
            );

            modelBuilder.Entity<Models.Compte>().HasData(
                new Models.Compte() { CompteId = 1, ClientId = 1 }
            );
            
            modelBuilder.Entity<Models.Panier>().HasData(
                new Models.Panier() { PanierId = 1, CompteId = 1} // Panier 1 est le panier par défaut
            );


            
            
            modelBuilder.Entity<Models.Stock>().HasData(
                new Models.Stock() { StockId=1, Titre="Stock par défaut"} // Stock 1 est le stock par défaut
            );

            
            modelBuilder.Entity<Models.Produit>().HasData(
                new Models.Produit()
                {
                    Id = 1,
                    Nom = "Caisse_basic",
                    Capacite = 3,
                    Prix = 60,
                    AbonnementId = 1,
                    StockId = 1,
                    PanierId = 1,
                    Image = "ressources/img/articleImg_caisse.jpg",
                    Manuel = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eleifend imperdiet varius. Vivamus eu faucibus sapien, maximus porta odio. Fusce viverra purus non orci vulputate, mollis auctor velit accumsan. Quisque sit amet cursus tellus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus ligula libero, posuere vitae lectus a, finibus dapibus ligula. In blandit egestas sollicitudin. Pellentesque venenatis, tellus vitae tincidunt dapibus, tortor neque bibendum eros, a bibendum ex quam et erat. Integer accumsan ultrices dui sit amet pulvinar. Sed at odio id urna finibus maximus sit amet vel velit. Donec nec ultricies lacus. Maecenas eu convallis libero. Quisque mauris massa, vestibulum sit amet neque vitae, pretium vehicula leo.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam finibus tempus urna. Donec libero leo, sagittis vel turpis laoreet, lacinia suscipit metus. Etiam a nisi eu purus sollicitudin cursus nec eget est. Nullam a arcu justo. Curabitur eget efficitur ex, eget eleifend sapien. Phasellus ultrices nulla libero, quis lobortis est vulputate id. Vestibulum finibus tortor eget odio efficitur, ac tempor magna ullamcorper. Pellentesque eu ultricies risus."
                },
                new Models.Produit()
                {
                    Id = 2,
                    Nom = "Caisse_medium",
                    Capacite = 3,
                    Prix = 100,
                    AbonnementId = 1,
                    StockId = 1,
                    PanierId = 1,
                    Image = "ressources/img/articleImg_caisse.jpg",
                    Manuel = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eleifend imperdiet varius. Vivamus eu faucibus sapien, maximus porta odio. Fusce viverra purus non orci vulputate, mollis auctor velit accumsan. Quisque sit amet cursus tellus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus ligula libero, posuere vitae lectus a, finibus dapibus ligula. In blandit egestas sollicitudin. Pellentesque venenatis, tellus vitae tincidunt dapibus, tortor neque bibendum eros, a bibendum ex quam et erat. Integer accumsan ultrices dui sit amet pulvinar. Sed at odio id urna finibus maximus sit amet vel velit. Donec nec ultricies lacus. Maecenas eu convallis libero. Quisque mauris massa, vestibulum sit amet neque vitae, pretium vehicula leo.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam finibus tempus urna. Donec libero leo, sagittis vel turpis laoreet, lacinia suscipit metus. Etiam a nisi eu purus sollicitudin cursus nec eget est. Nullam a arcu justo. Curabitur eget efficitur ex, eget eleifend sapien. Phasellus ultrices nulla libero, quis lobortis est vulputate id. Vestibulum finibus tortor eget odio efficitur, ac tempor magna ullamcorper. Pellentesque eu ultricies risus. "
                }
            );

            modelBuilder.Entity<Models.Service>().HasData(
                new Models.Service() {
                    Id = 3,
                    Nom = "Wifi_simple",
                    Prix = 20,
                    AbonnementId=1,
                    PanierId = 1,
                    Image="ressources/img/articleImg_wifi.png",
                    Manuel="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eleifend imperdiet varius. Vivamus eu faucibus sapien, maximus porta odio. Fusce viverra purus non orci vulputate, mollis auctor velit accumsan. Quisque sit amet cursus tellus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus ligula libero, posuere vitae lectus a, finibus dapibus ligula. In blandit egestas sollicitudin. Pellentesque venenatis, tellus vitae tincidunt dapibus, tortor neque bibendum eros, a bibendum ex quam et erat. Integer accumsan ultrices dui sit amet pulvinar. Sed at odio id urna finibus maximus sit amet vel velit. Donec nec ultricies lacus. Maecenas eu convallis libero. Quisque mauris massa, vestibulum sit amet neque vitae, pretium vehicula leo.",
                    Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam finibus tempus urna. Donec libero leo, sagittis vel turpis laoreet, lacinia suscipit metus. Etiam a nisi eu purus sollicitudin cursus nec eget est. Nullam a arcu justo. Curabitur eget efficitur ex, eget eleifend sapien. Phasellus ultrices nulla libero, quis lobortis est vulputate id. Vestibulum finibus tortor eget odio efficitur, ac tempor magna ullamcorper. Pellentesque eu ultricies risus. "
                }
            );

        }
    }
}
