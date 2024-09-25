
using DoubleV.DTOs;
using DoubleV.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DoubleV.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> ObtenerTodosLosRolesAsync();
    }
}
