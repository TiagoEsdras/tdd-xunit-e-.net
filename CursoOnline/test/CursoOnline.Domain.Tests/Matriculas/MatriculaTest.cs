﻿using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
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
}