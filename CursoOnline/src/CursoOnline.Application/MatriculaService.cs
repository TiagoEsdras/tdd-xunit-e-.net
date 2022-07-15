using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Matriculas;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Matriculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Application
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IRepositorioBase<Matricula> _repositorioBase;

        public MatriculaService(IMatriculaRepositorio matriculaRepositorio, ICursoRepositorio cursoRepositorio, IAlunoRepositorio alunoRepositorio, IRepositorioBase<Matricula> repositorioBase)
        {
            _matriculaRepositorio = matriculaRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _repositorioBase = repositorioBase;
        }

        public async Task<MatriculaDto> Adicionar(CreateMatriculaDto matriculaDto)
        {
            var aluno = await _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);
            var curso = await _cursoRepositorio.ObterPorId(matriculaDto.CursoId);

            var matriculas = await _matriculaRepositorio.ObterLista();

            if (matriculas.Any(m => m.Aluno.Id == matriculaDto.AlunoId && m.Curso.Id == matriculaDto.CursoId))
                throw new ArgumentException(ErroMessage.ALUNO_JA_MATRICULADO_PARA_CURSO);

            var matriculaCriada = await _repositorioBase.Adicionar(new Matricula(aluno, curso, matriculaDto.ValorPago));
            return new MatriculaDto(matriculaCriada);
        }

        public async Task<MatriculaDto> ObterPorId(Guid id)
        {
            var matricula = await _matriculaRepositorio.ObterPorId(id);

            return new MatriculaDto(matricula);
        }

        public async Task<List<MatriculaDto>> ObterMatriculas()
        {
            var matriculas = await _matriculaRepositorio.ObterLista();

            return matriculas.Select(matricula => new MatriculaDto(matricula)).ToList();
        }

        public async Task<MatriculaDto> Atualizar(Guid id, UpdateMatriculaDto updateMatriculaDto)
        {
            var matricula = await _matriculaRepositorio.ObterPorId(id);

            matricula.AlterarValorPago(updateMatriculaDto.ValorPago);

            var matriculaAtualizada = await _repositorioBase.Atualizar(matricula);
            return new MatriculaDto(matriculaAtualizada);
        }

        public async Task Deletar(Guid id)
        {
            var matricula = await _matriculaRepositorio.ObterPorId(id);
            await _matriculaRepositorio.Deletar(matricula);
        }
    }
}