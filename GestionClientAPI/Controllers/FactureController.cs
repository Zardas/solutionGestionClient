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
    [Route("api/Facture")]
    [ApiController]
    public class FactureController : ControllerBase
    {
        private gestionAPI_DBContext _context;

        public FactureController(gestionAPI_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFactures()
        {
            try
            {
                List<Facture> factures = _context.Factures.ToList();


                return Ok(factures);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Facture d'un compte
        [HttpGet("{CompteId}")]
        public IActionResult GetFactures(int CompteId)
        {
            try
            {
                Facture factures = _context.Factures.Where(f => f.CompteId.Equals(CompteId)).FirstOrDefault();

                return Ok(factures);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost("Generer")]
        public IActionResult AddFacture([FromBody] int CompteId)
        {
            try
            {
                Compte compte = _context.Comptes.Where(c => c.CompteId.Equals(CompteId)).FirstOrDefault();

                Panier panier = _context.Paniers.Where(p => p.CompteId.Equals(compte.CompteId)).FirstOrDefault();
                Client client = _context.Clients.Where(c => c.UtilisateurId.Equals(compte.ClientId)).FirstOrDefault();

                List<Article> articlesDansPanier = _context.Articles.Where(a => a.PanierId.Equals(panier.PanierId)).ToList();

                int montantTotal = 0;
                articlesDansPanier.ForEach(article =>
                {
                    montantTotal += article.Prix;
                });


                // Ne devrait jamais arriver puisque on fait la vérification coté client
                if (montantTotal > client.Solde)
                {
                    return BadRequest();
                }
                else
                {
                    Facture facture = new Facture()
                    {
                        Compte = compte,
                        DateEmission = DateTime.Now,
                        Montant = montantTotal
                    };
                    _context.Factures.Add(facture);

                    // On retire les articles du panier
                    articlesDansPanier.ForEach(article =>
                    {
                        panier.SupprimerPanierArticle(article);
                        _context.Articles.Remove(article);
                    });

                    // Le gestionnaire associé gagne 15% du montant total
                    Gestionnaire gestionnaireAssocie = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(client.GestionnaireAssocieId)).FirstOrDefault();
                    gestionnaireAssocie.ajoutFacture(facture);

                    // On réduit évidemment le solde du client
                    client.GenererFacture(facture);

                    _context.SaveChanges();

                    return Ok();
                }

            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
