using CursoOnline.Domain.Alunos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contratos
{
    public interface IAlunoRepositorio
    {
        Task<Aluno> ObterPeloCPF(string cpf);

        Task<Aluno> ObterPorId(Guid id);

        Task<List<Aluno>> ObterLista();
    }
}