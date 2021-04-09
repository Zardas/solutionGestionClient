using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gestionRelationClient.DatabaseContext
{
    class gestionRelationClient_DBContext : DbContext
    {
        // Pour créer la BDD : 

        public DbSet<Models.TestClass> tableTest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionBuilder)
        {
            dbContextOptionBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                                                        Initial Catalog=master;
                                                        Integrated Security=True;
                                                        Connect Timeout=30;
                                                        Encrypt=False;
                                                        TrustServerCertificate=False;
                                                        ApplicationIntent=ReadWrite;
                                                        MultiSubnetFailover=False;
                                                        Database=GestionRelationClient;
                                                        Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Avec une propriété
            modelBuilder.Entity<Models.TestClass>().HasKey(t => t.Id); // Test aura comme clé primaire "Id"

            // Avec plusieurs propriétés
            modelBuilder.Entity<Models.TestClass>().HasKey(t => new { t.Id, t.login}); // Test aura comme clé primaire le couple "Id, login"

            modelBuilder.Entity<Models.TestClass>().HasData(
                new Models.TestClass(3),
                new Models.TestClass(4)
            );

        }
    }
}
