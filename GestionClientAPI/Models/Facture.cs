using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Facture
    {
        public int FactureId { get; set; }
        public DateTime DateEmission { get; set; }
        public int Montant { get; set; }

        // Un facture est associée à un Compte
        public int CompteId { get; set; }
        public Compte Compte { get; set; }

        public void CalculerMontant()
        {

        }
    }
}
