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
    /// <summary>
    /// Basisklasse für String-Validierer.
    /// </summary>
    public abstract class StringValueValidatorBase : ValueValidatorBase<string>, IEquatable<IValueValidator<string>>, IEquatable<StringValueValidatorBase>
    {
        private StringValidatorBehavior _behavior;

        protected StringValueValidatorBase(StringValidatorBehavior behavior)
        {
            _behavior = behavior;
        }

        public static bool Equals(StringValueValidatorBase x, StringValueValidatorBase y)
        {
            if (ReferenceEquals(x, y)) return true;
            if ((object)x == null || (object)y == null) return false;

            return x.CalculateHashCode() == y.CalculateHashCode()
                && x.EqualsSameTypeSameHashCode(y);
        }

        public bool Equals(StringValueValidatorBase other) => Equals(this, other);

        public bool Equals(IValueValidator<string> other) => Equals(this, other as StringValueValidatorBase);

        public override bool Equals(object obj) => Equals(this, obj as StringValueValidatorBase);

        public override int GetHashCode() => CalculateHashCode();

        /// <summary>
        /// Gibt in überschreibenden Klassen zurück, ob die aktuelle Instanz
        /// gleich der übergebenen Instanz ist, welche vom selben Typ (identisch, 
        /// nicht nur verwandt), also auch nicht <c>null</c> ist und außerdem
        /// den selben HashCode (siehe <see cref="CalculateHashCode"/>) besitzt.
        /// </summary>
        /// <param name="other">Die Vergleichsinstanz des selben Typs und HashCodes.</param>
        /// <returns><c>true</c>, falls die Vergleichsinstanz gleich dieser Instanz ist, 
        /// sonst <c>false</c></returns>
        protected abstract bool EqualsSameTypeSameHashCode(StringValueValidatorBase other);

        protected override string GetErrorText(string valueToValidate)
        {
            if (valueToValidate.IsNothing())
            {
                if (_behavior.AllowsEmpty())
                {
                    return null;
                }
                else
                {
                    return "NULL oder leerer String nicht erlaubt.";
                }
            }

            return GetErrorTextForNonEmpty(valueToValidate);
        }

        protected abstract string GetErrorTextForNonEmpty(string stringToValidate);

        protected abstract int CalculateHashCode();

        protected override string GetPreparedValue(string value)
        {
            switch (_behavior)
            {
                case StringValidatorBehavior.LowcaseTrimAllowEmpty:
                case StringValidatorBehavior.LowcaseTrimNonEmpty:
                    return value.EnforceTrimmedLowerCase();

                case StringValidatorBehavior.UpcaseTrimAllowEmpty:
                case StringValidatorBehavior.UpcaseTrimNonEmpty:
                    return value.EnforceTrimmedUpperCase();

                case StringValidatorBehavior.TrimAllowEmpty:
                case StringValidatorBehavior.TrimNonEmpty:
                    return string.IsNullOrEmpty(value) ? value : value.Trim();

                default:
                    return base.GetPreparedValue(value);
            }
        }
    }
}