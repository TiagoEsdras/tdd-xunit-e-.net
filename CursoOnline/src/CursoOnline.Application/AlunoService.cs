using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Alunos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoOnline.Application
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepositorioBase<Aluno> _repositorioBase;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public AlunoService(IRepositorioBase<Aluno> repositorioBase, IAlunoRepositorio alunoRepositorio)
        {
            _repositorioBase = repositorioBase;
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task Adicionar(CreateAlunoDto createAlunoDto)
        {
            var alunoJaExiste = await _alunoRepositorio.ObterPeloCPF(createAlunoDto.CPF);
            if (alunoJaExiste is not null)
                throw new ArgumentException(ErroMessage.ALUNO_COM_CPF_JA_EXISTENTE);

            if (!Enum.TryParse<PublicoAlvoEnum>(createAlunoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            var aluno = new Aluno(createAlunoDto.Nome, createAlunoDto.CPF, createAlunoDto.Email, publicoAlvo);
            await _repositorioBase.Adicionar(aluno);
        }

        public async Task Atualizar(Guid id, UpdateAlunoDto updateAlunoDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(ErroMessage.ID_INVALIDO);

            var aluno = await _alunoRepositorio.ObterPorId(id);

            if (aluno is null)
                throw new ArgumentException(ErroMessage.ALUNO_NAO_EXISTENTE);

            aluno.AlterarNome(updateAlunoDto.Nome);

            await _repositorioBase.Atualizar(aluno);
        }

        public async Task<AlunoDto> ObterPorId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(ErroMessage.ID_INVALIDO);

            var aluno = await _alunoRepositorio.ObterPorId(id);

            if (aluno is null)
                throw new ArgumentException(ErroMessage.ALUNO_NAO_EXISTENTE);

            return new AlunoDto(aluno);
        }

        public async Task<List<AlunoDto>> ObterLista()
        {
            var alunos = await _alunoRepositorio.ObterLista();
            var listAlunos = new List<AlunoDto>();

            foreach (var aluno in alunos)
            {
                listAlunos.Add(new AlunoDto(aluno));
            }

            return listAlunos;
        }

        public async Task Deletar(Guid id)
        {
            var aluno = await _alunoRepositorio.ObterPorId(id);
            if (aluno is null)
                throw new ArgumentException(ErroMessage.ALUNO_NAO_EXISTENTE);
            await _alunoRepositorio.Deletar(aluno);
        }
    }
}