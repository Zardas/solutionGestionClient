using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace gestionRelationClient.Models
{
    //[Table("Client")]
    class Client : Utilisateur
    {

        public String Nom { get; set; }

        public String Prenom { get; set; }

        public String Mail { get; set; }

        public String Telephone { get; set; }

        public int Age { get; set; }


        // Un client possède plusieurs comptes
        public ICollection<Compte> Comptes;

        public int GestionnaireAssocieId { get; set; }

        public int Solde { get; set; }


    }
}
