using System.Net.Mail;

namespace CursoOnline.Domain.Helpers
{
    public static class ValidadorDeEmail
    {
        public static bool IsValid(string email)
        {
            try
            {
                MailAddress m = new(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}