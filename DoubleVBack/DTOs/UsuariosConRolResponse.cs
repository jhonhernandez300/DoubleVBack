using DoubleV.Modelos;

namespace DoubleV.DTOs
{
    public class UsuariosConRolResponse
    {
        public string Message { get; set; }
        public List<UsuarioConRolDTO> Usuarios { get; set; }
    }
}
