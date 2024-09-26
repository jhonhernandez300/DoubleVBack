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

        public async Task<bool> BorrarUsuarioAsync(int usuarioId)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(u => u.Tareas)
                                                     .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

                if (usuario == null)
                {
                    // Usuario no encontrado
                    return false; 
                }

                // Al eliminar el usuario, también se eliminan sus tareas debido a la cascada configurada en OnModelCreating
                _context.Usuarios.Remove(usuario); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return false;
            }
        }

        public async Task<int> CrearUsuarioAsync(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return usuario.UsuarioId;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("Error de base de datos: " + dbEx.Message);
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine("Detalle: " + dbEx.InnerException.Message);
                }
                // Devuelve -1 en caso de error
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return -1;
            }
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

        public async Task<List<UsuarioConRolDTO>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                // Obtener los usuarios con su rol y mapear a UsuarioConRol
                var usuarios = await _context.Usuarios
                    .Include(u => u.Rol) // Incluir la relación con Rol
                    .Select(u => new UsuarioConRolDTO
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
                return new List<UsuarioConRolDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return new List<UsuarioConRolDTO>();
            }
        }

        public async Task<Usuario> ObtenerUsuarioConRolYTareasUsandoElIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Rol).Include(u => u.Tareas)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

    }
}