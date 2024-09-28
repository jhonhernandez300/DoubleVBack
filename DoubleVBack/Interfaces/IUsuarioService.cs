using DoubleV.DTOs;
using DoubleV.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoubleV.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        Task<string?> GetRoleByEmailAsync(string email);
        Task<(string Message, bool IsValid)> ValidateEmployeeCredentialsAsync(Usuario usuario);
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);
        Task<List<UsuarioConRolDTO>> ObtenerTodosLosUsuariosAsync();
        Task<Usuario> ObtenerUsuarioConRolYTareasUsandoElIdAsync(int id);        
        Task<int> CrearUsuarioAsync(Usuario usuario);
        Task<bool> BorrarUsuarioAsync(int usuarioId);
    }
}
