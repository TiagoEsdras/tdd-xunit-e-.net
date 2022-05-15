using CursoOnline.Domain.Cursos;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contratos
{
    public interface ICursoRepositorio
    {
        Task Adicionar(Curso curso);

        Task<Curso> ObterPeloNome(string nome);
    }
}