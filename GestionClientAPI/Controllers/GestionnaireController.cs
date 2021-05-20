using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionClientAPI.Models.Shared;
using GestionClientAPI.Models;
using System.Diagnostics;

namespace GestionClientAPI.Controllers
{
    [Route("api/Gestionnaire")]
    [ApiController]
    public class GestionnaireController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public GestionnaireController(gestionAPI_DBContext context)
        {
            _context = context;
        }

    }
}
