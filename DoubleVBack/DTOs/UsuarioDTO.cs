namespace DoubleV.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? RolId { get; set; }
    }
}
