using CursoOnline.Domain.Alunos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Cursos;
using System;
using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Domain.Matriculas
{
    public class Matricula
    {
        public Matricula(Aluno aluno, Curso curso, decimal valorPago)
        {
            if (aluno is null)
                throw new ArgumentException(ErroMessage.ALUNO_INVALIDO);
            if (curso is null)
                throw new ArgumentException(ErroMessage.CURSO_INVALIDO);
            if (valorPago <= 0)
                throw new ArgumentException(ErroMessage.VALOR_PAGO_INVALIDO);

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
        }

        [Key]
        public Guid Id { get; set; }

        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public decimal ValorPago { get; set; }
    }
}