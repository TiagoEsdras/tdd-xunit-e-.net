using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;
using System;
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

        public async Task Adicionar(CreateCursoDto cursoDto)
        {
            var cursoJaSalvo = await _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException(ErroMessage.NOME_DO_CURSO_JA_EXISTENTE);

            if (!Enum.TryParse<PublicoAlvoEnum>(cursoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor);
            await _repositorioBase.Adicionar(curso);
        }
    }
}