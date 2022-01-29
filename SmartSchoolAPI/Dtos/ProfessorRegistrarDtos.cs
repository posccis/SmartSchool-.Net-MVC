using System;

namespace SmartSchoolAPI.Dtos
{
    public class ProfessorRegistrarDtos
    {
        public int Id { get; set; }

        public int Registro { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataIni { get; set; } = DateTime.Now;

        public DateTime? DataFim { get; set; } = null;

        public bool Ativo { get; set; } = true;
    }
}