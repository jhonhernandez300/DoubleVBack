using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ObtenerTodosLosUsuariosAsync")]
        public async Task<ActionResult<UsuariosResponse>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return Ok(new UsuariosResponse { Message = "No se encontraron usuarios", Usuarios = new List<UsuarioConRol>() });
                }

                return Ok(new UsuariosResponse { Usuarios = usuarios });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetUsuarios: " + ex.Message);
                return StatusCode(500, new UsuariosResponse { Message = "Error interno del servidor" });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            var createdUsuario = await _usuarioService.CreateUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.UsuarioId }, createdUsuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId) return BadRequest();
            await _usuarioService.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
