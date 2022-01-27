using System;

namespace SmartSchoolAPI.Models
{
    public class AlunoCurso
    {
        public AlunoCurso(int alunoId, int cursoId)
        {
            this.AlunoId = alunoId;

            this.CursoId = cursoId;
        }

        public int AlunoId { get; set; }

        public DateTime DataIni { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; }


        public Aluno Aluno { get; set; }

        public int CursoId { get; set; }

        public Disciplina Disciplina { get; set; }
    }
}