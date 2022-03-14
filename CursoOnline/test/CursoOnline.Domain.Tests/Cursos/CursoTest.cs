using System;
using Xunit;

namespace CursoOnline.Domain.Tests.Cursos
{
    public class CursoTest
    {
        /// <summary>
        /// Deve criar um curso com nome, carga horária, público alvo, e valor do curso
        /// </summary>
        [Fact]
        public void DeveCriarUmCurso()
        {
            string nome = "Curso 01";
            int cargaHoraria = 500;
            string publicoAlvo = "Estudantes";
            decimal valor = 199.99m;

            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
        }
    }

    public class Curso
    {
        public Curso(string nome, int cargaHoraria, string publicoAlvo, decimal valor)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}