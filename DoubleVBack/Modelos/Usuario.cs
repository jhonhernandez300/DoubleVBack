using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Text.Json.Serialization;

namespace DoubleV.Modelos
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? RolId { get; set; }

        [JsonIgnore]
        public Rol? Rol { get; set; }

        [JsonIgnore]
        public ICollection<Tarea>? Tareas { get; set; }
    }
}
