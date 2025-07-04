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
        }
    }
}
