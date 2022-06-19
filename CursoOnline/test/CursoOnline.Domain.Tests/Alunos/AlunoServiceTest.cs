using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Alunos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Moq;
using System;
using System.Collections.Generic;
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
        private readonly Mock<IConversorPublicoAlvo> _conversorPublicoAlvoMock;
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
            _conversorPublicoAlvoMock = new Mock<IConversorPublicoAlvo>();

            _alunoService = new AlunoService(_repositorioBaseMock.Object, _alunoRepositorioMock.Object, _conversorPublicoAlvoMock.Object);
        }

        [Fact]
        public async Task DeveAdicinarAluno()
        {
            var aluno = AlunoBuilder.Novo().ComId(_faker.Random.Guid()).Build();

            _repositorioBaseMock.Setup(rb => rb.Adicionar(It.IsAny<Aluno>())).ReturnsAsync(aluno);

            var response = await _alunoService.Adicionar(_createAlunoDto);

            response.ToExpectedObject().ShouldMatch(new AlunoDto(aluno));
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
        public async Task DeveAlterarNomeDoAluno()
        {
            var aluno = AlunoBuilder.Novo().ComId(_faker.Random.Guid()).Build();
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(aluno.Id)).ReturnsAsync(aluno);

            var alunoEditado = AlunoBuilder.Novo()
                .ComId(aluno.Id)
                .ComNome(_updateAlunoDto.Nome)
                .ComCPF(aluno.CPF)
                .Build();

            _repositorioBaseMock.Setup(rb => rb.Atualizar(It.IsAny<Aluno>())).ReturnsAsync(alunoEditado);

            var response = await _alunoService.Atualizar(aluno.Id, _updateAlunoDto);

            response.ToExpectedObject().ShouldMatch(new AlunoDto(alunoEditado));
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoTentarAtualizarAlunoENaoExistirRegistro()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Atualizar(_faker.Random.Guid(), _updateAlunoDto));

            error.ComMensagem(ErroMessage.ALUNO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoTentarAtualizarAlunoEIdPassadoEUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Atualizar(Guid.Empty, _updateAlunoDto));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }

        [Fact]
        public async Task DeveBuscarAlunoPorId()
        {
            var aluno = AlunoBuilder.Novo().ComId(_faker.Random.Guid()).Build();

            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(aluno.Id)).ReturnsAsync(aluno);

            var response = await _alunoService.ObterPorId(aluno.Id);

            _alunoRepositorioMock.Verify(r => r.ObterPorId(aluno.Id), Times.Once);
            response.ToExpectedObject().ShouldMatch(new AlunoDto(aluno));
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoBuscarAlunoPorIdForPassadoUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.ObterPorId(Guid.Empty));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoBuscarAlunoPorIdNaoExistirRegistro()
        {
            _alunoRepositorioMock.Setup(ar => ar.ObterPorId(_faker.Random.Guid())).ThrowsAsync(new ArgumentException(message: ErroMessage.ALUNO_NAO_EXISTENTE));

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.ObterPorId(_faker.Random.Guid()));

            error.ComMensagem(ErroMessage.ALUNO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveBuscarListaDeAlunos()
        {
            var quantidadeDeAlunos = _faker.Random.Int(0, 10);
            var alunos = new List<Aluno>();
            for (int i = 0; i < quantidadeDeAlunos; i++)
            {
                alunos.Add(AlunoBuilder.Novo().Build());
            }

            _alunoRepositorioMock.Setup(ar => ar.ObterLista()).ReturnsAsync(alunos);

            var response = await _alunoService.ObterLista();

            _alunoRepositorioMock.Verify(r => r.ObterLista(), Times.Once);
            Assert.Equal(response.Count, quantidadeDeAlunos);
        }

        [Fact]
        public async Task DeveDeletarAlunoComIdInformado()
        {
            var quantidadeDeAlunos = _faker.Random.Int(1, 10);
            var alunos = new List<Aluno>();
            for (int i = 0; i < quantidadeDeAlunos; i++)
            {
                alunos.Add(AlunoBuilder.Novo().Build());
            }

            _alunoRepositorioMock.Setup(rb => rb.ObterPorId(alunos[0].Id)).ReturnsAsync(alunos[0]);

            await _alunoService.Deletar(alunos[0].Id);

            _alunoRepositorioMock.Verify(r => r.Deletar(alunos[0]), Times.Once);
        }

        [Fact]
        public async Task DeveRetornarErroAoTentarDeletarAlunoQuandoIdDoAlunoNaoExiste()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Deletar(_faker.Random.Guid()));

            error.ComMensagem(ErroMessage.ALUNO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveRetornarErroAoTentarDeletarAlunoQuandoIdPassadoForPassadoUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _alunoService.Deletar(Guid.Empty));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }
    }
}