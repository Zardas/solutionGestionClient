using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Client : Utilisateur
    {
        //public int ClientId { get; set; }

        public String Nom { get; set; }

        public String Prenom { get; set; }

        public String Mail { get; set; }

        public String Telephone { get; set; }

        public int Age { get; set; }


        // Un client possède plusieurs comptes
        public ICollection<Compte> Comptes;

        public int GestionnaireAssocieId { get; set; }

        public int Solde { get; set; }

        public Client()
        {
            this.Comptes = new List<Compte>();
            this.Solde = 0;
        }






        public void Inscrire(string password)
        {
            this.LoginStatus = "offline";
            MotDePasse = Shared.Utilitaire.HashPassword(password);
        }

        public void AjouterCompte(Compte newCompte)
        {
            this.Comptes.Add(newCompte);
        }

        // TODO ?
        public void Recherche()
        {

        }

        public void AjoutSolde(int montant)
        {
            this.Solde += montant;
        }

        public void GenererFacture(Facture facture)
        {
            this.Solde -= facture.Montant;
        }

        public void ModifierProfil(string nouveauLogin, string nouveauMail, string nouveauNom, string nouveauPrenom, string nouveauMotDePasse, string nouveauTelephone, int nouveauAge)
        {
            if (nouveauLogin != "")
            {
                this.Login = nouveauLogin;
            }
            if (nouveauMail != "")
            {
                this.Mail = nouveauMail;
            }
            if (nouveauNom != "")
            {
                this.Nom = nouveauNom;
            }
            if (nouveauPrenom != "")
            {
                this.Prenom = nouveauPrenom;
            }
            if (nouveauMotDePasse != "")
            {
                this.MotDePasse = Shared.Utilitaire.HashPassword(nouveauMotDePasse);
            }
            if (nouveauTelephone != "")
            {
                this.Telephone = nouveauTelephone;
            }
            if (nouveauAge != 0)
            {
                this.Age = nouveauAge;
            }
        }
    }
}
