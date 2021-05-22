using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientWPF.Models
{
    public class Panier
    {
        public int PanierId { get; set; }
        public int QuantiteProduit { get; set; }

        // Un panier est lié à un compte
        public int CompteId { get; set; }
        public Compte Compte { get; set; }

        // Un panier peut contenir plusieurs article (c'est juste un peu sa raison d'être d'ailleurs)
        public ICollection<Article> Articles { get; set; }

        public Panier()
        {
            this.Articles = new List<Article>();
        }



        public Panier(Compte compte)
        {
            this.Compte = compte;
            this.CompteId = compte.CompteId;

            this.Articles = new List<Article>();

        }
    }
}
