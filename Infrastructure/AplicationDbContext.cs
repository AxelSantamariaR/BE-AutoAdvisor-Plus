using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Asesor> Asesores { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Usado> Usados { get; set; }
        public DbSet<Nuevo> Nuevos { get; set; }
        public DbSet<Cita> citas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asesor>().Property(a => a.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Asesor>().Property(a => a.FechaReg).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Horario>().Property(h => h.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Horario>().Property(h => h.FechaReg).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Auto>().Property(a => a.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Auto>().Property(a => a.FechaReg).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Usado>().Property(h => h.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Usado>().Property(h => h.FechaReg).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Nuevo>().Property(a => a.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Nuevo>().Property(a => a.FechaReg).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Cita>().Property(a => a.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Cita>().Property(a => a.FechaReg).HasDefaultValueSql("GETDATE()");

        }

    }
}