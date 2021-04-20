using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gestionRelationClient.Models
{
    public abstract class Article
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int ArticleId { get; set; }

        [Key]
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public int Prix { get; set; }
        public String Image { get; set; }
        public String Type { get; set; }
        public String Manuel { get; set; }

        
        // Un article peut être lié à plusieurs supports (beaucoup s'il est nul)
        public ICollection<Support> Supports;

        // Un article peut avoir un abonnement associé (ou non)
        public int AbonnementId { get; set; }
        public Abonnement Abonnement { get; set; }


        public Article()
        {
            this.Supports = new List<Support>();
        }


        // TODO ?
        public void AfficherArticle()
        {

        }

        //TODO
        public void GetArticleDetails()
        {

        }
    }
}
