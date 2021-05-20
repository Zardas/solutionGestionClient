using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class CompteModel
    {
        public DateTime DateCreation { get; set; }
        public String NomCompte { get; set; }

        // Un compte est lié à un seul client
        public int ClientId { get; set; }

    }
}
