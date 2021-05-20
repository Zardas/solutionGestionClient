using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class SupportModel
    {
        public DateTime DateCreation { get; set; }
        public String Status { get; set; }
        public String Objet { get; set; }
        public String Description { get; set; }
        public String Resolution { get; set; }

        // Un support est lié à un compte
        public int CompteId { get; set; }

    }
}
