using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoubleV.Modelos
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TareaId { get; set; }
        public required string Descripcion { get; set; }
        public required int UsuarioId { get; set; }
        public required string Estado { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
