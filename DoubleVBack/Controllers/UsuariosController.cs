using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors;
using DoubleV.Helpers;

namespace DoubleV.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigins")]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public IConfiguration _configuration;        

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest(new { message = "El usuario no puede ser nulo." });
                }

                if (string.IsNullOrWhiteSpace(login.Correo) ||
                    string.IsNullOrWhiteSpace(login.Password))
                {
                    return BadRequest(new { message = "El correo o el password no pueden ser nulos o estar vacíos." });
                }

                var usuario = _mapper.Map<Usuario>(login);
                var (message, isValid) = await _usuarioService.ValidateEmployeeCredentialsAsync(usuario);
                if (!isValid)
                {
                    return Unauthorized(new { Message = message, IsValid = isValid });
                }

                var roleName = await _usuarioService.GetRoleByEmailAsync(login.Correo);
                if (roleName == null)
                {
                    return NotFound(new { message = "No se encontró un rol para este usuario." });
                }

                var token = GenerateJwtToken(login, roleName);

                return Ok(new
                {
                    token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Ocurrió un error interno en el servidor: {ex.Message}" });
            }
        }

        private string GenerateJwtToken(LoginDTO login, string nombreRol)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("correo", login.Correo),
                new Claim("password", login.Password),
                new Claim(ClaimTypes.Role, nombreRol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }       

        [HttpDelete("BorrarUsuario/{id}")]
        [AuthorizeRoles("Administrador")]
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
        [AuthorizeRoles("Administrador", "Supervisor")]
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
        [AuthorizeRoles("Administrador")]
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
        [AuthorizeRoles("Administrador")]
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
