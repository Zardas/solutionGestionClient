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
    [Route("api/Compte")]
    [ApiController]
    public class CompteController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public CompteController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetComptes()
        {
            try
            {
                List<Compte> comptes = _context.Comptes.ToList();

                return Ok(comptes);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400

        }

        // Get a specific compte
        [HttpGet("{CompteId}")]
        public IActionResult GetComptesSpecifique(int CompteId)
        {
            try
            {
                Compte compte = _context.Comptes.Where(c => c.CompteId.Equals(CompteId)).FirstOrDefault();

                return Ok(compte);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Get les comptes d'un client spécifique
        [HttpGet("Client/{ClientId}")]
        public IActionResult GetGestionnaire(int ClientId)
        {
            try
            {
                List<Compte> comptes = _context.Comptes.Where(c => c.ClientId.Equals(ClientId)).ToList();

                return Ok(comptes);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost]
        public IActionResult AddCompte([FromBody] CompteModel model)
        {
            try
            {
                Client client = _context.Clients.Where(c => c.UtilisateurId.Equals(model.ClientId)).FirstOrDefault();

                Compte compteATrouver = _context.Comptes.Where(c => (
                        c.ClientId.Equals(client.UtilisateurId) &&
                        c.NomCompte.Equals(model.NomCompte)
                )).FirstOrDefault();

                if (compteATrouver == null)
                {

                    Compte newCompte = new Compte() { ClientId = client.UtilisateurId, DateCreation = System.DateTime.Now, NomCompte = model.NomCompte };
                    Panier newPanier = new Panier() { Compte = newCompte };


                    _context.Comptes.Add(newCompte);
                    _context.Paniers.Add(newPanier);
                    _context.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPut("{CompteId}")]
        public IActionResult ModifierCompte(int CompteId, [FromBody] CompteModel model)
        {
            try
            {
                Compte compte = _context.Comptes.Where(c => c.CompteId.Equals(CompteId)).FirstOrDefault();

                compte.NomCompte = model.NomCompte;

                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
