using CursoOnline.Application;
using CursoOnline.Domain.Constants;
using CursoOnline.Domain.Enums;
using System;
using Xunit;

namespace CursoOnline.Domain.Tests.PublicoAlvo
{
    public class ConversorPublicoAlvoTest
    {
        private readonly ConversorPublicoAlvo _conversorPublicoAlvo;

        public ConversorPublicoAlvoTest()
        {
            _conversorPublicoAlvo = new ConversorPublicoAlvo();
        }

        [Theory]
        [InlineData(PublicoAlvoEnum.Estudante, "Estudante")]
        [InlineData(PublicoAlvoEnum.Universitario, "Universitario")]
        [InlineData(PublicoAlvoEnum.Empregado, "Empregado")]
        [InlineData(PublicoAlvoEnum.Empreendedor, "Empreendedor")]
        public void DeveConverterPublicoAlvo(PublicoAlvoEnum publicoAlvo, string publicoAlvoString)
        {
            var publicoAlvoConvertido = _conversorPublicoAlvo.Converter(publicoAlvoString);

            Assert.Equal(publicoAlvoConvertido, publicoAlvo);
        }

        [Fact]
        public void DeveLancarExcecaoAoTentarConverterPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";

            var error = Assert.Throws<ArgumentException>(() => _conversorPublicoAlvo.Converter(publicoAlvoInvalido));

            error.ComMensagem(ErroMessage.PUBLICO_ALVO_INVALIDO);
        }
    }
}