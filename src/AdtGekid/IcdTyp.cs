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
    /// Datentyp für onkologische Kodierungen nach ICD-10
    /// </summary>
    /// <seealso cref="StringTypBase" />
    public class IcdTyp : StringTypBase
    {
        private static Regex _pattern = new Regex(@"^[CD]\d\d(\.\d(\d)?)?$");

        /// <summary>
        /// Erstellt eine leere <see cref="IcdTyp"/> Instanz.
        /// </summary>
        public IcdTyp():base()
        { }

        /// <summary>
        /// Erstellt eine <see cref="IcdTyp"/> Instanz mit gegebenem Code.
        /// </summary>
        /// <param name="code">The code.</param>
        public IcdTyp(string code):base()
        {
            SetString(code);
        }

        protected override bool AllowEmpty => false;

        protected override bool IsStringValid(string str) => _pattern.IsMatch(str.Trim());

        /// <summary>
        /// Implizite Kovertierung/Parsen von <see cref="string"/> nach <see cref="IcdTyp"/>.
        /// </summary>
        /// <param name="str">Der zu parsende String mit dem ICD-10 Code.</param>
        /// <returns>Der resultierende <see cref="IcdTyp"/>-Wert oder <c>null</c>.</returns>
        public static implicit operator IcdTyp(string str)
        {
            if(str == null)
            {
                return null;
            }

            var result = new IcdTyp();
            result.SetString(str);
            return result;
        }

        protected override string TransformNonemptyString(string str) => str.Trim().ToUpper();
    }
}

