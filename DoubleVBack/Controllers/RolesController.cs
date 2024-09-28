using DoubleV.DTOs;
using DoubleV.Helpers;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using DoubleV.Servicios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigins")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IRolService _rolService;

        public RolesController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet("ObtenerTodosLosRolesAsync")]
        [AuthorizeRoles("Administrador")]
        public async Task<ActionResult<RolesResponse>> ObtenerTodosLosRolesAsync()
        {
            try
            {
                var roles = await _rolService.ObtenerTodosLosRolesAsync();

                if (roles == null || !roles.Any())
                {
                    return Ok(new RolesResponse { Message = "No se encontraron roles", Roles = new List<Rol>() });
                }

                return Ok(new RolesResponse { Roles = roles });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerTodosLosRolesAsync: " + ex.Message);
                return StatusCode(500, new RolesResponse { Message = "Error interno del servidor" });
            }
        }

    }
}
