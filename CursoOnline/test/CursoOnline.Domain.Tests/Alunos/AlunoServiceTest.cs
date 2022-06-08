using Bogus;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Tests.Builders;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Alunos
{
    public class AlunoServiceTest
    {
        private readonly AlunoService _alunoService;
        private readonly CreateAlunoDto _createAlunoDto;
        private readonly UpdateAlunoDto _updateAlunoDto;
        private readonly Mock<IRepositorioBase<Aluno>> _repositorioBaseMock;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly Faker _faker;

        public AlunoServiceTest()
        {
            _faker = new Faker("pt_BR");
            _createAlunoDto = new CreateAlunoDto
            {
                Nome = _faker.Person.FullName,
                CPF = "34834537501",
                Email = _faker.Person.Email,
                PublicoAlvo = "Estudante"
            };

            _updateAlunoDto = new UpdateAlunoDto
            {
                Id = _faker.Random.Guid(),
                Nome = _faker.Person.FullName,
            };

            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _repositorioBaseMock = new Mock<IRepositorioBase<Aluno>>();

            _alunoService = new AlunoService(_repositorioBaseMock.Object, _alunoRepositorioMock.Object);
        }

        [Fact]
        public async Task DeveAdicinarAluno()
        {
            await _alunoService.Adicionar(_createAlunoDto);

            _repositorioBaseMock.Verify(r => r.Adicionar(It.Is<Aluno>(
                c => c.Nome == _createAlunoDto.Nome && c.CPF == _createAlunoDto.CPF && c.Email == _createAlunoDto.Email)));
        }

        [Fact]
        public async Task NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _createAlunoDto.PublicoAlvo = publicoAlvoInvalido;

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Adicionar(_createAlunoDto));

            error.ComMensagem(ErroMessage.PUBLICO_ALVO_INVALIDO);
        }

        [Fact]
        public async Task DeveAlterarNomeDoAluno()
        {
            var aluno = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(_updateAlunoDto.Id)).ReturnsAsync(aluno);

            await _alunoService.Atualizar(_updateAlunoDto);

            Assert.Equal(_updateAlunoDto.Nome, aluno.Nome);
        }
    }

    public interface IAlunoRepositorio
    {
        Task<Aluno> ObterPorId(Guid id);
    }

    public class CreateAlunoDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string PublicoAlvo { get; set; }
    }

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
            if (!Enum.TryParse<PublicoAlvoEnum>(createAlunoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            var aluno = new Aluno(createAlunoDto.Nome, createAlunoDto.CPF, createAlunoDto.Email, publicoAlvo);
            await _repositorioBase.Adicionar(aluno);
        }

        public async Task Atualizar(UpdateAlunoDto updateAlunoDto)
        {
            var aluno = await _alunoRepositorio.ObterPorId(updateAlunoDto.Id);
            aluno.AlterarNome(updateAlunoDto.Nome);
            await _repositorioBase.Atualizar(aluno);
        }
    }

    public interface IAlunoService
    {
        Task Adicionar(CreateAlunoDto createAlunoDto);

        Task Atualizar(UpdateAlunoDto alunoDto);
    }

    public class UpdateAlunoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}