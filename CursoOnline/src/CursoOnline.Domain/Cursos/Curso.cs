using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Domain.Cursos
{
    public class Curso
    {
        public Curso(string nome, string descricao, int cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException(ErroMessage.NOME_INVALIDO);

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException(ErroMessage.DESCRICAO_INVALIDA);

            if (cargaHoraria <= 0)
                throw new ArgumentException(ErroMessage.CARGA_HORARIA_INVALIDA);

            if (valor <= 0)
                throw new ArgumentException(ErroMessage.VALOR_INVALIDO);

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }
    }
}