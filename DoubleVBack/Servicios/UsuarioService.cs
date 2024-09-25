using DoubleV.DTOs;
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

        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Where(u => u.UsuarioId == id)
                    .FirstOrDefaultAsync();

                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el usuario: " + ex.Message);
                return null;
            }
        }

        public async Task<List<UsuarioConRol>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                // Obtener los usuarios con su rol y mapear a UsuarioConRol
                var usuarios = await _context.Usuarios
                    .Include(u => u.Rol) // Incluir la relación con Rol
                    .Select(u => new UsuarioConRol
                    {
                        UsuarioId = u.UsuarioId,
                        Nombre = u.Nombre,
                        Email = u.Email,
                        Password = u.Password,
                        RolId = u.RolId,
                        RolNombre = u.Rol != null ? u.Rol.Nombre : string.Empty // Manejo del caso nulo
                    })
                    .ToListAsync();

                return usuarios;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                return new List<UsuarioConRol>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return new List<UsuarioConRol>(); 
            }
        }

        public async Task<Usuario> ObtenerUsuarioConRolYTareasUsandoElIdAsync(int id)
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