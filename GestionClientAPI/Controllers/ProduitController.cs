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
    [Route("api/Produit")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public ProduitController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        // Tout les produits
        [HttpGet]
        public IActionResult GetProduits()
        {
            try
            {
                List<Produit> produits = _context.Produits.ToList();

                return Ok(produits);
            }
            catch (Exception) { }

            return BadRequest();
        }

        /* Clients associés à un gestionnaire */
        [HttpGet("GestionnaireAssocie/{GestionnaireId}")]
        public IActionResult GetClients_GestionnaireAssocie(int GestionnaireId)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                List<Produit> produits = _context.Produits.Where(p => p.StockId.Equals(gestionnaire.StockId)).ToList();

                return Ok(produits);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost("{GestionnaireId}")]
        public IActionResult AddProduit(int GestionnaireId, [FromBody] ProduitModel model)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();
                Abonnement abonnementNul = _context.Abonnements.Where(a => a.AbonnementId.Equals(1)).FirstOrDefault();

                Produit produit = new Produit()
                {
                    Nom = model.Nom,
                    Image = model.Image,
                    Fabricant = model.Fabricant,
                    Type = model.Type,
                    Prix = model.Prix,
                    Quantite = model.Quantite,
                    Capacite = model.Capacite,
                    Description = model.Description,
                    Manuel = model.Manuel,
                    StockId = gestionnaire.StockId,
                    Abonnement = abonnementNul,
                    PanierId = 1 // Le panier nul
                };

                _context.Produits.Add(produit);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPut("{ProduitId}")]
        public IActionResult ModifierProduit(int produitId, [FromBody] ProduitModel model)
        {

            try
            {

                Produit produit = _context.Produits.Where(p => p.ArticleId.Equals(produitId)).FirstOrDefault();

                produit.Nom = model.Nom;
                produit.Image = model.Image;
                produit.Fabricant = model.Fabricant;
                produit.Type = model.Type;
                produit.Prix = model.Prix;
                produit.Quantite = model.Quantite;
                produit.Capacite = model.Capacite;
                produit.Description = model.Description;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }


        [HttpDelete("RetirerUn/{ProduitId}")]
        public IActionResult RetirerProduit(int ProduitId)
        {
            try
            {
                Produit produit = _context.Produits.Where(p => p.ArticleId.Equals(ProduitId)).FirstOrDefault();

                // On retire 1 à la quantité, et si la quantité arrive à zéro, on retire l'article
                produit.Quantite--;
                if (produit.Quantite <= 0)
                {
                    _context.Remove(produit);
                }

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

    }
}
