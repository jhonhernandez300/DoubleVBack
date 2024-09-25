using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDbContext _context;

        public TareaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TareaConUsuarioDTO>> ObtenerTareasConUsuariosAsync()
        {
            try
            {
                Console.WriteLine("Iniciando la obtención de tareas con usuarios...");
                // Obtiene todas las tareas con la relación del usuario cargada
                var tareas = await _context.Tareas
                    .Include(t => t.Usuario)
                    .ToListAsync();

                // Mapea las tareas a TareaConUsuarioDTO
                var tareasConUsuarioDTO = tareas.Select(t => new TareaConUsuarioDTO
                {
                    TareaId = t.TareaId,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    // Manejo de posible null
                    UsuarioId = t.Usuario?.UsuarioId ?? 0, 
                    UsuarioNombre = t.Usuario?.Nombre ?? "Usuario no disponible" 
                }).ToList();

                return tareasConUsuarioDTO;                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error durante la obtención de tareas.");
                Console.WriteLine($"Mensaje de error: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle de error interno: {ex.InnerException.Message}");
                }

                Console.WriteLine("Asegúrate de que la base de datos está accesible y la conexión es correcta.");
                return new List<TareaConUsuarioDTO>();
            }
        }



        public async Task<int> CrearTareaAsync(Tarea tarea)
        {
            try
            {
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();
                // Devuelve el ID de la tarea creada
                return tarea.TareaId;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                return -1; // Devuelve -1 en caso de error
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return -1; // Devuelve -1 en caso de error
            }
        }


        public async Task<List<Tarea>> GetAllTareasAsync()
        {
            return await _context.Tareas.Include(t => t.Usuario).ToListAsync();
        }

        public async Task<Tarea> GetTareaByIdAsync(int id)
        {
            return await _context.Tareas.Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.TareaId == id);
        }   

        public async Task<Tarea> UpdateTareaAsync(Tarea tarea)
        {
            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<bool> DeleteTareaAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return false;

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
