using System.Text.RegularExpressions;

namespace FirstDecisionDesafioMoises.Infraestructure.Extensions
{
    public static class StringExtensions
    {
        public static string SomenteNumeros(this string texto)
        {
            return Regex.Replace(texto ?? string.Empty, "[^0-9,]", string.Empty);
        }
    }
}