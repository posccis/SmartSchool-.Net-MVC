using System.Collections.Generic;

namespace SmartSchoolAPI.Dtos
{
    public class CursoDto
    {
        public int Id { get; set; }

        public string Nome {get; set;}

        public IEnumerable<DisciplinasDto> Disciplinas { get; set; }
    }
}