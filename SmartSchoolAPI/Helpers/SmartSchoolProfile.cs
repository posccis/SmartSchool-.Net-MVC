using AutoMapper;
using SmartSchoolAPI.Dtos;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            //ALUNO Dtos map -->
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom( src => src.DataNasc.GetCurrentAge())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
            CreateMap<Aluno, AlunoPatchDto>().ReverseMap();

            // <--
            // PROFESSOR Dtos map--> 
            CreateMap<Professor, ProfessorDto>()
                        .ForMember(
                            dest => dest.Nome,
                            opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                        );
            CreateMap<Professor, ProfessorRegistrarDtos>().ReverseMap();

            CreateMap<DisciplinasDto, Disciplina>().ReverseMap();
            CreateMap<CursoDto, Curso>().ReverseMap();

            // <--
        }
    }
}