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
using System.Linq;
using System.Text.RegularExpressions;

namespace AdtGekid.Validation
{
    public class StringValidatorByRegex : StringValueValidatorBase
    {
        private Regex _regex;
        private string _pattern;
        private int _hash;

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorForStrings" />
        /// mit einem angegebenen Verhalten für StringWerte.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="regexPattern">Das zu verwendende RegEx Pattern..</param>
        /// <exception cref="System.ArgumentException">Falls <c>RegEx</c> keinen gültigen Wert enthielt, 
        /// <c>null</c> oder eer war.</exception>
        public StringValidatorByRegex(StringValidatorBehavior behavior, string regexPattern) : base(behavior)
        {
            if (string.IsNullOrEmpty(regexPattern))
            {
                throw new ArgumentException($"'{nameof(regexPattern)}' darf weder NULL noch leer sein.");
            }

            _pattern = regexPattern;
            _regex = new Regex(regexPattern);
            _hash = regexPattern.GetHashCode();
        }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorForStrings" />
        /// mit einem angegebenen Verhalten für StringWerte.
        /// </summary>
        /// <param name="regexPattern">Das zu verwendende RegEx Pattern..</param>
        /// <exception cref="System.ArgumentException">Falls <c>RegEx</c> keinen gültigen Wert enthielt, 
        /// <c>null</c> oder eer war.</exception>
        public StringValidatorByRegex(string regexPattern) : this(StringValidatorBehavior.UpcaseTrimAllowEmpty, regexPattern)
        { }

        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            if (!_regex.IsMatch(stringToValidate))
            {
                return $"Unerlaubter Wert '{stringToValidate}'";
            }

            return null;
        }

        /// <summary>
        /// Gibt in überschreibenden Klassen zurück, ob die aktuelle Instanz
        /// gleich der übergebenen Instanz ist, welche vom selben Typ (identisch,
        /// nicht nur verwandt), also auch nicht <c>null</c> ist und außerdem
        /// den selben HashCode (siehe <see cref="CalculateHashCode" />) besitzt.
        /// </summary>
        /// <param name="other">Die Vergleichsinstanz des selben Typs und HashCodes.</param>
        /// <returns>
        /// <c>true</c>, falls die Vergleichsinstanz gleich dieser Instanz ist,
        /// sonst <c>false</c>
        /// </returns>
        protected override bool EqualsSameTypeSameHashCode(StringValueValidatorBase other)
                    => _pattern == ((StringValidatorByRegex)other)._pattern;

        protected override int CalculateHashCode() => _hash;
    }
}