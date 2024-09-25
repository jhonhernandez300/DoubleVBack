namespace DoubleV.DTOs
{
    public class UsuarioSinIdDTO
    {        
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? RolId { get; set; }
    }
}
