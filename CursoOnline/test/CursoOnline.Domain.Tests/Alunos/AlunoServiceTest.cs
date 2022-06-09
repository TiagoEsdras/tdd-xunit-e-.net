using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Dtos.Alunos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
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
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(aluno.Id)).ReturnsAsync(aluno);

            await _alunoService.Atualizar(aluno.Id, _updateAlunoDto);

            Assert.Equal(_updateAlunoDto.Nome, aluno.Nome);
        }

        [Fact]
        public async Task NaoDeveAdicionarAlunoComMesmoCPFDeOutroAlunoJaSalvo()
        {
            var alunoJaSalvo = AlunoBuilder.Novo().ComCPF(_createAlunoDto.CPF).Build();

            _alunoRepositorioMock.Setup(ar => ar.ObterPeloCPF(_createAlunoDto.CPF)).ReturnsAsync(alunoJaSalvo);

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Adicionar(_createAlunoDto));

            error.ComMensagem(ErroMessage.ALUNO_COM_CPF_JA_EXISTENTE);
        }

        [Fact]
        public async Task DeveBuscarAlunoPorId()
        {
            var aluno = AlunoBuilder.Novo().Build();

            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(aluno.Id)).ReturnsAsync(aluno);

            var response = await _alunoService.ObterPorId(aluno.Id);

            response.ToExpectedObject().ShouldMatch(new AlunoDto(aluno));
        }
    }
}