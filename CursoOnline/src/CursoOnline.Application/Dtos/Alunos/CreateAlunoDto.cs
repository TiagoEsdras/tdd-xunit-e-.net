namespace CursoOnline.Application.Dtos.Alunos
{
    public class CreateAlunoDto
    {
        /// <summary>
        /// Nome do aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// CPF do aluno
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Email do aluno
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Publico alvo do aluno (Estudante, Universitario, Empregado, Empreendedor)
        /// </summary>
        public string PublicoAlvo { get; set; }
    }
}