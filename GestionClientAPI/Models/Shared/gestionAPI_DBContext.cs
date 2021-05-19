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

        // 1 - Pour intégrer le schéma des données : add-migration {nomMigration}
        // 2 - Pour update la base de données : Update-Database -verbose

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
                                                    Database=GestionClientDatabaseWebApp;
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
        }

    }
}
