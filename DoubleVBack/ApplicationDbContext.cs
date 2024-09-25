using DoubleV.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

namespace DoubleV
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Tarea>().ToTable("Tareas");

            modelBuilder.Entity<Usuario>()
                .HasKey(df => df.UsuarioId);

            modelBuilder.Entity<Rol>()
                .HasKey(df => df.RolId);

            modelBuilder.Entity<Tarea>()
                .HasKey(df => df.TareaId);

            //Las relaciones
            modelBuilder.Entity<Usuario>()
                .HasOne(df => df.Rol)
                .WithMany(f => f.Usuarios);

            modelBuilder.Entity<Tarea>()
                .HasOne(df => df.Usuario)
                .WithMany(f => f.Tareas);

            base.OnModelCreating(modelBuilder);

        }
    }
}
