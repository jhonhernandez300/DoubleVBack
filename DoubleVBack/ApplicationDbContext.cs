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
                .WithMany(f => f.Usuarios)
                .HasForeignKey(df => df.RolId);

            modelBuilder.Entity<Tarea>()
                .HasOne(df => df.Usuario)
                .WithMany(f => f.Tareas)
                .HasForeignKey(df => df.UsuarioId);

            //Seeds
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {                    
                    UsuarioId = 1,
                    Nombre = "James",
                    Email = "james@gmail.com",
                    Password = "James0101*",
                    RolId = 1
                },
                new Usuario
                {
                    UsuarioId = 2,
                    Nombre = "Radamel",
                    Email = "radamel@gmail.com",
                    Password = "Radamel0101*",
                    RolId = 2
                }
            );

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    RolId = 1,
                    Nombre = "Administrador"
                },
                new Rol
                {
                    RolId = 2,
                    Nombre = "Supervisor"
                },
                new Rol
                {
                    RolId = 3,
                    Nombre = "Empleado"
                }
            );

            modelBuilder.Entity<Tarea>().HasData(
                 new Tarea
                 {
                     TareaId = 1,
                     Descripcion = "Vender",
                     UsuarioId = 1,
                     Estado = "En Proceso"
                 },
                 new Tarea
                 {
                     TareaId = 2,
                     Descripcion = "Reparar",
                     UsuarioId = 2,
                     Estado = "Completada"
                 }
             );

            base.OnModelCreating(modelBuilder);

        }
    }
}
