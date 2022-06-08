using CursoOnline.Domain.Enums;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }

    public class Aluno
    {
        [Key]
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }

        public Aluno(string nome, string cpf, string email, PublicoAlvoEnum publicoAlvo)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
            PublicoAlvo = publicoAlvo;
        }
    }
}
