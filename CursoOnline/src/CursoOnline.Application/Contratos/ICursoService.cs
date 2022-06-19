using CursoOnline.Application.Dtos.Cursos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application.Contratos
{
    public interface ICursoService
    {
        Task<CursoDto> Adicionar(CreateCursoDto cursoDto);

        Task<CursoDto> Atualizar(Guid id, UpdateCursoDto cursoDto);

        Task<List<CursoDto>> ObterCursos();

        Task<CursoDto> ObterPorId(Guid id);

        Task Deletar(Guid id);
    }
}