using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public String Titre { get; set; }

        // Un stock est lié à un gestionnaire
        //public int GestionnaireId { get; set; }
        public Gestionnaire ResponsableStock { get; set; }

        // Un stock possède plusieurs produits
        public ICollection<Produit> Produits { get; set; }

        public Stock()
        {
            this.Produits = new List<Produit>();
        }
    }
}
