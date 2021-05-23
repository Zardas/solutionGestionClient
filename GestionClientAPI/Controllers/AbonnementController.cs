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
    [Route("api/Abonnement")]
    [ApiController]
    public class AbonnementController : ControllerBase
    {

        private gestionAPI_DBContext _context;

        public AbonnementController(gestionAPI_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAbonnements()
        {
            try
            {
                List<Abonnement> abonnements = _context.Abonnements.ToList();


                return Ok(abonnements);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost]
        public IActionResult AddAbonnement([FromBody] AbonnementModel model)
        {
            try
            {
                Abonnement abonnement = new Abonnement()
                {
                    DureeAbonnement = model.DureeAbonnement
                };

                _context.Abonnements.Add(abonnement);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
