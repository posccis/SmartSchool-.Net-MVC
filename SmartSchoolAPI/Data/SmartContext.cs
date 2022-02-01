using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options){}
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunoDisciplinas { get; set; }
        public DbSet<Curso> Cursos {get; set;}
        public DbSet<AlunoCurso> AlunosCursos { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlunoDisciplina>()
                .HasKey(AD => new {AD.AlunoId, AD.DisciplinaId});            

            builder.Entity<AlunoCurso>()
                .HasKey(AD => new {AD.AlunoId, AD.CursoId});

            builder.Entity<AlunoDisciplina>()
                .HasKey(AD => new {AD.AlunoId, AD.DisciplinaId});


            builder.Entity<AlunoDisciplina>();    
        }
    }
    
}