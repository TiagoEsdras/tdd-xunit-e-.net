using System;

namespace CursoOnline.Application.Dtos.Matriculas
{
    public class CreateMatriculaDto
    {
        /// <summary>
        /// Id do aluno
        /// </summary>
        public Guid AlunoId { get; set; }
        /// <summary>
        /// Id do curso
        /// </summary>
        public Guid CursoId { get; set; }
        /// <summary>
        /// Valo pago no ato da matricula
        /// </summary>
        public decimal ValorPago { get; set; }
    }
}