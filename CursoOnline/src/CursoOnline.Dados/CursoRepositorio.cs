using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Cursos;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Curso>> ObterLista()
        {
            return await _databaseContext.Cursos.ToListAsync();
        }
    }
}