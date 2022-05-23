using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoTest
    {   
        /// <summary>
        /// Deve criar um curso com nome, descrição, carga horária, público alvo, e valor do curso;
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
            ).ComMensagem(ErroMessage.NOME_INVALIDO);
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
            ).ComMensagem(ErroMessage.DESCRICAO_INVALIDA);
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
             ).ComMensagem(ErroMessage.CARGA_HORARIA_INVALIDA);
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
            ).ComMensagem(ErroMessage.VALOR_INVALIDO);
        }

        /// <summary>
        /// Nome do curso deve ser alterado
        /// </summary>
        [Theory]
        [InlineData("José")]
        public void DeveAlterarNomeDoCUrso(string nomeEsperado)
        {
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        /// <summary>
        /// Não pode alterar nome do curso quando nome for invalido
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarCursoComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => curso.AlterarNome(nomeInvalido)).ComMensagem(ErroMessage.NOME_INVALIDO);
        }

        /// <summary>
        /// Descricao do curso deve ser alterada
        /// </summary>
        [Theory]
        [InlineData("Descrição válida")]
        public void DeveAlterarDescricaoDoCurso(string descricaoEsperada)
        {
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarDescricao(descricaoEsperada);

            Assert.Equal(descricaoEsperada, curso.Descricao);
        }

        /// <summary>
        /// Não pode alterar descrição do curso quando descrição for invalida
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarCursoComDescricaoInvalida(string descricaoInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => curso.AlterarDescricao(descricaoInvalida)).ComMensagem(ErroMessage.DESCRICAO_INVALIDA);
        }

        /// <summary>
        /// Carga horaria do curso deve ser alterada
        /// </summary>
        [Theory]
        [InlineData(80)]
        public void DeveAlterarCargaHorariaDoCurso(int cargaHorariaEsperada)
        {
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        /// <summary>
        /// Não pode alterar carga horária do curso quando carga horaria for invalida
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void NaoDeveAlterarCursoComCargaHorariaInvalido(int cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida)).ComMensagem(ErroMessage.CARGA_HORARIA_INVALIDA);
        }

        /// <summary>
        /// Valor do curso deve ser alterado
        /// </summary>
        [Theory]
        [InlineData(80.79)]
        public void DeveAlterarValorDoCurso(decimal valorEsperado)
        {
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        /// <summary>
        /// Não pode alterar carga horária do curso quando carga horaria for invalida
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void NaoDeveAlterarCursoComValorInvalido(decimal valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ArgumentException>(() => curso.AlterarValor(valorInvalido)).ComMensagem(ErroMessage.VALOR_INVALIDO);
        }
    }
}