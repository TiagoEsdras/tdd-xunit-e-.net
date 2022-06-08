using CursoOnline.Application.Dtos.Alunos;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface IAlunoService
    {
        Task Adicionar(CreateAlunoDto createAlunoDto);

        Task Atualizar(UpdateAlunoDto alunoDto);
    }
}