namespace CursoOnline.Application.Dtos.Cursos
{
    public class UpdateCursoDto
    {
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
        /// Valor do curso
        /// </summary>
        public decimal Valor { get; set; }
    }
}