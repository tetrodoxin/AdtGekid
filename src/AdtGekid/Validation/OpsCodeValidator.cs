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
    /// Stringvalidierer für OPS Codes für Operationen.
    /// </summary>
    public class OpsCodeValidator : StringValidatorByRegex
    {
        private static readonly Lazy<IValueValidator<string>> _instance = new Lazy<IValueValidator<string>>(() => new OpsCodeValidator());

        public static IValueValidator<string> Instance => _instance.Value;

        private OpsCodeValidator() : base(StringValidatorBehavior.LowcaseTrimAllowEmpty, @"^5\-[0-9]{2}[0-9a-z]?(\.[0-9a-z][0-9a-z]?)?$")
        { }

        private OpsCodeValidator(string validatedAdtObject, string validatedAdtField) : this()
        {
            base.ValidateAdtObject = validatedAdtObject;
            base.ValidatedAdtField = validatedAdtField;
        }

        public static OpsCodeValidator CreateInstance(string validatedAdtObject, string validatedAdtField)
        {
            return new OpsCodeValidator(validatedAdtObject, validatedAdtField);
        }


        protected override string GetErrorTextForNonEmpty(string stringToValidate)
        {
            if(stringToValidate.Length > 8)
            {
                return "OPS-Codes dürfen maximal 8 Zeichen lang sein.";
            }

            return base.GetErrorTextForNonEmpty(stringToValidate);
        }
    }
}
