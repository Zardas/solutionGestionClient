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
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private gestionAPI_DBContext _context;

        public RoleController(gestionAPI_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                List<Role> roles = _context.Roles.ToList();


                return Ok(roles);
            }
            catch (Exception) { }

            return BadRequest(); // Error code 400
        }

        [HttpPost]
        public IActionResult AddRole([FromBody] RoleModel model)
        {
            try
            {
                Role role = new Role()
                {
                    Title = model.Title
                };

                _context.Roles.Add(role);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception) { }

            return BadRequest();
        }

    }
}
