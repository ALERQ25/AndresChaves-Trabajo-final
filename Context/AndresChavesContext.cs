using AndresChaves.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AndresChaves.Context
{
    public class AndresChavesContext : DbContext
    {
        public AndresChavesContext() : base("name=AndresChavesContext")
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Personas> Personas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuarios
            modelBuilder.Entity<Usuarios>()
                .ToTable("Usuarios")
                .HasKey(u => u.Id_Usuarios);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Id_Usuarios)
                .IsRequired()
                .HasColumnName("Id_Usuarios");

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Usuario)
                .IsRequired()
                .HasColumnName("Usuario")
                .HasMaxLength(50);

          


            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Contraseña)
                .IsRequired()
                .HasColumnName("Contraseña")
                .HasMaxLength(50);
       

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Fecha_creacion)
                .IsRequired()
                .HasColumnName("Fecha_creacion");

            // Configuración de la entidad Personas
            modelBuilder.Entity<Personas>()
                .ToTable("Personas")
                .HasKey(p => p.Id_Personas);

            modelBuilder.Entity<Personas>()
                .Property(p => p.Nombres)
                .IsRequired()
                .HasColumnName("Nombres")
                .HasMaxLength(50);

            modelBuilder.Entity<Personas>()
                .Property(p => p.Apellidos)
                .IsRequired()
                .HasColumnName("Apellidos")
                .HasMaxLength(50);

            modelBuilder.Entity<Personas>()
                .Property(p => p.Num_identificacion)
                .IsRequired()
                .HasColumnName("Num_identificacion")
                .HasMaxLength(50);

            modelBuilder.Entity<Personas>()
                .Property(p => p.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(50);

            modelBuilder.Entity<Personas>()
                .Property(p => p.Tipo_identificacion)
                .IsRequired()
                .HasColumnName("Tipo_identificacion")
                .HasMaxLength(50);
           
            modelBuilder.Entity<Personas>()
                .Property(p => p.Fecha_creacion)
                .IsRequired()
                .HasColumnName("Fecha_creacion");

            base.OnModelCreating(modelBuilder); // Llama al método base
        }
    }
}