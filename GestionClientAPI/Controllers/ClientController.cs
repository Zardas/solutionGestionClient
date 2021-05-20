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
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private Models.Shared.gestionAPI_DBContext _context;

        public ClientController(gestionAPI_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            try
            {
                List<Client> clients = _context.Clients.ToList();
                return Ok(clients); // Error code 200 StatusCode(200, clients)
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] ClientModel model)
        {
            try
            {
                // On choisit un gestionnaire au hasard à affecter au client
                List<Gestionnaire> gestionnaires = _context.Gestionnaires.ToList();

                Random rand = new Random();
                int index = rand.Next(gestionnaires.Count);

                Debug.WriteLine("Gestionnaire " + gestionnaires[index].NomGestionnaire + " choisi au hasard");


                Client client = new Client(model.MotDePasse)
                {
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Age = model.Age,
                    Login = model.Login,
                    Mail = model.Mail,
                    Telephone = model.Telephone,
                };

                client.GestionnaireAssocieId = gestionnaires[index].UtilisateurId;
                gestionnaires[index].ClientsAssocies.Add(client);


                _context.Clients.Add(client);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }
    }
}
