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
            if (valorPago > curso.Valor)
                throw new ArgumentException(ErroMessage.VALOR_PAGO_MAIOR_QUE_VALOR_DO_CURSO);
            if (aluno.PublicoAlvo != curso.PublicoAlvo)
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_DE_CURSO_E_ALUNO_DIFERENTES);

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            ExisteDesconto = valorPago < curso.Valor;
        }

        [Key]
        public Guid Id { get; private set; }

        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorPago { get; private set; }

        public bool ExisteDesconto { get; private set; }
    }
}