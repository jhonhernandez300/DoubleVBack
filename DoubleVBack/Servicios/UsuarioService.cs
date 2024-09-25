using DoubleV.Interfaces;
using DoubleV.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                return await _context.Usuarios.Include(u => u.Rol).Include(u => u.Tareas).ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }                
                return new List<Usuario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return new List<Usuario>();
            }
        }


        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Rol).Include(u => u.Tareas)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}