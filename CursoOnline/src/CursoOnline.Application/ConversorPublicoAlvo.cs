using CursoOnline.Application.Contratos;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using System;

namespace CursoOnline.Application
{
    public class ConversorPublicoAlvo : IConversorPublicoAlvo
    {
        public PublicoAlvoEnum Converter(string publicoAlvo)
        {
            if (!Enum.TryParse<PublicoAlvoEnum>(publicoAlvo, out var publicoAlvoConvertido))
                throw new ArgumentException(ErroMessage.PUBLICO_ALVO_INVALIDO);

            return publicoAlvoConvertido;
        }
    }
}