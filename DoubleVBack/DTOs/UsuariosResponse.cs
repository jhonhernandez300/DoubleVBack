using DoubleV.Modelos;

namespace DoubleV.DTOs
{
    public class UsuariosResponse
    {
        public string Message { get; set; }
        public List<UsuarioConRol> Usuarios { get; set; }
    }
}
