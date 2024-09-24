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

        [HttpGet]
        public async Task<ActionResult<List<Tarea>>> GetTareas()
        {
            return await _tareaService.GetAllTareasAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);
            if (tarea == null) return NotFound();
            return tarea;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> CreateTarea(Tarea tarea)
        {
            var createdTarea = await _tareaService.CreateTareaAsync(tarea);
            return CreatedAtAction(nameof(GetTarea), new { id = createdTarea.TareaId }, createdTarea);
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
