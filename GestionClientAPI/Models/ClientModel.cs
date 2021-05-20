using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class ClientModel : UtilisateurModel
    {
        public String Nom { get; set; }

        public String Prenom { get; set; }

        public String Mail { get; set; }

        public String Telephone { get; set; }

        public int Age { get; set; }

    }
}
