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
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private Models.Shared.gestionAPI_DBContext _context;
        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;

        public UtilisateurController(gestionAPI_DBContext context, IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _context = context;
            _jwtAuthentificationManager = jwtAuthentificationManager;
        }

        [HttpGet]
        public IActionResult GetUtilisateurs()
        {
            try
            {
                List<Utilisateur> utilisateurs = _context.Utilisateurs.ToList();

                return Ok(utilisateurs); // Error code 200 StatusCode(200, utilisateurs)
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400

        }


        // Pour la connexion
        private class ResultConnexion
        {
            public string Token;
            public string Type;
            public int Id;
        }

        [AllowAnonymous]
        [HttpGet("{Login},{MotDePasse}", Name= "GetUtilisateurs")]
        public IActionResult GetUtilisateurs(string Login, string MotDePasse)
        {
            try
            {
                Utilisateur utilisateurATrouver = _context.Utilisateurs.Where(c => (c.Login.Equals(Login) && c.MotDePasse.Equals(Utilitaire.HashPassword(MotDePasse)))).FirstOrDefault();

                if(utilisateurATrouver == null)
                {
                    return NoContent();
                }

                string type;
                
                switch(utilisateurATrouver.GetType().ToString()) {
                    case "GestionClientAPI.Models.Client":
                        type = "Client";
                        break;
                    case "GestionClientAPI.Models.Gestionnaire":
                        type = "Gestionnaire";
                        break;
                    case "GestionClientAPI.Models.Administrateur":
                        type = "Administrateur";
                        break;
                    default:
                        type = "Utilisateur";
                        break;
                }


                string token = _jwtAuthentificationManager.Authentify(utilisateurATrouver.UtilisateurId);
                ResultConnexion resultConnexion = new ResultConnexion() { Token = token, Type = type, Id = utilisateurATrouver.UtilisateurId };
                
                return Ok(resultConnexion);

            } catch(Exception) { }

            return BadRequest();
        }
    }
}
