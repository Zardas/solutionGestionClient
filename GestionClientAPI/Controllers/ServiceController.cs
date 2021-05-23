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
