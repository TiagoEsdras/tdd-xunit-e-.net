using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using CursoOnline.Domain.Helpers;
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

            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentException(ErroMessage.CPF_NULO_OU_VAZIO);

            if (!ValidadorDeCPF.IsCpf(cpf))
                throw new ArgumentException(ErroMessage.CPF_INVALIDO);

            Nome = nome;
            CPF = cpf;
            Email = email;
            PublicoAlvo = publicoAlvo;
        }
    }
}