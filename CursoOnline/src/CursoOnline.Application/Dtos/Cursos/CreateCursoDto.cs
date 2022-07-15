namespace CursoOnline.Application.Dtos.Cursos
{
    public class CreateCursoDto
    {
        /// <summary>
        /// Nome do curso
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descrição do curso
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