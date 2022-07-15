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

        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal ValorPago { get; set; }
        public bool ExisteDesconto { get; }
    }
}