using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Matriculas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Dados
{
    public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
    {
        private readonly DatabaseContext _databaseContext;

        public MatriculaRepositorio(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Matricula>> ObterLista()
        {
            return await _databaseContext.Matriculas.ToListAsync();
        }

        public async Task<Matricula> ObterPorId(Guid id)
        {
            return await _databaseContext.Matriculas.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task Deletar(Matricula matricula)
        {
            _databaseContext.Matriculas.Remove(matricula);
            await _databaseContext.SaveChangesAsync();
        }
    }
}