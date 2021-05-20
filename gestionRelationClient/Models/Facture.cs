using System;
using System.Collections.Generic;
using System.Text;

namespace gestionRelationClient.Models
{
    class Facture
    {
        public int FactureId { get; set; }
        public DateTime DateEmission { get; set; }
        public int Montant { get; set; }

        // Un facture est associée à un Compte
        public int CompteId { get; set; }
        public Compte Compte { get; set; }

    }
}
