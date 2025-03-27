using gmltec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmltec.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var createdDate = new DateTime(2024, 1, 1); // Usamos una fecha estática para evitar el error

            modelBuilder.Entity<DocumentType>().HasData(
                new DocumentType { Id = 1, Name = "Cédula de Ciudadanía", Abbreviation = "CC", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 2, Name = "Tarjeta de Identidad", Abbreviation = "TI", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 3, Name = "Registro Civil", Abbreviation = "RC", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 4, Name = "Cédula de Extranjería", Abbreviation = "CE", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 5, Name = "Pasaporte", Abbreviation = "PA", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 6, Name = "Número de Identificación Tributaria", Abbreviation = "NIT", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null },
                new DocumentType { Id = 7, Name = "Permiso Especial de Permanencia", Abbreviation = "PEP", CreatedDate = createdDate, Active = true, IsDeleted = false, UpdatedDate = null }
            );
        }

    }
}
