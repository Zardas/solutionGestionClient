using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClientAPI.Controllers
{
    public interface IJwtAuthentificationManager
    {
        public string Authentify(int Id);
    }
}
