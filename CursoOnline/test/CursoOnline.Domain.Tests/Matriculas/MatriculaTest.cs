using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Matriculas;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using System;
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
            var valorPago = curso.Valor;

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

        [Theory]
        [InlineData(100, 200)]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso(decimal valorDoCurso, decimal valorPago)
        {
            var curso = CursoBuilder.Novo().ComValor(valorDoCurso).Build();

            Assert.Throws<ArgumentException>(() =>
               MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPago).Build()
           ).ComMensagem(ErroMessage.VALOR_PAGO_MAIOR_QUE_VALOR_DO_CURSO);
        }

        [Theory]
        [InlineData(200, 100)]
        public void DeveIndicarQUeHouveDescontoNaMatricula(decimal valorDoCurso, decimal valorPago)
        {
            var curso = CursoBuilder.Novo().ComValor(valorDoCurso).Build();
            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPago).Build();

            Assert.True(matricula.ExisteDesconto);
        }

        [Theory]
        [InlineData(PublicoAlvoEnum.Empreendedor, PublicoAlvoEnum.Empregado)]
        public void NaoDeveAdicionarMatriculaCasoPublicoAlvoDoCursoEAlunoNaoForemIguais(PublicoAlvoEnum cursoPublicoAlvo, PublicoAlvoEnum alunoPublicoAlvo)
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(cursoPublicoAlvo).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(alunoPublicoAlvo).Build();

            Assert.Throws<ArgumentException>(() =>
               MatriculaBuilder.Novo().ComCurso(curso).ComAluno(aluno).ComValorPago(curso.Valor).Build()
           ).ComMensagem(ErroMessage.PUBLICO_ALVO_DE_CURSO_E_ALUNO_DIFERENTES);
        }
    }
}