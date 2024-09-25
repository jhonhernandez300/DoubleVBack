using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoubleV.Modelos
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolId { get; set; }
        public required string Nombre { get; set; }

        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
