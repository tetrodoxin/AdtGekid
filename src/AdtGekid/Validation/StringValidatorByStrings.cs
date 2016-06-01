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

namespace AdtGekid.Validation
{
    public class StringValidatorByStrings : StringValueValidatorBase
    {
        private string[] _allowedStrings;
        private int _maxLen = 0;
        private int _hash;

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByStrings" />
        /// mit einem angegebenen Verhalten für StringWerte.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="allowedStrings">Ein Array mit den gültigen Werten.</param>
        /// <param name="maxLength">Die Maximallänge des Strings oder <c>0</c>, falls unbegrenzt.</param>
        /// <exception cref="ArgumentNullException">Wenn <c>allowedStrings</c> nicht angegeben.</exception>
        /// <exception cref="ArgumentException">Wenn einer der Werte in <c>allowedStrings</c> leer oder <c>null</c> war.</exception>
        public StringValidatorByStrings(StringValidatorBehavior behavior, string[] allowedStrings, int maxLength) : base(behavior)
        {
            if (allowedStrings == null)
            {
                throw new ArgumentNullException(nameof(allowedStrings));
            }

            if (allowedStrings.Any(p => string.IsNullOrEmpty(p)))
            {
                throw new ArgumentException($"Keiner der Werte in {nameof(allowedStrings)} darf null sein.");
            }

            _maxLen = maxLength;
            _allowedStrings = allowedStrings.ToArray();
            _hash = string.Join(string.Empty, _allowedStrings).GetHashCode() * 397;
            _hash ^= _maxLen.GetHashCode();
        }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByStrings"/>
        /// welche <see cref="StringValidatorBehavior.UpcaseTrimAllowEmpty"/> als Verhalten verwendet.
        /// </summary>
        /// <param name="allowedStrings">Ein Array mit den gültigen Werten.</param>
        /// <param name="maxLength">Die Maximallänge des Strings oder <c>0</c>, falls unbegrenzt.</param>
        public StringValidatorByStrings(string[] allowedStrings, int maxLength) : this(StringValidatorBehavior.UpcaseTrimAllowEmpty, allowedStrings, maxLength)
        { }

        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            if (_maxLen > 0 && stringToValidate.Length > _maxLen)
            {
                return $"Feld erlaubt maximal {_maxLen} Zeichen.";
            }

            if (!_allowedStrings.Contains(stringToValidate))
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
            => _allowedStrings.All(p => ((StringValidatorByStrings)other)._allowedStrings.Contains(p))
                && _maxLen == ((StringValidatorByStrings)other)._maxLen;


        protected override int CalculateHashCode() => _hash;
    }
}