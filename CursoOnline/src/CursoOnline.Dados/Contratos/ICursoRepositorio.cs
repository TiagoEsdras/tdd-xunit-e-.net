using CursoOnline.Domain.Cursos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contratos
{
    public interface ICursoRepositorio
    {
        Task<Curso> ObterPeloNome(string nome);

        Task<List<Curso>> ObterLista();
    }
}