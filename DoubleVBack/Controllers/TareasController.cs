using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace DoubleV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public TareasController(ITareaService tareaService, IUsuarioService usuarioService, IMapper mapper)
        {
            _tareaService = tareaService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpDelete("BorrarTarea/{id}")]
        public async Task<ActionResult<ApiResponse>> BorrarTarea(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse { Message = "El ID de la tarea es requerido.", Data = null });
            }
            try
            {
                var resultado = await _tareaService.BorrarTareaAsync(id);

                if (resultado)
                {
                    return Ok(new ApiResponse
                    {
                        Message = "Tarea eliminada exitosamente.",
                        Data = null
                    });
                }
                return NotFound(new ApiResponse { Message = "Tarea no encontrada.", Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Error al borrar la tarea.", Error = ex.Message });
            }
        }

        [HttpGet("ObtenerTareasConUsuarios")]
        public async Task<ActionResult<ApiResponse>> ObtenerTareasConUsuarios()
        {
            try
            {
                var tareasConUsuarios = await _tareaService.ObtenerTareasConUsuariosAsync();
                var tareasConUsuariosDTO = _mapper.Map<IEnumerable<TareaConUsuarioDTO>>(tareasConUsuarios);

                return Ok(new ApiResponse
                {
                    Message = "Tareas obtenidas exitosamente.",
                    Data = tareasConUsuariosDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Message = "Error al obtener tareas.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("CrearTarea")]
        public async Task<ActionResult<ApiResponse>> CrearTarea([FromBody] TareaSinIdDTO tarea)
        {
            if (tarea == null)
            {
                return BadRequest(new ApiResponse { Message = "Los datos de la tarea son requeridos.", Data = null });
            }

            try
            {
                var tareaMapeada = _mapper.Map<Tarea>(tarea);

                int tareaId = await _tareaService.CrearTareaAsync(tareaMapeada);
                if (tareaId > 0) // Si se creó correctamente, el ID será mayor que 0
                {
                    return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tareaId }, new ApiResponse { Message = "Tarea creada exitosamente.", Data = tareaId });
                }
                return BadRequest(new ApiResponse { Message = "Fallo al crear la tarea", Data = null });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Error al crear una tarea.", Error = ex.Message });
            }
        }

        [HttpGet("ObtenerTareaPorId/{id}")]
        public async Task<ActionResult<Tarea>> ObtenerTareaPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse { Message = "id inválido para la tarea", Data = null });
            }
            try
            {
                var tareaEncontrada = await _tareaService.GetTareaByIdAsync(id);
                if (tareaEncontrada == null)
                {
                    return NotFound(new ApiResponse { Message = $"Tarea con el id {id} no fue encontrada.", Data = null });
                }
                return Ok(new ApiResponse { Message = "Tarea encontrada.", Data = tareaEncontrada });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Error al buscar tarea por id.", Error = ex.Message });
            }
        }                   

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarea>> UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.TareaId) return BadRequest();
            await _tareaService.UpdateTareaAsync(tarea);
            return NoContent();
        }
        
    }
}
