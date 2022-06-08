using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Domain.Alunos
{
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
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException(ErroMessage.NOME_INVALIDO);

            Nome = nome;
            CPF = cpf;
            Email = email;
            PublicoAlvo = publicoAlvo;
        }
    }
}