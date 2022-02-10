using System;
using System.Collections.Generic;

namespace SmartSchoolAPI.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }

        public int Registro { get; set; }

        public string Nome { get; set; }

        public DateTime DataIni { get; set; }

        public bool Ativo { get; set; } = true;

        public IEnumerable<DisciplinasDto> Disciplinas { get; set; }
    }
}