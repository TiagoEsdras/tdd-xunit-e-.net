using CursoOnline.Domain.Cursos;
using System;

namespace CursoOnline.Application.Dtos
{
    public class CursoDto
    {
        public CursoDto()
        {
        }

        public CursoDto(Curso curso)
        {
            Id = curso.Id;
            Nome = curso.Nome;
            Descricao = curso.Descricao;
            CargaHoraria = curso.CargaHoraria;
            PublicoAlvo = curso.PublicoAlvo.ToString();
            Valor = curso.Valor;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}