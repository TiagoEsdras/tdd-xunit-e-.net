using CursoOnline.Domain.Cursos;
using System;

namespace CursoOnline.Application.Dtos.Cursos
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

        /// <summary>
        /// Id do curso
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nome do curso
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descricao do curso
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Carga horaria do curso
        /// </summary>
        public int CargaHoraria { get; set; }
        /// <summary>
        /// Publico alvo do curso (Estudante, Universitario, Empregado, Empreendedor)
        /// </summary>
        public string PublicoAlvo { get; set; }
        /// <summary>
        /// Valor do curso
        /// </summary>
        public decimal Valor { get; set; }
    }
}