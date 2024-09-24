using DoubleV.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Interfaces
{
    public interface ITareaService
    {
        Task<List<Tarea>> GetAllTareasAsync();
        Task<Tarea> GetTareaByIdAsync(int id);
        Task<Tarea> CreateTareaAsync(Tarea tarea);
        Task<Tarea> UpdateTareaAsync(Tarea tarea);
        Task<bool> DeleteTareaAsync(int id);
    }
}
