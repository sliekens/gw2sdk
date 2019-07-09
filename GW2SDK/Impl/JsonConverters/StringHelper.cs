using System;
using GW2SDK.Annotations;

namespace GW2SDK.Impl.JsonConverters
{
    internal static class StringHelper
    {
        internal static string ToSnakeCase([NotNull] string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length > 1)
            {
                bool inWord = false;
                for (var i = text.Length - 1; i > 0; i--)
                {
                    if (char.IsLower(text[i]))
                    {
                        inWord = true;
                    }
                    else
                    {
                        if (inWord || !char.IsUpper(text[i - 1]))
                        {
                            text = text.Insert(i, "_");
                        }

                        inWord = false;
                    }
                    
                }
            }

            return text.ToLowerInvariant();
        }
    }
}
