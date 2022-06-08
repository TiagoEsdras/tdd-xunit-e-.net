﻿using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Cursos;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Dados
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}