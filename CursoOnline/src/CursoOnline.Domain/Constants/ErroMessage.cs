namespace CursoOnline.Domain.Constants
{
    public static class ErroMessage
    {
        public const string NOME_INVALIDO = "Nome não pode ser nulo ou uma string vazia";
        public const string DESCRICAO_INVALIDA = "Descrição não pode ser nula ou uma string vazia";
        public const string CARGA_HORARIA_INVALIDA = "Carga horária não pode ser menor ou igual a zero";
        public const string VALOR_INVALIDO = "Valor do curso não pode ser menor ou igual a zero";
        public const string NOME_DO_CURSO_JA_EXISTENTE = "Nome do curso já consta no banco de dados";
        public const string PUBLICO_ALVO_INVALIDO = "Público Alvo inválido";
        public const string INTERNAL_SERVER_ERROR = "Internal server error";
    }
}