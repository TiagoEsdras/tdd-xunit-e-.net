using CursoOnline.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface ICursoService
    {
        Task Adicionar(CreateCursoDto cursoDto);
        Task<List<CursoDto>> ObterCursos();
    }
}