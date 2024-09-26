using DoubleV.DTOs;
using DoubleV.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Interfaces
{
    public interface ITareaService
    {
        Task<bool> ActualizarTareaAsync(Tarea tarea);
        Task<Tarea?> ObtenerTareaPorIdAsync(int tareaId);
        Task<IEnumerable<TareaConUsuarioDTO>> ObtenerTareasConUsuariosAsync();
        Task<List<Tarea>> GetAllTareasAsync();        
        Task<int> CrearTareaAsync(Tarea tarea);
        Task<Tarea> UpdateTareaAsync(Tarea tarea);
        Task<bool> BorrarTareaAsync(int tareaId);
    }
}
