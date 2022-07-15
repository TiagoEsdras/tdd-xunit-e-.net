using CursoOnline.Domain.Matriculas;
using System;

namespace CursoOnline.Application.Dtos.Matriculas
{
    public class MatriculaDto
    {
        public MatriculaDto(Matricula matricula)
        {
            Id = matricula.Id;
            AlunoId = matricula.Aluno.Id;
            CursoId = matricula.Curso.Id;
            ValorPago = matricula.ValorPago;
            ExisteDesconto = matricula.ExisteDesconto;
        }

        /// <summary>
        /// Id da matricula
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id do aluno
        /// </summary>
        public Guid AlunoId { get; set; }
        /// <summary>
        /// Id do curso
        /// </summary>
        public Guid CursoId { get; set; }
        /// <summary>
        /// Valor pago no ato da matricula
        /// </summary>
        public decimal ValorPago { get; set; }
        /// <summary>
        /// Campo que indica se houve desconto
        /// </summary>
        public bool ExisteDesconto { get; }
    }
}