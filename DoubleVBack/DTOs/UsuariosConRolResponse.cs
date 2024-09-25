using DoubleV.Modelos;

namespace DoubleV.DTOs
{
    public class UsuariosConRolResponse
    {
        public string Message { get; set; }
        public List<UsuarioConRol> Usuarios { get; set; }
    }
}
