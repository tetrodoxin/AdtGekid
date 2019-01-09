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
    public class StringValidatorByLength : StringValueValidatorBase
    {
        private static Lazy<IValueValidator<string>> _instance100 = new Lazy<IValueValidator<string>>(() => new StringValidatorByLength(100));
        private static Lazy<IValueValidator<string>> _instance16 = new Lazy<IValueValidator<string>>(() => new StringValidatorByLength(255));
        private static Lazy<IValueValidator<string>> _instance255 = new Lazy<IValueValidator<string>>(() => new StringValidatorByLength(255));
        private static Lazy<IValueValidator<string>> _instance50 = new Lazy<IValueValidator<string>>(() => new StringValidatorByLength(50));
        private static Lazy<IValueValidator<string>> _instance500 = new Lazy<IValueValidator<string>>(() => new StringValidatorByLength(500));

        private int _maxLength;

        public static IValueValidator<string> Max16 => _instance16.Value;
        public static IValueValidator<string> Max50 => _instance50.Value;
        public static IValueValidator<string> Max100 => _instance100.Value;
        public static IValueValidator<string> Max255 => _instance255.Value;
        public static IValueValidator<string> Max500 => _instance500.Value;

          

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorForStrings" />
        /// mit einem angegebenen Verhalten für StringWerte.
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="maxLength">Die Maximale Länge des Strings.</param>
        /// <exception cref="System.ArgumentException">Falls <c>RegEx</c> keinen gültigen Wert enthielt,
        /// <c>null</c> oder eer war.</exception>
        public StringValidatorByLength(StringValidatorBehavior behavior, int maxLength) : base(behavior)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorForStrings" />
        /// mit einem angegebenen Verhalten für StringWerte mit <see cref="StringValidatorBehavior.TrimAllowEmpty"/>.
        /// </summary>
        /// <param name="maxLength">Die Maximale Länge des Strings.</param>
        /// <exception cref="System.ArgumentException">Falls <c>RegEx</c> keinen gültigen Wert enthielt,
        /// <c>null</c> oder eer war.</exception>
        public StringValidatorByLength(int maxLength) : this(StringValidatorBehavior.TrimAllowEmpty, maxLength)
        { }

        public StringValidatorByLength(int maxLength, string validatedAdtObject, string validatedAdtField)
            : this(maxLength)
        {
            base.ValidateAdtObject = validatedAdtObject;
            base.ValidatedAdtField = validatedAdtField;
        }

        public static StringValidatorByLength CreateInstanceForMax16(string validatedAdtObject, string validatedAdtField)
        {
            return new StringValidatorByLength(16, validatedAdtObject, validatedAdtField);            
        }

        public static StringValidatorByLength CreateInstanceForMax100(string validatedAdtObject, string validatedAdtField)
        {
            return new StringValidatorByLength(100, validatedAdtObject, validatedAdtField);
        }
        public static StringValidatorByLength CreateInstanceForMax50(string validatedAdtObject, string validatedAdtField)
        {
            return new StringValidatorByLength(50, validatedAdtObject, validatedAdtField);
        }

        public static StringValidatorByLength CreateInstanceForMax255(string validatedAdtObject, string validatedAdtField)
        {
            return new StringValidatorByLength(255, validatedAdtObject, validatedAdtField);
        }

        public static StringValidatorByLength CreateInstanceForMax500(string validatedAdtObject, string validatedAdtField)
        {
            return new StringValidatorByLength(500, validatedAdtObject, validatedAdtField);
        }

        protected override int CalculateHashCode() => _maxLength;

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
        protected override bool EqualsSameTypeSameHashCode(StringValueValidatorBase other) => true;

        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            if (stringToValidate.Length > _maxLength)
            {
                return $"Wert '{stringToValidate}' ist länger als {_maxLength} Zeichen.";
            }

            return null;
        }

 // same hash (_maxLength) means equal
    }
}