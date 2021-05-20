using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace gestionRelationClient.Models
{
    //[Table("Gestionnaire")]
    class Gestionnaire : Utilisateur
    {
        public string NomGestionnaire { get; set; }
        public string Email { get; set; }

        // Un gestionnaire possède un rôle
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Un gestionnaire est responsable d'un stock
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Gain { get; set; }

        public List<Client> ClientsAssocies { get; set; }

        // TODO
        public void AjouterStock()
        {
            
        }

        // TODO
        public void ModifierStock()
        {

        }

        // TODO
        public void SupprimerStock()
        {

        }

        // TODO
        public void AjoutArticle()
        {

        }

        // TODO
        public void SupprimerArticle()
        {

        }

        // TODO
        public void ModifierArticle()
        {

        }

        // TODO
        public void AjouterRole()
        {

        }

        // TODO
        public void ChangerRole()
        {

        }

        public void SupprimerRole()
        {
            this.Role = null;
        }
    }
}
