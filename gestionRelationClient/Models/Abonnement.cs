using System;
using System.Collections.Generic;
using System.Text;

namespace gestionRelationClient.Models
{
    public class Abonnement
    {
        // AbonnementId = clé primaire pour la bdd
        public int AbonnementId { get; set; }
        public int DureeAbonnement { get; set; }

        // Un abonnement peut concerner plusieurs article
        public ICollection<Article> Articles { get; set; }

        public Abonnement()
        {
            this.Articles = new List<Article>();
        }
    }
}
