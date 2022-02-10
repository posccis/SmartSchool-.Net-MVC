using System.Collections.Generic;

namespace SmartSchoolAPI.Dtos
{
    public class DisciplinasDto
    {
        
        public int Id { get; set; }

        public string Nome { get; set; }

        public int CargaHoraria { get; set; }

        public int? PrerequisitoId { get; set; } = null;

        public DisciplinasDto Prerequisito { get; set; }

        public int ProfessorId { get; set; }

        public ProfessorDto Professor { get; set; }

        public int CursoId{get; set;}

        public CursoDto Curso { get; set; }

        public IEnumerable<AlunoDto> Alunos { get; set; }
    }
}