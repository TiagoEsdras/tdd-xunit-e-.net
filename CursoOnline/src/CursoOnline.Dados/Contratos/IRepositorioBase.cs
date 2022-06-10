using System.Threading.Tasks;

namespace CursoOnline.Dados.Contratos
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> Adicionar(T entity);

        Task Atualizar(T entity);
    }
}