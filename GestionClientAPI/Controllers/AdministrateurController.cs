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
    [Route("api/Administrateur")]
    [ApiController]
    public class AdministrateurController : Controller
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public AdministrateurController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAdministrateurs()
        {
            try
            {
                List<Administrateur> administrateurs = _context.Administrateurs.ToList();

                return Ok(administrateurs);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400

        }

    }
}
