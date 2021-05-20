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

    [Route("api/Utilisateur")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public UtilisateurController(gestionAPI_DBContext context)
        {
            _context = context;
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


        private class ResultConnexion
        {
            public int Id;
            public string Type;
        }

        [HttpGet("{Login},{MotDePasse}", Name= "GetUtilisateurs")]
        public IActionResult GetUtilisateurs(string Login, string MotDePasse)
        {
            try
            {
                Utilisateur utilisateurATrouver = _context.Utilisateurs.Where(c => (c.Login.Equals(Login) && c.MotDePasse.Equals(Utilitaire.HashPassword(MotDePasse)))).FirstOrDefault();

                if(utilisateurATrouver == null)
                {
                    Debug.WriteLine("FLAG B");
                    return NoContent();
                }
                Debug.WriteLine("FLAG A : " + utilisateurATrouver.GetType());

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

                Debug.WriteLine("FLAG C " + type);

                ResultConnexion resultConnexion = new ResultConnexion() { Id = utilisateurATrouver.UtilisateurId, Type = type };
                return Ok(resultConnexion);

            } catch(Exception) { }

            return BadRequest();
        }
    }
}
