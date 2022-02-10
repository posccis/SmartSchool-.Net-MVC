using System.Threading.Tasks;
using SmartSchoolAPI.Helpers;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;

         void Update<T>(T entity) where T : class;

         void Delete<T>(T entity) where T : class;

         bool SaveChanges();

         // ALUNO -->
        Task<PageList<Aluno>> GetAlunos(PageParams pageParams, bool includeDisciplina = false);
        public Aluno[] GetAlunosByDisciplinaId(int disciplina_Id, bool includeDisciplina);
        public Aluno GetAlunoById(int Id, bool includeDisciplina = false);
         // <--

         // Professores -->
        public Professor[] GetProfessor(bool includeAluno = false);
        public Professor[] GetProfessoresByDisciplinaId(int Id, bool includeAluno = false);
        public Professor GetProfessorById(int Id, bool includeAluno = false);
        public Professor[] GetProfessorByAlunoId(int alunoId, bool includeAluno = false);
         // <--

    }
}