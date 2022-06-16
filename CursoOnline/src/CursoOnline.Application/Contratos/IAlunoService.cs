using CursoOnline.Application.Dtos.Alunos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface IAlunoService
    {
        Task<AlunoDto> Adicionar(CreateAlunoDto createAlunoDto);

        Task<AlunoDto> Atualizar(Guid id, UpdateAlunoDto alunoDto);

        Task<AlunoDto> ObterPorId(Guid id);

        Task<List<AlunoDto>> ObterLista();

        Task Deletar(Guid id);
    }
}