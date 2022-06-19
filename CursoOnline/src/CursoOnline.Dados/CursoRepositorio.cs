using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Cursos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Dados
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {
        private readonly DatabaseContext _databaseContext;

        public CursoRepositorio(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Curso> ObterPeloNome(string nome)
        {
            return await _databaseContext.Cursos.Where(c => c.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<Curso> ObterPorId(Guid id)
        {
            return await _databaseContext.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Curso>> ObterLista()
        {
            return await _databaseContext.Cursos.ToListAsync();
        }

        public async Task Deletar(Curso curso)
        {
            _databaseContext.Cursos.Remove(curso);
            await _databaseContext.SaveChangesAsync();
        }
    }
}