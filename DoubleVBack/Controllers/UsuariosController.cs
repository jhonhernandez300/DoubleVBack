using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace DoubleV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpDelete("BorrarUsuario/{id}")]
        public async Task<ActionResult<ApiResponse>> BorrarUsuario(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse { Message = "Los datos del usuario son requeridos.", Data = null });
            }
            try
            {
                var resultado = await _usuarioService.BorrarUsuarioAsync(id);

                if (resultado)
                {
                    return Ok(new ApiResponse
                    {
                        Message = "Usuario y sus tareas asociadas eliminados exitosamente.",
                        Data = null
                    });
                }
                return NotFound(new ApiResponse { Message = "Usuario no encontrado.", Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Error al borrar el usuario.", Error = ex.Message });
            }
        }

        [HttpPost("CrearUsuario")] 
        public async Task<ActionResult<ApiResponse>> CrearUsuario([FromBody] UsuarioSinIdDTO usuarioSinIdDto) 
        {
            if (usuarioSinIdDto == null) 
            {
                return BadRequest(new ApiResponse { Message = "Los datos del usuario son requeridos.", Data = null }); 
            }

            try
            {
                var usuarioMapeado = _mapper.Map<Usuario>(usuarioSinIdDto); 

                int usuarioId = await _usuarioService.CrearUsuarioAsync(usuarioMapeado);

                // Si se creó correctamente, el ID será mayor que 0
                if (usuarioId > 0) 
                {
                    return Ok(new ApiResponse
                    {
                        Message = "Usuario creado exitosamente.",
                        Data = usuarioId
                    });

                }
                return BadRequest(new ApiResponse { Message = "Fallo al crear el usuario", Data = null }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Error al crear un usuario.", Error = ex.Message }); 
            }
        }

        [HttpGet("ObtenerUsuarioPorIdAsync/{id}")]
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
                    return Ok(new UsuariosConRolResponse { Message = "No se encontraron usuarios", Usuarios = new List<UsuarioConRolDTO>() });
                }

                return Ok(new UsuariosConRolResponse { Usuarios = usuarios });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GetUsuarios: " + ex.Message);
                return StatusCode(500, new UsuariosConRolResponse { Message = "Error interno del servidor" });
            }
        }   

       
    }
}
