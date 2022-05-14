using CursoOnline.Domain.Tests.Cursos;

namespace CursoOnline.Domain.Tests.Builders
{
    public class CursoBuilder
    {
        private string _nome = "Nome do curso";
        private string _descricao = "Descrição do curso";
        private int _cargaHoraria = 80;
        private PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;
        private decimal _valor = 950;

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