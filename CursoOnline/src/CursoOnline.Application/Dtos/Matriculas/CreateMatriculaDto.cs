using System;

namespace CursoOnline.Application.Dtos.Matriculas
{
    public class CreateMatriculaDto
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal ValorPago { get; set; }
    }
}