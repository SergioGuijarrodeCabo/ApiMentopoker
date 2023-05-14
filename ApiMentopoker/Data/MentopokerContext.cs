﻿using ApiMentopoker.Models;
using Microsoft.EntityFrameworkCore;
using NugetMentopoker.Models;

namespace ApiMentopoker.Data
{
    public class MentopokerContext : DbContext
    {
        public MentopokerContext(DbContextOptions<MentopokerContext> options) : base(options) {}

        public DbSet<Models.PartidaModel> Partidas { get; set; }

        public DbSet<RondaModel> Rondas { get; set; }
        public DbSet<JugadaModel> Jugadas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<Celda> Celdas { get; set; }
        public DbSet<JugadasCalculadasModel> JugadasCalculadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JugadasCalculadasModel>().HasNoKey();
        }

      
    }
}
