using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoubleV.Modelos
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TareaId { get; set; }
        public required string Descripcion { get; set; }
        public required int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
