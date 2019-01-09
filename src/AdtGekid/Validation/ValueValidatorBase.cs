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
    /// Basisklasse für Wert-Validierer.
    /// </summary>
    /// <typeparam name="T">Datentyp des zu validierenden Werts.</typeparam>
    public abstract class ValueValidatorBase<T> : IValueValidator<T>
    {
        public string ValidateAdtObject { get; protected set; }
        public string ValidatedAdtField { get; protected set; }

        public T GetValidatedValueOrThrow(T value)
        {
            return GetValidatedValueOrThrow(value, this.ValidateAdtObject, this.ValidatedAdtField);
        }


        public T GetValidatedValueOrThrow(T value, string validatedAdtObject, string validatedAdtField)
        {           
            var preparedValue = GetPreparedValue(value);
            string error = GetErrorText(preparedValue);
            if (string.IsNullOrEmpty(error))
            {
                return preparedValue;
            }
            else
            {
                throw new ValidationArgumentException (error, validatedAdtObject, validatedAdtField);
            }
        }

        /// <summary>
        /// Prüft den Wert und gibt im Fehlerfall einen Text zurück,
        /// der als Meldung einer auszulösenden Maßnahme übergeben wird.
        /// </summary>
        /// <param name="valueToValidate">Der zu validierende Wert.</param>
        /// <returns><c>null</c>, falls der übergebene Wert korrekt ist,
        /// ansonsten einen Fehlertext.</returns>
        protected virtual string GetErrorText(T valueToValidate) => null;

        /// <summary>
        /// Wird in überschreibenden Klassen zum vorangehenden Anpassen des zu prüfenden
        /// Werts verwendet, bspw. Trimming bei Strings.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        protected virtual T GetPreparedValue(T value) => value;


    }
}
