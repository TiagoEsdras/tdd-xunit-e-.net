using Bogus;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Matriculas;
using System;

namespace CursoOnline.Domain.Tests.Builders
{
    public class MatriculaBuilder
    {
        private Guid _id;
        private Aluno _aluno;
        private Curso _curso;
        private decimal _valorPago;

        public MatriculaBuilder()
        {
            var faker = new Faker("pt_BR");
            _id = faker.Random.Guid();
            _aluno = AlunoBuilder.Novo().ComId(faker.Random.Guid()).Build();
            _curso = CursoBuilder.Novo().ComId(faker.Random.Guid()).Build();
            _valorPago = _curso.Valor;
        }

        public static MatriculaBuilder Novo()
        {
            return new MatriculaBuilder();
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            _aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            _curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(decimal valorPago)
        {
            _valorPago = valorPago;
            return this;
        }

        public MatriculaBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        public Matricula Build()
        {
            var matricula = new Matricula(_aluno, _curso, _valorPago);
            if (_id != Guid.Empty)
            {
                var propertyInfo = matricula.GetType().GetProperty("Id");
                propertyInfo.SetValue(matricula, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }
            return matricula;
        }
    }
}