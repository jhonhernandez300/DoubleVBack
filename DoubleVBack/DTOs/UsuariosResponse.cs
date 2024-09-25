using DoubleV.Modelos;

namespace DoubleV.DTOs
{
    public class UsuariosResponse
    {
        public string Message { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
