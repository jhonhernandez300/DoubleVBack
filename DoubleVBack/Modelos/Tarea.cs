namespace DoubleV.Modelos
{
    public class Tarea
    {
        public int TareaId { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
