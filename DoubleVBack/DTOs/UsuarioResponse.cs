using DoubleV.Modelos;

namespace DoubleV.DTOs
{
    public class UsuarioResponse
    {
        public string Message { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
