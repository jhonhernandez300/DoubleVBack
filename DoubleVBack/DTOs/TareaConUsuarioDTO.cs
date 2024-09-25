namespace DoubleV.DTOs
{
    public class TareaConUsuarioDTO
    {
        public int TareaId { get; set; }
        public required string Descripcion { get; set; }        
        public required string Estado { get; set; }
        public required int UsuarioId { get; set; }
        public required string UsuarioNombre { get; set; }
    }
}
