using Bogus;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;

namespace CursoOnline.Domain.Tests.Builders
{
    public class CursoBuilder
    {
        private string _nome;
        private string _descricao;
        private int _cargaHoraria;
        private PublicoAlvoEnum _publicoAlvo;
        private decimal _valor;

        public CursoBuilder()
        {
            var faker = new Faker();
            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Int(50, 100);
            _publicoAlvo = PublicoAlvoEnum.Estudante;
            _valor = faker.Random.Decimal(500, 1000);
        }

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(int cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }
    }
}