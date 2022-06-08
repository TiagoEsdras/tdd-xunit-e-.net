using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Dados
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        private readonly DatabaseContext _databaseContext;

        public AlunoRepositorio(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Aluno> ObterPeloCPF(string cpf)
        {
            return await _databaseContext.Alunos.Where(a => a.CPF == cpf).FirstOrDefaultAsync();
        }

        public async Task<Aluno> ObterPorId(Guid id)
        {
            return await _databaseContext.Alunos.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Aluno>> ObterLista()
        {
            return await _databaseContext.Alunos.ToListAsync();
        }
    }
}