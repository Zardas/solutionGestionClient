using System;
using System.Collections.Generic;
using System.Text;

namespace gestionRelationClient.Models
{
    class Article
    {
        // ArticleId = clé primaire dans la bdd pour la table Article
        public int ArticleId { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public int Prix { get; set; }
        public String Image { get; set; }
        public String Type { get; set; }
        public String Manuel { get; set; }

        public ICollection<Support> Supports;

        public Article()
        {
            this.Supports = new List<Support>();
        }

    }
}
