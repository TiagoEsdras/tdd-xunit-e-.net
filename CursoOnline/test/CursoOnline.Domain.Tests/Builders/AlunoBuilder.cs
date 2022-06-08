using Bogus;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Enums;

namespace CursoOnline.Domain.Tests.Builders
{
    public class AlunoBuilder
    {
        private string _nome;
        private string _cpf;
        private string _email;
        private PublicoAlvoEnum _publicoAlvo;
        private readonly Faker _faker;

        public AlunoBuilder()
        {
            _faker = new Faker();
            _nome = _faker.Person.FullName;
            _cpf = "83760151760";
            _email = _faker.Person.Email;
            _publicoAlvo = PublicoAlvoEnum.Estudante;
        }

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public Aluno Build()
        {
            return new Aluno(_nome, _cpf, _email, _publicoAlvo);
        }
    }
}