using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TblUsuario> TblUsuarios { get; set; }
        public DbSet<TblTarea> TblTareas { get; set; }
        public DbSet<TblPrioridad> TblPrioridad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblPrioridad>().HasData(
                new TblPrioridad { Id = 1, Nombre = "Alto" },
                new TblPrioridad { Id = 2, Nombre = "Medio" },
                new TblPrioridad { Id = 3, Nombre = "Bajo" }
            );
        }
    }
}
