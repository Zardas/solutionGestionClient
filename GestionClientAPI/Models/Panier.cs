using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
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

        public void AjoutArticle(Article article)
        {
            article.PanierId = this.PanierId;
            this.Articles.Add(article);
        }

        public ICollection<Article> GetAllArticles()
        {
            return this.Articles;
        }


        public void SupprimerPanierArticle(Article article)
        {
            // Le panier 1 est le panier nul défaut
            article.PanierId = 1;
            this.Articles.Remove(article);
        }

        public int getPrixTotal()
        {
            int prixTotal = 0;

            foreach (Article article in this.Articles)
            {
                prixTotal += article.Prix;
            }
            return prixTotal;
        }
    }
}

