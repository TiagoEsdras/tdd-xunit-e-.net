using Bogus;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
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
        private readonly Mock<IRepositorioBase<Aluno>> _repositorioBaseMock;
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
            _repositorioBaseMock = new Mock<IRepositorioBase<Aluno>>();

            _alunoService = new AlunoService(_repositorioBaseMock.Object);
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
        private IRepositorioBase<Aluno> _repositorioBase;

        public AlunoService(IRepositorioBase<Aluno> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task Adicionar(CreateAlunoDto createAlunoDto)
        {
            if (!Enum.TryParse<PublicoAlvoEnum>(createAlunoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            var aluno = new Aluno(createAlunoDto.Nome, createAlunoDto.CPF, createAlunoDto.Email, publicoAlvo);
            await _repositorioBase.Adicionar(aluno);
        }
    }

    public interface IAlunoService
    {
        Task Adicionar(CreateAlunoDto createAlunoDto);
    }
}