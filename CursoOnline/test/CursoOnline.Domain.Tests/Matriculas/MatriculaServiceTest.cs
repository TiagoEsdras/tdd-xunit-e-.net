using Bogus;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Matriculas;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Matriculas
{
    public class MatriculaServiceTest
    {
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly Mock<IRepositorioBase<Matricula>> _repositorioBaseMock;
        private readonly MatriculaService _matriculaService;
        private readonly CreateMatriculaDto _createMatriculaDto;
        private readonly Aluno _aluno;
        private readonly Curso _curso;
        private readonly Faker _faker;

        public MatriculaServiceTest()
        {
            _faker = new Faker("pt_BR");
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

            _matriculaService = new MatriculaService(_cursoRepositorioMock.Object, _alunoRepositorioMock.Object, _repositorioBaseMock.Object);
        }

        [Fact]
        public async Task DeveAdicionarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).ComCurso(_curso).ComAluno(_aluno).ComValorPago(_curso.Valor).Build();

            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_curso);
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(_aluno);
            _repositorioBaseMock.Setup(rb => rb.Adicionar(It.IsAny<Matricula>())).ReturnsAsync(matricula);

            var response = await _matriculaService.Adicionar(_createMatriculaDto);

            response.ToExpectedObject().ShouldMatch(new MatriculaDto(matricula));
        }
    }

    public class CreateMatriculaDto
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal ValorPago { get; set; }
    }

    public class MatriculaService : IMatriculaService
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IRepositorioBase<Matricula> _repositorioBase;

        public MatriculaService(ICursoRepositorio cursoRepositorio, IAlunoRepositorio alunoRepositorio, IRepositorioBase<Matricula> repositorioBase)
        {
            _cursoRepositorio = cursoRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _repositorioBase = repositorioBase;
        }

        public async Task<MatriculaDto> Adicionar(CreateMatriculaDto matriculaDto)
        {
            var aluno = await _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);
            var curso = await _cursoRepositorio.ObterPorId(matriculaDto.CursoId);

            var matriculaCriada = await _repositorioBase.Adicionar(new Matricula(aluno, curso, matriculaDto.ValorPago));
            return new MatriculaDto(matriculaCriada);
        }
    }

    public interface IMatriculaService
    {
        Task<MatriculaDto> Adicionar(CreateMatriculaDto matriculaDto);
    }

    public class MatriculaDto
    {
        public MatriculaDto(Matricula matricula)
        {
            Id = matricula.Id;
            AlunoId = matricula.Aluno.Id;
            CursoId = matricula.Curso.Id;
            ValorPago = matricula.ValorPago;
            ExisteDesconto = matricula.ExisteDesconto;
        }

        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal ValorPago { get; set; }
        public bool ExisteDesconto { get; }
    }
}