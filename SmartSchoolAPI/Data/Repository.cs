using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Helpers;
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
        public async Task<PageList<Aluno>> GetAlunos(PageParams pageParams, bool includeDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunoDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if(!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                    .ToUpper()
                                                    .Contains(pageParams.Nome.ToUpper()) ||
                                            aluno.Sobrenome
                                                    .ToUpper()
                                                    .Contains(pageParams.Nome.ToUpper())       
                );

            if (pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

            if(pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0 ));

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
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
        
        public Professor[] GetProfessorByAlunoId(int alunoId, bool includeAluno = false)
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
                        .Where(pr => pr.Disciplinas.Any( d => d.AlunoDisciplinas.Any(ad => ad.AlunoId == alunoId)));
            
            return query.ToArray();
        }
        // <--
    }
}