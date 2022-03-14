﻿using ExpectedObjects;
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
                CargaHoraria = 500,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 199.99m
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

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
            var cursoEsperado = new
            {
                Nome = "Curso 01",
                CargaHoraria = 500,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 199.99m
            };

            Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
            ).ComMensagem("Nome não pode ser nulo ou uma string vazia");
        }

        /// <summary>
        /// Carga horaria nao pode ser invalida
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void NaoDeveCursoTerCargaHorariaInvalida(int cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Curso 01",
                CargaHoraria = 500,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 199.99m
            };

           Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
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
            var cursoEsperado = new
            {
                Nome = "Curso 01",
                CargaHoraria = 500,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 199.99m
            };

            Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido)
            ).ComMensagem("Valor do curso não pode ser menor ou igual a zero");
        }
    }

    public class Curso
    {
        public Curso(string nome, int cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome não pode ser nulo ou uma string vazia");

            if (cargaHoraria <= 0)
                throw new ArgumentException("Carga horária não pode ser menor ou igual a zero");

            if (valor <= 0)
                throw new ArgumentException("Valor do curso não pode ser menor ou igual a zero");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
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