using CursoOnline.Application.Dtos.Cursos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface ICursoService
    {
        Task Adicionar(CreateCursoDto cursoDto);

        Task Atualizar(CursoDto cursoDto);

        Task<List<CursoDto>> ObterCursos();
    }
}