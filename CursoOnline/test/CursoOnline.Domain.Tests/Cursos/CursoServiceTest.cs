using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Contratos;
using CursoOnline.Application.Dtos.Cursos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoServiceTest
    {
        private readonly CreateCursoDto _createCursoDto;
        private readonly UpdateCursoDto _updateCursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IRepositorioBase<Curso>> _repositorioBaseMock;
        private readonly Mock<IConversorPublicoAlvo> _conversorPublicoAlvoMock;
        private readonly CursoService _cursoService;
        private readonly Faker _faker;

        public CursoServiceTest()
        {
            _faker = new Faker("pt_BR");

            _createCursoDto = new CreateCursoDto
            {
                Nome = _faker.Random.Word(),
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Int(50, 100),
                PublicoAlvo = "Estudante",
                Valor = _faker.Random.Decimal(500, 1000)
            };

            _updateCursoDto = new UpdateCursoDto
            {
                Nome = _faker.Random.Word(),
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Int(50, 100),
                Valor = _faker.Random.Decimal(500, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _repositorioBaseMock = new Mock<IRepositorioBase<Curso>>();
            _conversorPublicoAlvoMock = new Mock<IConversorPublicoAlvo>();

            _cursoService = new CursoService(_repositorioBaseMock.Object, _cursoRepositorioMock.Object, _conversorPublicoAlvoMock.Object);
        }

        [Fact]
        public async Task DeveAdicinarCurso()
        {
            var curso = CursoBuilder.Novo().ComId(_faker.Random.Guid()).Build();
            _repositorioBaseMock.Setup(rb => rb.Adicionar(It.IsAny<Curso>())).ReturnsAsync(curso);

            var response = await _cursoService.Adicionar(_createCursoDto);

            response.ToExpectedObject().ShouldMatch(new CursoDto(curso));
        }

        [Fact]
        public async Task NaoDeveAdicionarCursoComMesmoNomeDeOutroCursoJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_createCursoDto.Nome).Build();

            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_createCursoDto.Nome)).ReturnsAsync(cursoJaSalvo);

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Adicionar(_createCursoDto));

            error.ComMensagem(ErroMessage.NOME_DO_CURSO_JA_EXISTENTE);
        }

        [Fact]
        public async Task DeveAlterarDadosDoCurso()
        {
            var curso = CursoBuilder.Novo().ComId(_faker.Random.Guid()).Build();
            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(curso.Id)).ReturnsAsync(curso);

            var cursoAtualizado = CursoBuilder.Novo()
                .ComId(curso.Id)
                .ComNome(_updateCursoDto.Nome)
                .ComDescricao(_updateCursoDto.Descricao)
                .ComCargaHoraria(_updateCursoDto.CargaHoraria)
                .ComValor(_updateCursoDto.Valor)
                .Build();

            _repositorioBaseMock.Setup(rb => rb.Atualizar(It.IsAny<Curso>())).ReturnsAsync(cursoAtualizado);

            var response = await _cursoService.Atualizar(curso.Id, _updateCursoDto);

            response.ToExpectedObject().ShouldMatch(new CursoDto(cursoAtualizado));
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoTentarAtualizarCursoENaoExistirRegistro()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Atualizar(_faker.Random.Guid(), _updateCursoDto));

            error.ComMensagem(ErroMessage.CURSO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoTentarAtualizarCursoEIdPassadoEUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Atualizar(Guid.Empty, _updateCursoDto));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }

        [Fact]
        public async Task DeveBuscarCursoPorId()
        {
            var curso = CursoBuilder.Novo().ComId(_faker.Random.Guid()).Build();

            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(curso.Id)).ReturnsAsync(curso);

            var response = await _cursoService.ObterPorId(curso.Id);

            _cursoRepositorioMock.Verify(r => r.ObterPorId(curso.Id), Times.Once);
            response.ToExpectedObject().ShouldMatch(new CursoDto(curso));
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoBuscarCursoPorIdForPassadoUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.ObterPorId(Guid.Empty));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }

        [Fact]
        public async Task DeveRetornarErroQuandoAoBuscarCursoPorIdNaoExistirRegistro()
        {
            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(_faker.Random.Guid())).ThrowsAsync(new ArgumentException(message: ErroMessage.CURSO_NAO_EXISTENTE));

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.ObterPorId(_faker.Random.Guid()));

            error.ComMensagem(ErroMessage.CURSO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveBuscarListaDeCursos()
        {
            var quantidadeDeCursos = _faker.Random.Int(0, 10);
            var cursos = new List<Curso>();
            for (int i = 0; i < quantidadeDeCursos; i++)
            {
                cursos.Add(CursoBuilder.Novo().Build());
            }

            _cursoRepositorioMock.Setup(cr => cr.ObterLista()).ReturnsAsync(cursos);

            var response = await _cursoService.ObterCursos();

            _cursoRepositorioMock.Verify(r => r.ObterLista(), Times.Once);
            Assert.Equal(response.Count, quantidadeDeCursos);
        }

        [Fact]
        public async Task DeveDeletarCursoComIdInformado()
        {
            var quantidadeDeCursos = _faker.Random.Int(1, 10);
            var cursos = new List<Curso>();
            for (int i = 0; i < quantidadeDeCursos; i++)
            {
                cursos.Add(CursoBuilder.Novo().Build());
            }

            _cursoRepositorioMock.Setup(rb => rb.ObterPorId(cursos[0].Id)).ReturnsAsync(cursos[0]);

            await _cursoService.Deletar(cursos[0].Id);

            _cursoRepositorioMock.Verify(r => r.Deletar(cursos[0]), Times.Once);
        }

        [Fact]
        public async Task DeveRetornarErroAoTentarDeletarCursoQuandoIdDoCursoNaoExiste()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Deletar(_faker.Random.Guid()));

            error.ComMensagem(ErroMessage.CURSO_NAO_EXISTENTE);
        }

        [Fact]
        public async Task DeveRetornarErroAoTentarDeletarCursoQuandoIdPassadoForPassadoUmEmptyGuid()
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Deletar(Guid.Empty));

            error.ComMensagem(ErroMessage.ID_INVALIDO);
        }
    }
}