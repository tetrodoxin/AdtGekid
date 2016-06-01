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
    /// Stringvalidierer für die Krankenversichertennummer.
    /// </summary>
    public class KvNrValidator : StringValidatorByRegex
    {
        private static readonly Lazy<IValueValidator<string>> _instance = new Lazy<IValueValidator<string>>(() => new KvNrValidator());

        public static IValueValidator<string> Instance => _instance.Value;

        private KvNrValidator() : base(StringValidatorBehavior.UpcaseTrimAllowEmpty, @"^[A-Z][0-9]{9}$")
        { }

        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            var err = base.GetErrorTextForNonEmpty(stringToValidate);
            if(err.IsNothing())
            {
                if (!CheckLuhnlike12(getLuhnableString(stringToValidate).ToCharArray()))
                {
                    return $"Prüfziffer der KV-Nr '{stringToValidate}' falsch.";
                }
            }

            return null;
        }

        private static string getLuhnableString(string stringToValidate) 
            => (stringToValidate[0] - 'A' + 1).ToString("D2") + stringToValidate.Substring(1);

        private static bool CheckLuhnlike12(char[] data)
        {
            int sum = 0;
            int len = data.Length;
            for (int i = 0; i < len-1; i++)
            {
                int add = (data[i] - 48) * (2 - (i+1) % 2);
                add -= add > 9 ? 9 : 0;
                sum += add;
            }

            return sum % 10 == (data.Last() - 48);
        }

    }
}
