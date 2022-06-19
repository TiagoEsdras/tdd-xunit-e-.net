using CursoOnline.Domain.Enums;

namespace CursoOnline.Application.Contratos
{
    public interface IConversorPublicoAlvo
    {
        PublicoAlvoEnum Converter(string publicoAlvo);
    }
}