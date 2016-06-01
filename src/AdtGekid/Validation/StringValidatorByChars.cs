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

namespace AdtGekid.Validation
{
    public class StringValidatorByChars : StringValueValidatorBase
    {
        private char[] _allowedChars;
        private int _hash;
        private int _allowedLength = -1;

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars"/>
        /// mit einem angegebenen Verhalten für StringWerte von einem Zeichen.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="allowedChars">Ein String mit den gültigen Zeichen.</param>
        public StringValidatorByChars(StringValidatorBehavior behavior, string allowedChars) : this(behavior, allowedChars.ToCharArray())
        { }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars"/>
        /// mit einem angegebenen Verhalten für StringWerte von einem Zeichen.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="allowedChars">Ein Array mit den gültigen Werten.</param>
        public StringValidatorByChars(StringValidatorBehavior behavior, char[] allowedChars) : this(behavior, allowedChars, -1)
        { }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars" />
        /// mit einem angegebenen Verhalten für StringWerte.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="allowedChars">Ein Array mit den gültigen Werten.</param>
        /// <param name="allowedLength">Falls Strings von mehr als einem Zeichen erlaubt sind, hier die maximale Länge. 0 bedeutet unbegrenzt.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public StringValidatorByChars(StringValidatorBehavior behavior, char[] allowedChars, int allowedLength) : base(behavior)
        {
            if (allowedChars == null)
            {
                throw new ArgumentNullException(nameof(allowedChars));
            }

            _allowedChars = allowedChars.ToArray();
            _allowedLength = allowedLength;
            _hash = (new string(_allowedChars).GetHashCode() * 397) ^ (_allowedLength.GetHashCode());
        }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars"/>
        /// welche <see cref="StringValidatorBehavior.UpcaseTrimAllowEmpty"/> als Verhalten verwendet.
        /// </summary>
        /// <param name="allowedChars">Ein Array mit den gültigen Werten.</param>
        public StringValidatorByChars(char[] allowedChars) : this(StringValidatorBehavior.UpcaseTrimAllowEmpty, allowedChars)
        { }

        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            if (_allowedLength < 0)
            {
                if (stringToValidate.Length != 1)
                {
                    return $"Wert '{stringToValidate}' ist länger als 1 Zeichen.";
                }
                else if (!_allowedChars.Contains(stringToValidate[0]))
                {
                    return $"Unerlaubter Wert '{stringToValidate}'";
                }
            }
            else
            {
                if (_allowedLength > 0 && stringToValidate.Length > _allowedLength)
                {
                    return $"Wert '{stringToValidate}' zu lang.";
                }
                else if (!stringToValidate.All(p => _allowedChars.Contains(p)))
                {
                    return $"Unerlaubter Wert '{stringToValidate}'";
                }
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
            => _allowedChars.All(p => ((StringValidatorByChars)other)._allowedChars.Contains(p))
                    && _allowedLength == ((StringValidatorByChars)other)._allowedLength;

        protected override int CalculateHashCode() => _hash;
    }
}