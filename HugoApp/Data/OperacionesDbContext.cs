using Microsoft.EntityFrameworkCore;
using taskmanager_webservice.Models;

namespace taskmanager_webservice.Data
{
    public class OperacionesDbContext : DbContext
    {
        public OperacionesDbContext(DbContextOptions<OperacionesDbContext> options)
            : base(options) { }

        // DbSet para Tareas
        public DbSet<Tarea> Tareas { get; set; }

        // DbSet para Usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.CorreoElectronico).HasColumnName("correoelectronico");
                entity.Property(e => e.Contraseña).HasColumnName("contrasena");
            });
            // Configuración de la tabla tareas y relación con usuario
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("tareas");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Titulo).HasColumnName("titulo");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.EstaCompletada).HasColumnName("esta_completada");
                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(e => e.Usuario)
                       .WithMany(u => u.Tareas)
                       .HasForeignKey(e => e.UsuarioId)
                        .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
