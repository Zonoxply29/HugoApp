namespace taskmanager_webservice.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool EstaCompletada { get; set; }

        // Relación con el Usuario
        public int UsuarioId { get; set; }

        // (Opcional pero recomendable) Navegación a Usuario
        public Usuario Usuario { get; set; }
    }
}

