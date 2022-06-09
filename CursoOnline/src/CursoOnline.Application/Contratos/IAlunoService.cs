using CursoOnline.Application.Dtos.Alunos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface IAlunoService
    {
        Task Adicionar(CreateAlunoDto createAlunoDto);

        Task Atualizar(Guid id, UpdateAlunoDto alunoDto);

        Task<AlunoDto> ObterPorId(Guid id);

        Task<List<AlunoDto>> ObterLista();

        Task Deletar(Guid id);
    }
}