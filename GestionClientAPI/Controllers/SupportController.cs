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
    [Route("api/Support")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public SupportController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetSupports()
        {
            try
            {
                List<Support> supports = _context.Supports.ToList();

                return Ok(supports);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400

        }

        // Tickets de supports d'un compte
        [HttpGet("Compte/{CompteId}")]
        public IActionResult GetSupportCompte(int CompteId)
        {
            try
            {
                List<Support> supports = _context.Supports.Where(s => s.CompteId.Equals(CompteId)).ToList();

                return Ok(supports);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Tickets de supports liés à un commercial (commercial = gestionnaire)
        [HttpGet("Commercial/{GestionnaireId}")]
        public IActionResult GetSupportsGestionnaires(int GestionnaireId)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                List<Client> clients = _context.Clients.Where(c => c.GestionnaireAssocieId.Equals(gestionnaire.UtilisateurId)).ToList();

                // Liste de tout les comptes associés
                List<Compte> comptes = new List<Compte>();
                clients.ForEach(client =>
                {
                    List<Compte> comptesDuClient = _context.Comptes.Where(c => c.ClientId.Equals(client.UtilisateurId)).ToList();
                    comptesDuClient.ForEach(compte =>
                    {
                        comptes.Add(compte);
                    });

                });


                List<Support> supportsAssocies = new List<Support>();

                comptes.ForEach(compte =>
                {
                    List<Support> supports = _context.Supports.Where(s => s.CompteId.Equals(compte.CompteId)).ToList();

                    supports.ForEach(support =>
                    {
                        supportsAssocies.Add(support);
                    });

                });

                return Ok(supportsAssocies);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Tickets de supports ouverts liés à un commercial 
        [HttpGet("Commercial/Ouvert/{GestionnaireId}")]
        public IActionResult GetSupportsGestionnaires_ouvert(int GestionnaireId)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                List<Client> clients = _context.Clients.Where(c => c.GestionnaireAssocieId.Equals(gestionnaire.UtilisateurId)).ToList();

                // Liste de tout les comptes associés
                List<Compte> comptes = new List<Compte>();
                clients.ForEach(client =>
                {
                    List<Compte> comptesDuClient = _context.Comptes.Where(c => c.ClientId.Equals(client.UtilisateurId)).ToList();
                    comptesDuClient.ForEach(compte =>
                    {
                        comptes.Add(compte);
                    });

                });


                List<Support> supportsAssocies = new List<Support>();

                comptes.ForEach(compte =>
                {
                    List<Support> supports = _context.Supports.Where(s => 
                        (s.CompteId.Equals(compte.CompteId)) &&
                        (s.Status.Equals("Ouvert"))
                    ).ToList();

                    supports.ForEach(support =>
                    {
                        supportsAssocies.Add(support);
                    });

                });

                return Ok(supportsAssocies);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Ouverture d'un ticket
        [HttpPost("Ouvrir/{ArticleId}")]
        public IActionResult AddSupport(int ArticleId, [FromBody] SupportModel model)
        {
            try
            {
                Compte compte = _context.Comptes.Where(c => c.CompteId.Equals(model.CompteId)).FirstOrDefault();
                Article article = _context.Articles.Where(a => a.ArticleId.Equals(ArticleId)).FirstOrDefault();


                Support support = new Support(article)
                {
                    Objet = model.Objet,
                    Description = model.Description,
                    DateCreation = DateTime.Now,
                    Compte = compte,
                };
                _context.Supports.Add(support);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        

        // Fermeture d'un ticket
        [HttpPut("Resoudre/{SupportId}")]
        public IActionResult ResoudreSupport(int SupportId, [FromBody] string resolution)
        {
            try
            {
                Support support = _context.Supports.Where(s => s.SupportId.Equals(SupportId)).FirstOrDefault();
                support.Resoudre(resolution);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
