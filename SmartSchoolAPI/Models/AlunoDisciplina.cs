using System;

namespace SmartSchoolAPI.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;
        }

        public int AlunoId { get; set; }

        public DateTime DataIni { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; }

        public int? Nota { get; set; } = null;

        public Aluno Aluno { get; set; }

        public int DisciplinaId { get; set; }

        public Disciplina Disciplina { get; set; }
    }
}