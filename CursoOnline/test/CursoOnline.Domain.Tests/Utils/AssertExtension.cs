using System;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.Domain.Tests
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException ex, string mensagem)
        {
            if (ex.Message == mensagem)
                Assert.True(true);
            else
                Assert.True(false, $"Erro esperado: '{mensagem}'; Erro lançado: '{ex.Message}'");
        }
    }
}