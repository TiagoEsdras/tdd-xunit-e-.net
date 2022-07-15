using CursoOnline.Domain.Matriculas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contratos
{
    public interface IMatriculaRepositorio
    {
        Task<Matricula> ObterPorId(Guid id);

        Task<List<Matricula>> ObterLista();

        Task Deletar(Matricula matricula);
    }
}