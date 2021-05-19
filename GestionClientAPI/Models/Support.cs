using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Support
    {
        public int SupportId { get; set; }
        public DateTime DateCreation { get; set; }
        public String Status { get; set; }
        public String Objet { get; set; }
        public String Description { get; set; }
        public String Resolution { get; set; }

        // Un support est lié à un compte
        public int CompteId { get; set; }
        public Compte Compte { get; set; }

        // Un sopprot peut être lié à plusieurs articles
        public ICollection<Article> Articles { get; set; }


        public Support()
        {
            this.Articles = new List<Article>();
            this.Status = "Ouvert";
        }

        public Support(Article article)
        {
            this.Articles = new List<Article>();
            this.Articles.Add(article);

            this.Status = "Ouvert";
        }

        public void Resoudre(string resolution)
        {
            this.Resolution = resolution;
            this.Status = "Resolu";
        }
    }
}
