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
    [Route("api/Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private Models.Shared.gestionAPI_DBContext _context;

        public ServiceController(gestionAPI_DBContext context)
        {
            _context = context;
        }


        // Tout les services
        [HttpGet]
        public IActionResult GetService()
        {
            try
            {
                List<Service> services = _context.Services.ToList();

                return Ok(services);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddService([FromBody] ServiceModel model)
        {
            try
            {
                Abonnement abonnement = _context.Abonnements.Where(a => a.AbonnementId.Equals(model.AbonnementId)).FirstOrDefault();

                Service service = new Service()
                {
                    Nom = model.Nom,
                    Image = model.Image,
                    Type = model.Type,
                    Prix = model.Prix,
                    Description = model.Description,
                    Manuel = model.Manuel,
                    Conditions = model.Conditions,
                    Abonnement = abonnement,
                    PanierId = 1 // Le panier nul
                };

                _context.Services.Add(service);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPut("{ServiceId}")]
        public IActionResult ModifierProduit(int serviceId, [FromBody] ServiceModel model)
        {

            try
            {

                Service service = _context.Services.Where(p => p.ArticleId.Equals(serviceId)).FirstOrDefault();
                Abonnement abonnement = _context.Abonnements.Where(a => a.AbonnementId.Equals(model.AbonnementId)).FirstOrDefault();

                service.Nom = model.Nom;
                service.Image = model.Image;
                service.Type = model.Type;
                service.Prix = model.Prix;
                service.Description = model.Description;
                service.Conditions = model.Conditions;
                service.Abonnement = abonnement;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpDelete("{ServiceId}")]
        public IActionResult RemoveClient(int ServiceId)
        {
            try
            {
                Service service = _context.Services.Where(s => s.ArticleId.Equals(ServiceId)).FirstOrDefault();

                _context.Remove(service);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

    }
}
