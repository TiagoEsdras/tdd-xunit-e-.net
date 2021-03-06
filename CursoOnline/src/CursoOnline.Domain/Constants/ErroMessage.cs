namespace CursoOnline.Domain.Constants
{
    public static class ErroMessage
    {
        public const string NOME_INVALIDO = "Nome não pode ser nulo ou uma string vazia";
        public const string ID_INVALIDO = "Id nãp pode ser uma string vazia ou um guid empty";
        public const string CPF_NULO_OU_VAZIO = "CPF não pode ser nulo ou uma string vazia";
        public const string EMAIL_NULO_OU_VAZIO = "Email não pode ser nulo ou uma string vazia";
        public const string CPF_INVALIDO = "Cpf inválido";
        public const string EMAIL_INVALIDO = "Email inválido";
        public const string DESCRICAO_INVALIDA = "Descrição não pode ser nula ou uma string vazia";
        public const string CARGA_HORARIA_INVALIDA = "Carga horária não pode ser menor ou igual a zero";
        public const string VALOR_INVALIDO = "Valor do curso não pode ser menor ou igual a zero";
        public const string NOME_DO_CURSO_JA_EXISTENTE = "Nome do curso já consta no banco de dados";
        public const string ALUNO_COM_CPF_JA_EXISTENTE = "Aluno com cpf já consta no banco de dados";
        public const string PUBLICO_ALVO_INVALIDO = "Público Alvo inválido";
        public const string INTERNAL_SERVER_ERROR = "Internal server error";
        public const string ALUNO_NAO_EXISTENTE = "Não existe aluno com o id informado";
        public const string CURSO_NAO_EXISTENTE = "Não existe curso com o id informado";
        public const string ALUNO_INVALIDO = "Aluno inválido";
        public const string CURSO_INVALIDO = "Curso inválido";
        public const string VALOR_PAGO_INVALIDO = "Valor pago não pode ser menor ou igual a zero";
        public const string VALOR_PAGO_MAIOR_QUE_VALOR_DO_CURSO = "Valor pago não pode ser maior que o valor do curso";
        public const string PUBLICO_ALVO_DE_CURSO_E_ALUNO_DIFERENTES = "Público alvo de curso e aluno são diferentes";
        public const string ALUNO_JA_MATRICULADO_PARA_CURSO = "Aluno já está matriculado para o curso";
    }
}