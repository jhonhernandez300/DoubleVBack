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

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> ObtenerUsuarioPorIdAsync(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

                if (usuarioEncontrado == null)
                {
                    return Ok(new UsuarioResponse
                    {
                        Message = $"No se encontró el usuario con ID {id}",
                        Usuarios = new List<Usuario>()
                    });
                }
               
                var usuario = new Usuario
                {
                    UsuarioId = usuarioEncontrado.UsuarioId,
                    Nombre = usuarioEncontrado.Nombre,
                    Email = usuarioEncontrado.Email,
                    Password = usuarioEncontrado.Password,
                    RolId = usuarioEncontrado.RolId                    
                };

                return Ok(new UsuarioResponse
                {
                    Message = "Usuario encontrado",
                    Usuarios = new List<Usuario> { usuario }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerUsuarioPorIdAsync: " + ex.Message);
                return StatusCode(500, new UsuarioResponse
                {
                    Message = "Error interno del servidor",
                    Usuarios = new List<Usuario>()
                });
            }
        }

        [HttpGet("ObtenerTodosLosUsuariosAsync")]
        public async Task<ActionResult<UsuariosConRolResponse>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    return Ok(new UsuariosConRolResponse { Message = "No se encontraron usuarios", Usuarios = new List<UsuarioConRol>() });
                }

                return Ok(new UsuariosConRolResponse { Usuarios = usuarios });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetUsuarios: " + ex.Message);
                return StatusCode(500, new UsuariosConRolResponse { Message = "Error interno del servidor" });
            }
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        //    if (usuario == null) return NotFound();
        //    return usuario;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        //{
        //    var createdUsuario = await _usuarioService.CreateUsuarioAsync(usuario);
        //    return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.UsuarioId }, createdUsuario);
        //}

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
