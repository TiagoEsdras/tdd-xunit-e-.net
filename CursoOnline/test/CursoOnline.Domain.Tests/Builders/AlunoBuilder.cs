using Bogus;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Enums;
using System;

namespace CursoOnline.Domain.Tests.Builders
{
    public class AlunoBuilder
    {
        private Guid _id;
        private string _nome;
        private string _cpf;
        private string _email;
        private PublicoAlvoEnum _publicoAlvo;
        private readonly Faker _faker;

        public AlunoBuilder()
        {
            _faker = new Faker();
            _id = _faker.Random.Guid();
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

        public AlunoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public AlunoBuilder ComCPF(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _cpf, _email, _publicoAlvo);

            if (_id == Guid.Empty) return aluno;
            var propertyInfo = aluno.GetType().GetProperty("Id");
            propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);

            return aluno;
        }
    }
}