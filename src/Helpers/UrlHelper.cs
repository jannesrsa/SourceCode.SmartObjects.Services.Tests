using System.Text;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    public static class UrlHelper
    {
        public static string InvalidPathChars { get; } = GetInvalidPathChars();

        /// <summary>
        /// Encodes the string to use in the path part (before the ?) of a Url.
        /// </summary>
        /// <param name="text">The text to encode.</param>
        /// <returns>Returns the encode string.</returns>
        /// <remarks>
        /// A url has a {path}?{query}.
        /// There are a few characters that cannot be part of the url path before a the ? in IIS whether they are Url encoded or not:
        ///
        /// Characters: %, &, *, or :
        ///
        /// See:
        /// http://lostechies.com/joshuaflanagan/2009/04/28/asp-net-400-bad-request-with-restricted-characters/
        /// http://stackoverflow.com/questions/4599940/getting-error-400-404-httputility-urlencode-not-encoding-full-string
        /// http://www.ietf.org/rfc/rfc2396.txt
        /// </remarks>
        public static string VanityEncode(string text)
        {
            // Variables
            byte[] bytes = new byte[10]; // Buffer used to encode characters
            int byteCount = 0; // The number of bytes to encode the character

            text.ThrowIfNull(nameof(text));

            int textCharLength = text.Length;
            StringBuilder builder = new StringBuilder();

            // Loop through the supplied text and build up the encoded text
            for (int index = 0; index < textCharLength; index++)
            {
                // Current char
                char c = text[index];

                if (c == '.' && index == textCharLength - 1)
                {
                    break;
                }

                // Special Case 1: Handle spaces
                if (c == ' ')
                {
                    builder.Append('+');
                    continue;
                }

                // Special Case 2: Handle underscores
                if (c == '_')
                {
                    builder.Append('_');
                    builder.Append('_');
                    continue;
                }

                // Special Case 3: Invalid path characters and control characters
                if (InvalidPathChars.IndexOf(c) != -1)
                {
                    byteCount = Encoding.UTF8.GetBytes(text, index, 1, bytes, 0);
                    for (int byteIndex = 0; byteIndex < byteCount; byteIndex++)
                    {
                        builder.Append('_');
                        builder.Append(bytes[byteIndex].ToString("X2"));
                    }
                    continue;
                }

                // Normal case
                builder.Append(c);
            }

            if (textCharLength > 0 && text[textCharLength - 1] == '.')
            {
                builder.Append("_2E");
            }

            // Return the built encoded
            return builder.ToString();
        }

        private static string GetInvalidPathChars()
        {
            StringBuilder builder = new StringBuilder();

            // Add control chars (0x00 - 0x1F)
            int code = 0;
            while (code < 0x20)
            {
                builder.Append((char)code++);
            }
            builder.Append('\x7F'); // DEL Control Char

            builder.Append("\"<>|:*?\\/#%&+");

            return builder.ToString();
        }
    }
}