using CursoOnline.Domain.Alunos;
using System;

namespace CursoOnline.Application.Dtos.Alunos
{
    public class AlunoDto
    {
        public AlunoDto()
        {
        }

        public AlunoDto(Aluno aluno)
        {
            Id = aluno.Id;
            Nome = aluno.Nome;
            CPF = aluno.CPF;
            Email = aluno.Email;
            PublicoAlvo = aluno.PublicoAlvo.ToString();
        }

        /// <summary>
        /// Id do aluno
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nome do aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// CPF do aluno
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Email do aluno
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Publico alvo do aluno (Estudante, Universitario, Empregado, Empreendedor)
        /// </summary>
        public string PublicoAlvo { get; set; }
    }
}