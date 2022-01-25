using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);

        }

        // Alunos -->
        public Aluno[] GetAlunos(bool includeDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAlunosByDisciplinaId(int disciplina_Id, bool includeDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(aluno => aluno.AlunoDisciplinas.Any(ad => ad.DisciplinaId == disciplina_Id));

            return query.ToArray();;
        }

        public Aluno GetAlunoById(int Id, bool includeDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(aluno => aluno.Id == Id);

            return query.FirstOrDefault();
        }
        // <--

        // Professores -->
        public Professor[] GetProfessor(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(a => a.Disciplinas)
                                    .ThenInclude(ad => ad.AlunoDisciplinas)
                                    .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Professor[] GetProfessoresByDisciplinaId(int Id, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(a => a.Disciplinas)
                                    .ThenInclude(ad => ad.AlunoDisciplinas)
                                    .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(pr => pr.Disciplinas.Any(d => d.Id == Id));

            return query.ToArray();
        }

        public Professor GetProfessorById(int Id, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(a => a.Disciplinas)
                            .ThenInclude(d => d.AlunoDisciplinas)
                            .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(pr => pr.Id == Id);
            
            return query.FirstOrDefault();
        }
        // <--
    }
}