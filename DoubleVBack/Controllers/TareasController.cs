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
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost("CrearTarea")]
        public async Task<ActionResult<Tarea>> CrearTarea([FromBody] Tarea tarea)
        {
            if (tarea == null)
            {
                return BadRequest(new ApiResponse { Message = "Los datos de la tarea son requeridos.", Data = null });
            }
            try 
            {
                if (await _tareaService.CrearTareaAsync(tarea))
                {
                    return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tarea.TareaId }, new ApiResponse { Message = "Tarea creada exitosamente.", Data = tarea });
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

        [HttpGet]
        public async Task<ActionResult<List<Tarea>>> GetTareas()
        {
            return await _tareaService.GetAllTareasAsync();
        }          

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarea>> UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.TareaId) return BadRequest();
            await _tareaService.UpdateTareaAsync(tarea);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTarea(int id)
        {
            var result = await _tareaService.DeleteTareaAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
