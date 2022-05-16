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

        public async Task Adicionar(T entity)
        {
            await _databaseContext.Set<T>().AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }
    }
}