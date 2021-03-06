using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Dtos.Matriculas;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Matriculas;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Matriculas
{
    public class MatriculaServiceTest
    {
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly Mock<IRepositorioBase<Matricula>> _repositorioBaseMock;
        private readonly MatriculaService _matriculaService;
        private readonly CreateMatriculaDto _createMatriculaDto;
        private readonly UpdateMatriculaDto _updateMatriculaDto;
        private readonly Aluno _aluno;
        private readonly Curso _curso;
        private readonly Faker _faker;

        public MatriculaServiceTest()
        {
            _faker = new Faker("pt_BR");
            _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _repositorioBaseMock = new Mock<IRepositorioBase<Matricula>>();

            var cursoId = _faker.Random.Guid();
            var alunoId = _faker.Random.Guid();

            _aluno = AlunoBuilder.Novo().ComId(alunoId).Build();
            _curso = CursoBuilder.Novo().ComId(cursoId).Build();

            _createMatriculaDto = new CreateMatriculaDto
            {
                AlunoId = _aluno.Id,
                CursoId = _curso.Id,
                ValorPago = _curso.Valor
            };

            _updateMatriculaDto = new UpdateMatriculaDto
            {
                ValorPago = _curso.Valor * 0.7m
            };

            _matriculaService = new MatriculaService(_matriculaRepositorioMock.Object, _cursoRepositorioMock.Object, _alunoRepositorioMock.Object, _repositorioBaseMock.Object);
        }

        [Fact]
        public async Task DeveAdicionarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).ComCurso(_curso).ComAluno(_aluno).ComValorPago(_curso.Valor).Build();

            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_curso);
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_aluno);
            _matriculaRepositorioMock.Setup(mr => mr.ObterLista()).ReturnsAsync(new List<Matricula>());
            _repositorioBaseMock.Setup(rb => rb.Adicionar(It.IsAny<Matricula>())).ReturnsAsync(matricula);

            var response = await _matriculaService.Adicionar(_createMatriculaDto);

            response.ToExpectedObject().ShouldMatch(new MatriculaDto(matricula));
        }

        [Fact]
        public async Task DeveLancarExcecaoQuandoAoTentarAdicionarMatriculaJaExistirMatriculaParaOAlunoNoCursoInformado()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).ComCurso(_curso).ComAluno(_aluno).ComValorPago(_curso.Valor).Build();

            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_curso);
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_aluno);
            _matriculaRepositorioMock.Setup(mr => mr.ObterLista()).ReturnsAsync(new List<Matricula>() { matricula });

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _matriculaService.Adicionar(_createMatriculaDto));
            error.ComMensagem(ErroMessage.ALUNO_JA_MATRICULADO_PARA_CURSO);
        }

        [Fact]
        public async Task DeveAlterarValorPagoDaMatricula()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).ComCurso(_curso).ComAluno(_aluno).ComValorPago(_curso.Valor).Build();
            _matriculaRepositorioMock.Setup(mr => mr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(matricula);

            var matriculaAtualizada = MatriculaBuilder.Novo().ComId(matricula.Id).ComCurso(_curso).ComAluno(_aluno).ComValorPago(_updateMatriculaDto.ValorPago).Build();

            _repositorioBaseMock.Setup(rb => rb.Atualizar(It.IsAny<Matricula>())).ReturnsAsync(matriculaAtualizada);

            var response = await _matriculaService.Atualizar(matricula.Id, _updateMatriculaDto);

            response.ToExpectedObject().ShouldMatch(new MatriculaDto(matriculaAtualizada));
        }

        [Fact]
        public async Task DeveBuscarMatriculaPorId()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).Build();

            _matriculaRepositorioMock.Setup(mr => mr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(matricula);

            var response = await _matriculaService.ObterPorId(matricula.Id);

            _matriculaRepositorioMock.Verify(r => r.ObterPorId(matricula.Id), Times.Once);
            response.ToExpectedObject().ShouldMatch(new MatriculaDto(matricula));
        }

        [Fact]
        public async Task DeveBuscarListaDeMatriculas()
        {
            var quantidadeDeMatriculas = _faker.Random.Int(0, 10);
            var matriculas = new List<Matricula>();
            for (int i = 0; i < quantidadeDeMatriculas; i++)
            {
                matriculas.Add(MatriculaBuilder.Novo().Build());
            }

            _matriculaRepositorioMock.Setup(mr => mr.ObterLista()).ReturnsAsync(matriculas);

            var response = await _matriculaService.ObterMatriculas();

            response.ToExpectedObject().ShouldMatch(matriculas.Select(it => new MatriculaDto(it)));
            _matriculaRepositorioMock.Verify(r => r.ObterLista(), Times.Once);
            Assert.Equal(response.Count, quantidadeDeMatriculas);
        }

        [Fact]
        public async Task DeveDeletarMatriculaComIdInformado()
        {
            var quantidadeDeMatriculas = _faker.Random.Int(1, 10);
            var matriculas = new List<Matricula>();
            for (int i = 0; i < quantidadeDeMatriculas; i++)
            {
                matriculas.Add(MatriculaBuilder.Novo().Build());
            }

            _matriculaRepositorioMock.Setup(mr => mr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(matriculas[0]);

            await _matriculaService.Deletar(matriculas[0].Id);

            _matriculaRepositorioMock.Verify(r => r.Deletar(matriculas[0]), Times.Once);
        }
    }
}