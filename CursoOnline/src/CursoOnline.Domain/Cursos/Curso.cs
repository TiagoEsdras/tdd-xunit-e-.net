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
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException(ErroMessage.NOME_INVALIDO);

            Nome = nome;
        }

        public void AlterarCargaHoraria(int cargaHoraria)
        {
            if (cargaHoraria <= 0)
                throw new ArgumentException(ErroMessage.CARGA_HORARIA_INVALIDA);

            CargaHoraria = cargaHoraria;
        }

        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException(ErroMessage.DESCRICAO_INVALIDA);

            Descricao = descricao;
        }

        public void AlterarValor(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException(ErroMessage.VALOR_INVALIDO);

            Valor = valor;
        }
    }
}