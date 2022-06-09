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

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string PublicoAlvo { get; set; }
    }
}