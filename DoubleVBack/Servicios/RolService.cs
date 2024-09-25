using DoubleV.DTOs;
using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Servicios 
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerTodosLosRolesAsync()
        {
            try
            {
                // Obtener todos los roles
                var roles = await _context.Roles
                    .ToListAsync();

                return roles;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                return new List<Rol>(); // **cambio** retorno vacío en caso de excepción
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return new List<Rol>(); // **cambio** manejo de error general
            }
        }

    }
}
