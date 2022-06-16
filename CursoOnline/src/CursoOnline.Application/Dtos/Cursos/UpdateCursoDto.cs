namespace CursoOnline.Application.Dtos.Cursos
{
    public class UpdateCursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public decimal Valor { get; set; }
    }
}