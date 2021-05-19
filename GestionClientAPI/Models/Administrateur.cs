using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Models
{
    public class Administrateur : Utilisateur
    {
        public string NomAdministrateur { get; set; }
        public string Mail { get; set; }

        // Un gestionnaire peut gérer les commerciaux et les clients
    }
}
