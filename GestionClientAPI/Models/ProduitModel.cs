using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class ProduitModel : ArticleModel
    {
        public String Fabricant { get; set; }
        public int Quantite { get; set; }
        public int Capacite { get; set; }

        // Un produit est lié à un stock
        public int StockId { get; set; }
    }
}
