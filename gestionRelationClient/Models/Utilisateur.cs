using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gestionRelationClient.Models
{
    public abstract class Utilisateur
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int UtilisateurId { get; set; }
        [Key]
        public int Id { get; set; }
        public String Login { get; set; }
        public String MotDePasse { get; set; }
        public String LoginStatus { get; set; }


    }
}
