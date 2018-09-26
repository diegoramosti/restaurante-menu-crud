using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Models;

namespace RestauranteApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :
            base(options)
        { }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prato>()
                .HasOne(p => p.Restaurante)
                .WithMany(b => b.Pratos)
                .HasForeignKey(p => p.RestauranteId)
                .HasConstraintName("ForeignKey_Restaurante_Prato");

            modelBuilder.Entity<Restaurante>()
                .HasKey(p => p.RestauranteId);

            modelBuilder.Entity<Prato>()
                .HasKey(p => p.PratoId);
        }

    }
}
