using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoTest
    {   
        /// <summary>
        /// Deve criar um curso com nome, carga horária, público alvo, e valor do curso;
        /// As opções para PublicoAlvo devem ser: "Estudante", "Universitário", "Empregado" e "Empreendedor";
        /// </summary>
        [Fact]
        public void DeveCriarUmCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Curso 01",
                Descricao = "Descricao do curso",
                CargaHoraria = 500,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 199.99m
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        /// <summary>
        /// Nome não pode ser invalido
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build()
            ).ComMensagem("Nome não pode ser nulo ou uma string vazia");
        }

        /// <summary>
        /// Descricao não pode ser invalida
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerDescricaoInvalida(string descricaoInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComDescricao(descricaoInvalida).Build()
            ).ComMensagem("Descrição não pode ser nula ou uma string vazia");
        }

        /// <summary>
        /// Carga horaria nao pode ser invalida
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void NaoDeveCursoTerCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                 CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()
             ).ComMensagem("Carga horária não pode ser menor ou igual a zero");
        }

        /// <summary>
        /// Valor do curso não pode ser invalido
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-50.88)]
        public void NaoDeveCursoTerValorInvalido(decimal valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build()
            ).ComMensagem("Valor do curso não pode ser menor ou igual a zero");
        }
    }

    public class Curso
    {
        public Curso(string nome, string descricao, int cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome não pode ser nulo ou uma string vazia");

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição não pode ser nula ou uma string vazia");

            if (cargaHoraria <= 0)
                throw new ArgumentException("Carga horária não pode ser menor ou igual a zero");

            if (valor <= 0)
                throw new ArgumentException("Valor do curso não pode ser menor ou igual a zero");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }
    }

    public enum PublicoAlvoEnum
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
}