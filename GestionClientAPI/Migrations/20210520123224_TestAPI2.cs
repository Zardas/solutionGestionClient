using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionClientAPI.Migrations
{
    public partial class TestAPI2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Abonnements",
                columns: new[] { "AbonnementId", "DureeAbonnement" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "Comptes",
                columns: new[] { "CompteId", "ClientId", "DateCreation", "NomCompte" },
                values: new object[] { 1, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CompteDefaut" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Login", "LoginStatus", "Administrateur_Mail", "MotDePasse", "NomAdministrateur", "Type_Utilisateur" },
                values: new object[] { 4, "Butters", null, "bt@sp.ca", "G6K9uekzJ2k/7tpxkHZvEVa5yoDOW+tELPPEyDKeGFs=", "Butters", "est_administrateur" });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "UtilisateurId", "Age", "GestionnaireAssocieId", "GestionnaireUtilisateurId", "Login", "LoginStatus", "Mail", "MotDePasse", "Nom", "Prenom", "Solde", "Telephone", "Type_Utilisateur" },
                values: new object[] { 5, 0, 0, null, "ClientDefaut", null, "er@ezr.cq", "iGmj0/VzeGneyVuucE40IMekMljU2iQKD0+0TSpW+Pw=", "Defaut", null, 0, null, "est_client" });

            migrationBuilder.InsertData(
                table: "Paniers",
                columns: new[] { "PanierId", "CompteId", "QuantiteProduit" },
                values: new object[] { 1, 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Abonnements",
                keyColumn: "AbonnementId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paniers",
                keyColumn: "PanierId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "UtilisateurId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "UtilisateurId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comptes",
                keyColumn: "CompteId",
                keyValue: 1);
        }
    }
}
