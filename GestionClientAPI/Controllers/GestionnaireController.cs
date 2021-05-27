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
    [Route("api/Gestionnaire")]
    [ApiController]
    public class GestionnaireController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public GestionnaireController(gestionAPI_DBContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult GetGestionnaires()
        {
            try
            {
                List<Gestionnaire> gestionnaires = _context.Gestionnaires.ToList();

                return Ok(gestionnaires);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        // Get a specific gestionnaire
        [HttpGet("{GestionnaireId}")]
        public IActionResult GetGestionnaire(int GestionnaireId)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(g => g.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                return Ok(gestionnaire);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost]
        public IActionResult AddGestionnaire([FromBody] GestionnaireModel model)
        {
            try
            {
                Stock stock = new Stock()
                {
                    Titre = "Stock_" + model.Login
                };

                _context.Stocks.Add(stock);
                _context.SaveChanges();

                Gestionnaire gestionnaire = new Gestionnaire()
                {
                    Login = model.Login,
                    Email = model.Email,
                    NomGestionnaire = model.NomGestionnaire,
                    MotDePasse = Utilitaire.HashPassword(model.MotDePasse),
                    RoleId = model.RoleId,
                    StockId = stock.StockId
                };

                _context.Gestionnaires.Add(gestionnaire);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        //[HttpPut("Modification/{GestionnaireId}")] POUR MODIFICER l'URL
        [HttpPut("{GestionnaireId}")]
        public IActionResult ModifierGestionnaire(int GestionnaireId, [FromBody] GestionnaireModel model)
        {

            try
            {

                Gestionnaire gestionnaire = _context.Gestionnaires.Where(c => c.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                gestionnaire.Login = model.Login;
                gestionnaire.Email = model.Email;
                gestionnaire.NomGestionnaire = model.NomGestionnaire;
                gestionnaire.RoleId = model.RoleId;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpDelete("{GestionnaireId}")]
        public IActionResult RemoveClient(int GestionnaireId)
        {
            try
            {
                Gestionnaire gestionnaire = _context.Gestionnaires.Where(c => c.UtilisateurId.Equals(GestionnaireId)).FirstOrDefault();

                // On doit aussi affecter un nouveau gestionnaire à tout les client qui lui était précédemment affectés
                List<Client> clientsAssocies = _context.Clients.Where(c => c.GestionnaireAssocieId.Equals(gestionnaire.UtilisateurId)).ToList();

                List<Gestionnaire> autresGestionnaires = _context.Gestionnaires.Where(g => !(g.UtilisateurId.Equals(gestionnaire.UtilisateurId))).ToList();

                clientsAssocies.ForEach(client =>
                {
                    Random rand = new Random();
                    int index = rand.Next(autresGestionnaires.Count);

                    Debug.WriteLine("Gestionnaire " + autresGestionnaires[index].NomGestionnaire + " choisi au hasard");
                    client.GestionnaireAssocieId = autresGestionnaires[index].UtilisateurId;

                    autresGestionnaires[index].ClientsAssocies.Add(client);
                });


                _context.Remove(gestionnaire);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }


       

    }
}
