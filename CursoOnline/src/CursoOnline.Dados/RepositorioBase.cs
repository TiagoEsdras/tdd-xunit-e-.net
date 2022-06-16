using CursoOnline.Dados.Contratos;
using System.Threading.Tasks;

namespace CursoOnline.Dados
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;

        public RepositorioBase(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> Adicionar(T entity)
        {
            var alunoCriado = await _databaseContext.Set<T>().AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
            return alunoCriado.Entity;
        }

        public async Task<T> Atualizar(T entity)
        {
            var alunoAtualizado = _databaseContext.Set<T>().Update(entity);
            await _databaseContext.SaveChangesAsync();
            return alunoAtualizado.Entity;
        }
    }
}