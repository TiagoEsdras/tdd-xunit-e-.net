using CursoOnline.Domain.Enums;
using System;

namespace CursoOnline.Domain.Cursos
{
    public class Curso
    {
        public Curso(string nome, string descricao, int cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome não pode ser nulo ou uma string vazia");

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição não pode ser nula ou uma string vazia");

            if (cargaHoraria <= 0)
                throw new ArgumentException("Carga horária não pode ser menor ou igual a zero");

            if (valor <= 0)
                throw new ArgumentException("Valor do curso não pode ser menor ou igual a zero");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }
    }
}