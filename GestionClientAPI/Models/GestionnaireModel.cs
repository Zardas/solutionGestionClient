using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class GestionnaireModel : UtilisateurModel
    {
        public string NomGestionnaire { get; set; }
        public string Email { get; set; }

        // Un gestionnaire possède un rôle
        public int RoleId { get; set; }

        // Un gestionnaire est responsable d'un stock
        public int StockId { get; set; }

        public int Gain { get; set; }
    }
}
