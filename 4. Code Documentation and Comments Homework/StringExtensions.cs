namespace Telerik.ILS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for System.String class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the MD5 hash of current string
        /// </summary>
        /// <param name="input">This instance of string.</param>
        /// <returns>A new string containing the hash of the string.</returns>
        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }

        /// <summary>
        /// Checks whether the string can be
        /// evaluated as <see cref="System.Boolean"/> "true" value.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>
        /// True if the string can be converted to
        /// <see cref="System.Boolean"/> "true".
        /// </returns>
        public static bool ToBoolean(this string input)
        {
            var stringTrueValues = new[] { "true", "ok", "yes", "1", "да" };
            return stringTrueValues.Contains(input.ToLower());
        }

        /// <summary>
        /// Converts string representation of a number to
        /// its 16-bit signed integer equivalent.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A 16-bit signed integer if string can be converted</returns>
        public static short ToShort(this string input)
        {
            short shortValue;
            short.TryParse(input, out shortValue);
            return shortValue;
        }

        /// <summary>
        /// Converts string representation of a number to
        /// its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A 32-bit signed integer if string can be converted</returns>
        public static int ToInteger(this string input)
        {
            int integerValue;
            int.TryParse(input, out integerValue);
            return integerValue;
        }

        /// <summary>
        /// Converts string representation of a number to
        /// its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A 64-bit signed integer if string can be converted</returns>
        public static long ToLong(this string input)
        {
            long longValue;
            long.TryParse(input, out longValue);
            return longValue;
        }

        /// <summary>
        /// Converts string representation of time to
        /// its <see cref="System.DateTime"/> equivalent.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A <see cref="System.DateTime"/> value if string can be converted</returns>
        public static DateTime ToDateTime(this string input)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(input, out dateTimeValue);
            return dateTimeValue;
        }

        /// <summary>
        /// Capitalizes first letter of a string.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A string with the first letter capitalized.</returns>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + input.Substring(1, input.Length - 1);
        }

        /// <summary>
        /// Returns the string between <paramref name="startString"/> and <pararef name="endString"/>.
        /// Search starts from <paramref name="startFrom"/> index.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <param name="startString">Substring left delimiter</param>
        /// <param name="endString">Substring right delimiter</param>
        /// <param name="startFrom">Index to start search from</param>
        /// <returns>The string between <paramref name="startString"/> and
        /// <paramref name="endString"/> or System.String.Empty if string is not found</returns>
        public static string GetStringBetween(this string input, string startString, string endString, int startFrom = 0)
        {
            input = input.Substring(startFrom);
            startFrom = 0;
            if (!input.Contains(startString) || !input.Contains(endString))
            {
                return string.Empty;
            }

            var startPosition = input.IndexOf(startString, startFrom, StringComparison.Ordinal) + startString.Length;
            if (startPosition == -1)
            {
                return string.Empty;
            }

            var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);
            if (endPosition == -1)
            {
                return string.Empty;
            }

            return input.Substring(startPosition, endPosition - startPosition);
        }

        /// <summary>
        /// Converts all cyrillic letters to their latin representations.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>
        /// A string in which all cyrilic letters are replaced with
        /// their latin represenations.
        /// </returns>
        public static string ConvertCyrillicToLatinLetters(this string input)
        {
            var bulgarianLetters = new[]
                                       {
                                           "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                           "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я"
                                       };
            var latinRepresentationsOfBulgarianLetters = new[]
                                                             {
                                                                 "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                                 "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                                 "c", "ch", "sh", "sht", "u", "i", "yu", "ya"
                                                             };
            for (var i = 0; i < bulgarianLetters.Length; i++)
            {
                input = input.Replace(bulgarianLetters[i], latinRepresentationsOfBulgarianLetters[i]);
                input = input.Replace(bulgarianLetters[i].ToUpper(), latinRepresentationsOfBulgarianLetters[i].CapitalizeFirstLetter());
            }

            return input;
        }

        /// <summary>
        /// Converts all latin letters in a string to their cyrillic representations.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>
        /// A string in which all latin letters are replaced with
        /// their cyrillic representations.
        /// </returns>
        public static string ConvertLatinToCyrillicKeyboard(this string input)
        {
            var latinLetters = new[]
                                   {
                                       "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                                       "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                                   };

            var bulgarianRepresentationOfLatinKeyboard = new[]
                                                             {
                                                                 "а", "б", "ц", "д", "е", "ф", "г", "х", "и", "й", "к",
                                                                 "л", "м", "н", "о", "п", "я", "р", "с", "т", "у", "ж",
                                                                 "в", "ь", "ъ", "з"
                                                             };

            for (int i = 0; i < latinLetters.Length; i++)
            {
                input = input.Replace(latinLetters[i], bulgarianRepresentationOfLatinKeyboard[i]);
                input = input.Replace(latinLetters[i].ToUpper(), bulgarianRepresentationOfLatinKeyboard[i].ToUpper());
            }

            return input;
        }

        /// <summary>
        /// Converts all cyrillic letters to their latin representations
        /// and removes all non-alphanumeric characters except period('.').
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>
        /// A new string in which all occurrences of cyrillic letters
        /// are replaced with their latin representations and all
        /// non-alphanumeric characters except period are removed.
        /// </returns>
        public static string ToValidUsername(this string input)
        {
            input = input.ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.]+", string.Empty);
        }

        /// <summary>
        /// Converts all cyrillic letters to their latin representations,
        /// replaces all spaces(' ') with hyphens('-') and removes all non-alphanumeric
        /// characters.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>
        /// A new string in which all occurrences of cyrillic letters
        /// are replaced with their latin representations,
        /// all spaces are replaced with hyphens and all non-alphanumeric
        /// characters are removed.
        /// </returns>
        public static string ToValidLatinFileName(this string input)
        {
            input = input.Replace(" ", "-").ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.\-]+", string.Empty);
        }

        /// <summary>
        /// Gets the first <paramref name="charsCount"/> characters
        /// of the current string.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <param name="charsCount">Number of characters to be extracted</param>
        /// <returns>A new string with first <paramref name="charsCount"/> characters.</returns>
        public static string GetFirstCharacters(this string input, int charsCount)
        {
            return input.Substring(0, Math.Min(input.Length, charsCount));
        }

        /// <summary>
        /// Gets the type of a file which name
        /// is represented as a string.
        /// </summary>
        /// <param name="fileName">This instace of string</param>
        /// <returns>
        /// A new string with the file type.
        /// </returns>
        public static string GetFileExtension(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            string[] fileParts = fileName.Split(new[] { "." }, StringSplitOptions.None);
            if (fileParts.Count() == 1 || string.IsNullOrEmpty(fileParts.Last()))
            {
                return string.Empty;
            }

            return fileParts.Last().Trim().ToLower();
        }

        /// <summary>
        /// Gets content type of file according to its extension
        /// </summary>
        /// <param name="fileExtension">This instace of string.</param>
        /// <returns>
        /// A new string representing the content of current file extension.
        /// </returns>
        public static string ToContentType(this string fileExtension)
        {
            var fileExtensionToContentType = new Dictionary<string, string>
                                                 {
                                                     { "jpg", "image/jpeg" },
                                                     { "jpeg", "image/jpeg" },
                                                     { "png", "image/x-png" },
                                                     {
                                                         "docx",
                                                         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                                     },
                                                     { "doc", "application/msword" },
                                                     { "pdf", "application/pdf" },
                                                     { "txt", "text/plain" },
                                                     { "rtf", "application/rtf" }
                                                 };
            if (fileExtensionToContentType.ContainsKey(fileExtension.Trim()))
            {
                return fileExtensionToContentType[fileExtension.Trim()];
            }

            return "application/octet-stream";
        }

        /// <summary>
        /// Converts current string into a sequence of bytes.
        /// </summary>
        /// <param name="input">This instace of string.</param>
        /// <returns>A byte array containing the specified set of characters.</returns>
        public static byte[] ToByteArray(this string input)
        {
            var bytesArray = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);
            return bytesArray;
        }
    }
}
