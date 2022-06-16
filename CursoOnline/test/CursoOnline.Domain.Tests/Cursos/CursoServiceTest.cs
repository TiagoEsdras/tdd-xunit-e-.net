using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Dtos.Cursos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoServiceTest
    {
        private readonly CreateCursoDto _createCursoDto;
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IRepositorioBase<Curso>> _repositorioBaseMock;
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

            _cursoDto = new CursoDto
            {
                Id = _faker.Random.Guid(),
                Nome = _faker.Random.Word(),
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Int(50, 100),
                PublicoAlvo = "Estudante",
                Valor = _faker.Random.Decimal(500, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _repositorioBaseMock = new Mock<IRepositorioBase<Curso>>();

            _cursoService = new CursoService(_repositorioBaseMock.Object, _cursoRepositorioMock.Object);
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
        public async Task NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _createCursoDto.PublicoAlvo = publicoAlvoInvalido;

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Adicionar(_createCursoDto));

            error.ComMensagem(ErroMessage.PUBLICO_ALVO_INVALIDO);
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
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(cr => cr.ObterPorId(_cursoDto.Id)).ReturnsAsync(curso);

            await _cursoService.Atualizar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
            Assert.Equal(_cursoDto.Descricao, curso.Descricao);
        }
    }
}