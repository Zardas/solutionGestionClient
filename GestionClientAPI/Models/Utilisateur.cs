using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Utilisateur
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
