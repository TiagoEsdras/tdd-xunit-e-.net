using Bogus;
using CursoOnline.Dados;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Helpers;
using CursoOnline.Domain.Matriculas;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
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

            _matriculaService = new MatriculaService(_matriculaRepositorioMock.Object, _cursoRepositorioMock.Object, _alunoRepositorioMock.Object, _repositorioBaseMock.Object);
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

        [Fact]
        public async Task DeveBuscarMatriculaPorId()
        {
            var matricula = MatriculaBuilder.Novo().ComId(_faker.Random.Guid()).Build();

            _matriculaRepositorioMock.Setup(mr => mr.ObterPorId(It.IsAny<Guid>())).ReturnsAsync(matricula);

            var response = await _matriculaService.ObterPorId(matricula.Id);

            _matriculaRepositorioMock.Verify(r => r.ObterPorId(matricula.Id), Times.Once);
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

            var matriculaCriada = await _repositorioBase.Adicionar(new Matricula(aluno, curso, matriculaDto.ValorPago));
            return new MatriculaDto(matriculaCriada);
        }

        public async Task<MatriculaDto> ObterPorId(Guid id)
        {
            var matricula = await _matriculaRepositorio.ObterPorId(id);

            return new MatriculaDto(matricula);
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

    public interface IMatriculaRepositorio
    {
        Task<Matricula> ObterPorId(Guid id);
    }

    public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
    {
        private readonly DatabaseContext _databaseContext;
        public MatriculaRepositorio(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<Matricula> ObterPorId(Guid id)
        {
            return await _databaseContext.Matriculas.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}