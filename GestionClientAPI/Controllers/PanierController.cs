using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionClientAPI.Models.Shared;
using GestionClientAPI.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace GestionClientAPI.Controllers
{
    [Authorize]
    [Route("api/Panier")]
    [ApiController]
    public class PanierController : ControllerBase
    {
        private gestionAPI_DBContext _context;

        public PanierController(gestionAPI_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPaniers()
        {
            try
            {
                List<Panier> paniers = _context.Paniers.ToList();


                return Ok(paniers);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpGet("{CompteId}")]
        public IActionResult GetPaniers(int CompteId)
        {
            try
            {
                Panier panier = _context.Paniers.Where(p => p.CompteId.Equals(CompteId)).FirstOrDefault();

                return Ok(panier);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }
    }
}
