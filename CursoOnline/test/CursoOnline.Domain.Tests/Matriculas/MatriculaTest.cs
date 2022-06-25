using Bogus;
using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoOnline.Domain.Tests.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarUmaMatricula()
        {
            var aluno = AlunoBuilder.Novo().Build();
            var curso = CursoBuilder.Novo().Build();
            const decimal valorPago = 1000;

            var matriculaEsperada = new
            {
                Aluno = aluno,
                Curso = curso,
                ValorPago = valorPago
            };

            var matricula = new Matricula(aluno, curso, valorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Theory]
        [InlineData(null)]
        public void NaoDeveCriarMatriculaSemAluno(Aluno alunoInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
               MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build()
           ).ComMensagem(ErroMessage.ALUNO_INVALIDO);
        }

        [Theory]
        [InlineData(null)]
        public void NaoDeveCriarMatriculaSemCurso(Curso cursoInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
               MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build()
           ).ComMensagem(ErroMessage.CURSO_INVALIDO);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50.88)]
        public void NaoDeveCriarMatriculaComValorPagoInvalido(decimal valorPagoInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
               MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build()
           ).ComMensagem(ErroMessage.VALOR_PAGO_INVALIDO);
        }
    }

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
            _aluno = AlunoBuilder.Novo().Build();
            _curso = CursoBuilder.Novo().Build();
            _valorPago = faker.Random.Decimal(500, 1000);
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

        public Matricula Build()
        {
            var matricula = new Matricula(_aluno, _curso, _valorPago);
            return matricula;
        }
    }

    public class Matricula
    {
        public Matricula(Aluno aluno, Curso curso, decimal valorPago)
        {
            if (aluno is null)
                throw new ArgumentException(ErroMessage.ALUNO_INVALIDO);
            if (curso is null)
                throw new ArgumentException(ErroMessage.CURSO_INVALIDO);
            if (valorPago <= 0)
                throw new ArgumentException(ErroMessage.VALOR_PAGO_INVALIDO);

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
        }

        [Key]
        public Guid Id { get; set; }

        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public decimal ValorPago { get; set; }
    }
}