using Bogus;
using CursoOnline.Application;
using CursoOnline.Application.Dtos;
using CursoOnline.Dados.Contratos;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Tests.Builders;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoServiceTest
    {
        private readonly CreateCursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IRepositorioBase<Curso>> _repositorioBaseMock;
        private readonly CursoService _cursoService;


        public CursoServiceTest()
        {
            var faker = new Faker();

            _cursoDto = new CreateCursoDto
            {                
                Nome = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Int(50, 100),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Decimal(500, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _repositorioBaseMock = new Mock<IRepositorioBase<Curso>>();

            _cursoService = new CursoService(_repositorioBaseMock.Object, _cursoRepositorioMock.Object);
        }

        [Fact]
        public async Task DeveAdicinarCurso()
        {
            await _cursoService.Adicionar(_cursoDto);

            _repositorioBaseMock.Verify(r => r.Adicionar(It.Is<Curso>(
                c => c.Nome == _cursoDto.Nome && c.Descricao == _cursoDto.Descricao)));
        }

        [Fact]
        public async Task NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;
          
            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Adicionar(_cursoDto));

            error.ComMensagem("Público Alvo inválido");
        }

        [Fact]
        public async void NaoDeveAdicionarCursoComMesmoNomeDeOutroCursoJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();

            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).ReturnsAsync(cursoJaSalvo);

            var error = await Assert.ThrowsAsync<ArgumentException>(() => _cursoService.Adicionar(_cursoDto));

            error.ComMensagem("Nome do curso já consta no banco de dados");
        }
    }
}