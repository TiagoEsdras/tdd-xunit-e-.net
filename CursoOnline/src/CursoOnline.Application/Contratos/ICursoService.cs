using CursoOnline.Application.Dtos;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface ICursoService
    {
        Task Adicionar(CreateCursoDto cursoDto);
    }
}