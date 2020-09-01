#region license

//MIT License

//Copyright(c) 2016 Andreas Huebner

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdtGekid
{
    /// <summary>
    /// Definiert Erweiterungsmethode für <see cref="string"/>-Objekte.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Erzwingt die Kleinschreibung von Strings.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Ein <see cref="string"/>, der nur Kleinschreibung enthält und dem führende und angehängte
        /// Leerzeichen entfernt wurden (Trim) bzw. <c>null</c>, falls der übergebene String leer oder ebenfalls <c>null</c> war.</returns>
        public static string EnforceTrimmedLowerCase(this string str)
        {
            if (IsNothing(str))
            {
                return null;
            }
            else
            {
                return str.Trim().ToLower();
            }
        }

        /// <summary>
        /// Erzwingt die Großschreibung von Strings.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Ein <see cref="string"/>, der nur Großschreibung enthält und dem führende und angehängte
        /// Leerzeichen entfernt wurden (Trim) bzw. <c>null</c>, falls der übergebene String leer oder ebenfalls <c>null</c> war.</returns>
        public static string EnforceTrimmedUpperCase(this string str)
        {
            if (IsNothing(str))
            {
                return null;
            }
            else
            {
                return str.Trim().ToUpper();
            }
        }

        /// <summary>
        /// Bestimmt, ob der String <c>null</c> oder leer ist oder aber
        /// ausschließlich aus Leerzeichen besteht.
        /// </summary>
        /// <param name="str">Zu prüfender <see cref="string"/>.</param>
        /// <returns><c>true</c> falls der String <c>null</c> oder ein leerer String war
        /// oder wenn er lediglich Leerzeichen enthielt. Andernfalls <c>false</c>.</returns>
        public static bool IsNothing(this string str)
        {
            if(str == null)
            {
                return true;
            }

            str = str.Trim();
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Versucht einen String in einen Integer umzuwandeln.
        /// </summary>
        /// <param name="str">Der zu parsende String.</param>
        /// <returns>Einen <see cref="int?"/>, der den geparsten Integer
        /// enthält, sofern dieser korrekt erkannt werden konnte, andernfalls <c>null</c>.</returns>
        public static int? ParseNullableInt(this string str)
        {
            if(str.IsNothing())
            {
                return null;
            }
            else
            {
                int i;
                if(int.TryParse(str, out i))
                {
                    return i;
                }

                return null;
            }
        }

        /// <summary>
        /// Prüft ob die angegebene Zeichenfolge mit dem Typ <see cref="int"/> 
        /// kompatibel ist.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIntegerCompatible(this string str)
        {
            return int.TryParse(str, out _);
        }
    }
}
