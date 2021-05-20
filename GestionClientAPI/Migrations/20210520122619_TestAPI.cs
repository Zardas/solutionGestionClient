using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionClientAPI.Migrations
{
    public partial class TestAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonnements",
                columns: table => new
                {
                    AbonnementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DureeAbonnement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnements", x => x.AbonnementId);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    CompteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomCompte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.CompteId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<int>(type: "int", nullable: false),
                    CompteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureId);
                    table.ForeignKey(
                        name: "FK_Factures_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "CompteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paniers",
                columns: table => new
                {
                    PanierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantiteProduit = table.Column<int>(type: "int", nullable: false),
                    CompteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paniers", x => x.PanierId);
                    table.ForeignKey(
                        name: "FK_Paniers_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "CompteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supports",
                columns: table => new
                {
                    SupportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supports", x => x.SupportId);
                    table.ForeignKey(
                        name: "FK_Supports_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "CompteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type_Utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomAdministrateur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Administrateur_Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    GestionnaireAssocieId = table.Column<int>(type: "int", nullable: true),
                    Solde = table.Column<int>(type: "int", nullable: true),
                    GestionnaireUtilisateurId = table.Column<int>(type: "int", nullable: true),
                    NomGestionnaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    StockId = table.Column<int>(type: "int", nullable: true),
                    Gain = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurId);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Utilisateurs_GestionnaireUtilisateurId",
                        column: x => x.GestionnaireUtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prix = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manuel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanierId = table.Column<int>(type: "int", nullable: false),
                    AbonnementId = table.Column<int>(type: "int", nullable: false),
                    SupportId = table.Column<int>(type: "int", nullable: true),
                    Type_Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fabricant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantite = table.Column<int>(type: "int", nullable: true),
                    Capacite = table.Column<int>(type: "int", nullable: true),
                    StockId = table.Column<int>(type: "int", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Abonnements_AbonnementId",
                        column: x => x.AbonnementId,
                        principalTable: "Abonnements",
                        principalColumn: "AbonnementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Paniers",
                        principalColumn: "PanierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Supports_SupportId",
                        column: x => x.SupportId,
                        principalTable: "Supports",
                        principalColumn: "SupportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Title" },
                values: new object[,]
                {
                    { 1, "Role1" },
                    { 2, "Role2" },
                    { 3, "Role3" },
                    { 4, "Role4" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "StockId", "Titre" },
                values: new object[,]
                {
                    { 1, "stockClassique" },
                    { 2, "stockNouveautes" },
                    { 3, "stockAncien" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Email", "Gain", "Login", "LoginStatus", "MotDePasse", "NomGestionnaire", "RoleId", "StockId", "Type_Utilisateur" },
                values: new object[] { 2, "sm@sp.ca", 0, "Stan", null, "nuqhOWieLZIBaHuA4B5lM0hir40FI/ABHqyYlm+AL4I=", "Marsh", 2, 1, "est_gestionnaire" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Email", "Gain", "Login", "LoginStatus", "MotDePasse", "NomGestionnaire", "RoleId", "StockId", "Type_Utilisateur" },
                values: new object[] { 3, "kBr@sp.ca", 0, "Kyle", null, "+8LmkEKGrLy9Mo5DSNqSZmZOxt5G3Nq6hLKSXXnhSC0=", "Broslovsky", 2, 2, "est_gestionnaire" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Email", "Gain", "Login", "LoginStatus", "MotDePasse", "NomGestionnaire", "RoleId", "StockId", "Type_Utilisateur" },
                values: new object[] { 1, "kMc@sp.ca", 0, "Kenny", null, "XCD184U9dMf46s9eCzzHEsRhgnOmwc8LH7JslQ2+jVk=", "McCormick", 1, 3, "est_gestionnaire" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AbonnementId",
                table: "Articles",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PanierId",
                table: "Articles",
                column: "PanierId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_StockId",
                table: "Articles",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SupportId",
                table: "Articles",
                column: "SupportId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CompteId",
                table: "Factures",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Paniers_CompteId",
                table: "Paniers",
                column: "CompteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supports_CompteId",
                table: "Supports",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_GestionnaireUtilisateurId",
                table: "Utilisateurs",
                column: "GestionnaireUtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_RoleId",
                table: "Utilisateurs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_StockId",
                table: "Utilisateurs",
                column: "StockId",
                unique: true,
                filter: "[StockId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Abonnements");

            migrationBuilder.DropTable(
                name: "Paniers");

            migrationBuilder.DropTable(
                name: "Supports");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Comptes");
        }
    }
}
