using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gestionRelationClient.Models
{
    public abstract class Utilisateur
    {
        public int UtilisateurId { get; set; }

        public String Login { get; set; }

        public String MotDePasse { get; set; }

        public String LoginStatus { get; set; }



        public void Connexion()
        {
            this.LoginStatus = "online";
        }
        public void Deconnexion()
        {
            this.LoginStatus = "offline";
        }
    }
}
