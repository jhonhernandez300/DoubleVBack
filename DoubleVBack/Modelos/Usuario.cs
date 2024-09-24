using System.Threading;

namespace DoubleV.Modelos
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public ICollection<Tarea> Tareas { get; set; }
    }
}
