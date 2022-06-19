using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Cursos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Application
{
    public class CursoService : ICursoService
    {
        private readonly IRepositorioBase<Curso> _repositorioBase;
        private readonly ICursoRepositorio _cursoRepositorio;

        public CursoService(IRepositorioBase<Curso> repositorioBase, ICursoRepositorio cursoRepositorio)
        {
            _repositorioBase = repositorioBase;
            _cursoRepositorio = cursoRepositorio;
        }

        public async Task<CursoDto> Adicionar(CreateCursoDto cursoDto)
        {
            var cursoJaSalvo = await _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException(ErroMessage.NOME_DO_CURSO_JA_EXISTENTE);

            if (!Enum.TryParse<PublicoAlvoEnum>(cursoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            var cursoCriado = await _repositorioBase.Adicionar(new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor));
            return new CursoDto(cursoCriado);
        }

        public async Task<CursoDto> Atualizar(Guid id, UpdateCursoDto cursoDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(ErroMessage.ID_INVALIDO);

            var curso = await _cursoRepositorio.ObterPorId(id);

            LancarExcecaoQuandoCursoEhNulo(curso);

            curso.AlterarNome(cursoDto.Nome);
            curso.AlterarDescricao(cursoDto.Descricao);
            curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            curso.AlterarValor(cursoDto.Valor);
            var cursoAtualizado = await _repositorioBase.Atualizar(curso);
            return new CursoDto(cursoAtualizado);
        }

        public async Task<List<CursoDto>> ObterCursos()
        {
            var cursos = await _cursoRepositorio.ObterLista();
            return cursos.Select(curso => new CursoDto(curso)).ToList();
        }

        public async Task<CursoDto> ObterPorId(Guid id)
        {
            ValidadorDeGuid.IsValid(id);
            var curso = await _cursoRepositorio.ObterPorId(id);
            LancarExcecaoQuandoCursoEhNulo(curso);
            return new CursoDto(curso);
        }

        public async Task Deletar(Guid id)
        {
            ValidadorDeGuid.IsValid(id);
            var curso = await _cursoRepositorio.ObterPorId(id);
            LancarExcecaoQuandoCursoEhNulo(curso);
            await _cursoRepositorio.Deletar(curso);
        }

        private static void LancarExcecaoQuandoCursoEhNulo(Curso curso)
        {
            if (curso is null)
                throw new ArgumentException(ErroMessage.CURSO_NAO_EXISTENTE);
        }
    }
}