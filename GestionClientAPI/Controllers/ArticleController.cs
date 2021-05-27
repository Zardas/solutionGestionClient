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
    [Route("api/Article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public ArticleController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        // Tout les produits
        [HttpGet]
        public IActionResult GetArticles()
        {
            try
            {
                List<Article> articles = _context.Articles.ToList();

                return Ok(articles);
            }
            catch (Exception) { }

            return BadRequest();
        }
        // Get a specific Article
        [HttpGet("{ArticleId}")]
        public IActionResult GetArticlesSpecific(int ArticleId)
        {
            try
            {
                List<Article> articles = _context.Articles.Where(a => a.ArticleId.Equals(ArticleId)).ToList();

                return Ok(articles);
            }
            catch (Exception) { }

            return BadRequest();
        }

        /* Articles dans le panier correspondants à la recherche */
        [HttpGet("Panier/{CompteId}/{stringRecherchee}")]
        public IActionResult GetArticlesRecherches_DansPanier(int CompteId, string stringRecherchee)
        {
            try
            {
                Compte compte = _context.Comptes.Where(c => c.CompteId.Equals(CompteId)).FirstOrDefault();

                Panier panier = _context.Paniers.Where(p => p.CompteId.Equals(compte.CompteId)).FirstOrDefault();

                List<Article> articleDansPanier = _context.Articles.Where(a => a.PanierId.Equals(panier.PanierId)).ToList();

                // Si une string a été recherchée
                if (!(String.IsNullOrEmpty(stringRecherchee) || String.IsNullOrWhiteSpace(stringRecherchee)))
                {

                    List<Article> articleDansPanier_newList = new List<Article>();
                    articleDansPanier.ForEach(article =>
                    {
                        if (article.Nom.ToLower().Contains(stringRecherchee.ToLower()))
                        {
                            articleDansPanier_newList.Add(article);
                        }
                    });
                    articleDansPanier = articleDansPanier_newList;
                }
                return Ok(articleDansPanier);
            }
            catch (Exception) { }

            return BadRequest();
        }
        /* Articles disponibles correspondants à la recherche */
        [HttpGet("Disponibles/{stringRecherchee}")]
        public IActionResult GetArticlesRecherches_Disponibles(string stringRecherchee)
        {
            try
            {

                List<Article> articleDansPanier = _context.Articles.Where(a => a.PanierId.Equals(1)).ToList();

                // Si une string a été recherchée
                if (!(String.IsNullOrEmpty(stringRecherchee) || String.IsNullOrWhiteSpace(stringRecherchee)))
                {

                    List<Article> articleDansPanier_newList = new List<Article>();
                    articleDansPanier.ForEach(article =>
                    {
                        if (article.Nom.ToLower().Contains(stringRecherchee.ToLower()))
                        {
                            articleDansPanier_newList.Add(article);
                        }
                    });
                    articleDansPanier = articleDansPanier_newList;
                }
                return Ok(articleDansPanier);
            }
            catch (Exception) { }

            return BadRequest();
        }


        /* Quand un client ajout l'article à son panier */
        [HttpPut("AjoutPanier/{ArticleId}")]
        public IActionResult AjoutPanierArticle(int ArticleId, [FromBody] int PanierId)
        {
            try
            {
                Panier panier = _context.Paniers.Where(p => p.PanierId.Equals(PanierId)).FirstOrDefault();
                Article article = _context.Articles.Where(a => a.ArticleId.Equals(ArticleId)).FirstOrDefault();


                panier.AjoutArticle(article);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
        /* Quand un client enlève l'article à son panier */
        [HttpPut("EnlevementPanier/{ArticleId}")]
        public IActionResult EnlevementPanierArticle(int ArticleId, [FromBody] int PanierId)
        {
            try
            {
                Panier panier = _context.Paniers.Where(p => p.PanierId.Equals(PanierId)).FirstOrDefault();
                Article article = _context.Articles.Where(a => a.ArticleId.Equals(ArticleId)).FirstOrDefault();

                panier.SupprimerPanierArticle(article);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
