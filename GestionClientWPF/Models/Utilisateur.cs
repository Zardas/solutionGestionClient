using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientWPF.Models
{
    class Utilisateur
    {
        public int UtilisateurId { get; set; }

        public String Login { get; set; }

        public String MotDePasse { get; set; }

        public String LoginStatus { get; set; }
    }
}
