using Bogus;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Int(50, 100),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Decimal(500, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicinarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(It.Is<Curso>(
                c => c.Nome == _cursoDto.Nome && c.Descricao == _cursoDto.Descricao)));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;
            
            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Público Alvo inválido");
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            Enum.TryParse(typeof(PublicoAlvoEnum), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Público Alvo inválido");

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, (PublicoAlvoEnum)publicoAlvo, cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}