using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientWPF.Models
{
    public class Produit : Article
    {
        public String Fabricant { get; set; }
        public int Quantite { get; set; }
        public int Capacite { get; set; }

        // Un produit est lié à un stock
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
