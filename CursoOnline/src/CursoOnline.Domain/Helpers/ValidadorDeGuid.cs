using CursoOnline.Domain.Constants;
using System;

namespace CursoOnline.Domain.Helpers
{
    public static class ValidadorDeGuid
    {
        public static void IsValid(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(ErroMessage.ID_INVALIDO);
        }
    }
}