using CursoOnline.Application.Dtos.Matriculas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface IMatriculaService
    {
        Task<MatriculaDto> Adicionar(CreateMatriculaDto matriculaDto);

        Task<MatriculaDto> ObterPorId(Guid id);

        Task<List<MatriculaDto>> ObterMatriculas();

        Task<MatriculaDto> Atualizar(Guid id, UpdateMatriculaDto updateMatriculaDto);
    }
}