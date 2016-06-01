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
namespace AdtGekid.Validation
{
    public class StringValidatorByLowerChars : StringValidatorByChars
    {
        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars"/>
        /// mit <see cref="StringValidatorBehavior.LowcaseTrimAllowEmpty"/>
        /// </summary>
        /// <param name="behavior">Das zu verwendende Verhalten für die Stringwerte.</param>
        /// <param name="allowedChars">Ein String mit den gültigen Zeichen.</param>
        public StringValidatorByLowerChars(string allowedChars) : base(StringValidatorBehavior.LowcaseTrimAllowEmpty, allowedChars.ToCharArray())
        { }

        /// <summary>
        /// Erzeugt eine Instanz von <see cref="StringValidatorByChars"/>
        /// welche <see cref="StringValidatorBehavior.LowcaseTrimAllowEmpty"/> als Verhalten verwendet.
        /// </summary>
        /// <param name="allowedChars">Ein Array mit den gültigen Werten.</param>
        public StringValidatorByLowerChars(char[] allowedChars) : base(StringValidatorBehavior.LowcaseTrimAllowEmpty, allowedChars)
        { }
    }
}