using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Application.Helpers;

public static class UrlHelper
{
    public static string GenerateShortUrl(int length = 5)
    {
        var bytes = new byte[length];
        RandomNumberGenerator.Fill(bytes);

        var base64 = Convert.ToBase64String(bytes).Replace("+", "").Replace("/", "").Replace("=", "");

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(base64);

        return stringBuilder.ToString();
    }
}