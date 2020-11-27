using System;

namespace SAEBRecommender.Extensions 
{
    public static class StringExtensions
    {
        public static bool IsValidUri(this string str)
        {
            Uri validatedUri;
            return Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out validatedUri);
        }
    }
}