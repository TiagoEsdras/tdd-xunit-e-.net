using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Tests.Builders;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Domain.Tests.Alunos
{
    public class AlunoTest
    {
        /// <summary>
        /// Deve criar um aluno com nome, cpf, email e publico alvo;
        /// </summary>
        [Fact]
        public void DeveCriarUmAluno()
        {
            var alunoEsperado = new
            {
                Nome = "Fulano de Tal",
                CPF = "75459341728",
                Email = "criacao_aluno@teste.com",
                PublicoAlvo = PublicoAlvoEnum.Estudante,
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.CPF, alunoEsperado.Email, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        /// <summary>
        /// Nome não pode ser invalido
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlunoTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                AlunoBuilder.Novo().ComNome(nomeInvalido).Build()
            ).ComMensagem(ErroMessage.NOME_INVALIDO);
        }

        /// <summary>
        /// CPF não pode ser nulo ou vazio
        /// </summary>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlunoTerCPFNuloOuVazio(string cpfInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                AlunoBuilder.Novo().ComCPF(cpfInvalido).Build()
            ).ComMensagem(ErroMessage.CPF_NULO_OU_VAZIO);
        }

        /// <summary>
        /// CPF não pode ser invalido
        /// </summary>
        [Theory]
        [InlineData("12345678910")]
        [InlineData("11111111111")]
        [InlineData("11111111119")]
        [InlineData("xxxxxxxxxxx")]
        public void NaoDeveAlunoTerCPFInvalido(string cpfInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                AlunoBuilder.Novo().ComCPF(cpfInvalido).Build()
            ).ComMensagem(ErroMessage.CPF_INVALIDO);
        }
    }
}