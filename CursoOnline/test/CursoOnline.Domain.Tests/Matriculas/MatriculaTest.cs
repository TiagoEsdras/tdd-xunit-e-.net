using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
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
    }

    public class Matricula
    {
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public decimal ValorPago { get; set; }

        public Matricula(Aluno aluno, Curso curso, decimal valorPago)
        {
            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
        }
    }
}