using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class PanierModel
    {
        public int QuantiteProduit { get; set; }

        // Un panier est lié à un compte
        public int CompteId { get; set; }
    }
}
