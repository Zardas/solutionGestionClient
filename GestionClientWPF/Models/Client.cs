using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientWPF.Models
{
    class Client
    {
        public String Nom { get; set; }

        public String Prenom { get; set; }

        public String Mail { get; set; }

        public String Telephone { get; set; }

        public int Age { get; set; }


        // Un client possède plusieurs comptes
        //public ICollection<Compte> Comptes;

        public int GestionnaireAssocieId { get; set; }

        public int Solde { get; set; }
    }
}
