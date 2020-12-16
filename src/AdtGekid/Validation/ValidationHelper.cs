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

namespace AdtGekid.Validation
{
    /// <summary>
    /// Hilfsklasse für Validierungen von einzelnen Werten.
    /// </summary>
    public static class ValidationHelper
    {
        private static Regex alphanumRegex = new Regex("^[a-zA-Z0-9]*$");
        private static Regex alphaRegex = new Regex("^[a-zA-Z]*$");            
        

        /// <summary>
        /// Validiert einen Wert mit einem gegebenen Validierer. 
        /// Falls die Validierung fehlschlägt wird eine Exception ausgelöst.
        /// </summary>
        /// <typeparam name="T">Datentyp des zu validierenden Werts.</typeparam>
        /// <param name="valueToValidate">Zu validierender Wert.</param>
        /// <param name="validator">Der Validierer, der verwendet werden soll.</param>
        /// <returns>Der (ggf. angepasste/veränderte/korrigierte) Wert, welcher übergeben wurde.</returns>
        /// <exception cref="ArgumentNullException">Falls <c>validator</c> gleich <c>null</c> ist.</exception>
        public static T ValidateOrThrow<T>(this T valueToValidate, IValueValidator<T> validator, string validatedAdtObject = null, string validatedAdtField = null)
        {
            if(validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return validator.GetValidatedValueOrThrow(valueToValidate, validatedAdtObject, validatedAdtField);
        }

        /// <summary>
        /// Validiert einen String mit einem gegebenen Validierer. 
        /// Falls die Validierung fehlschlägt wird eine Exception ausgelöst.
        /// </summary>
        /// <param name="stringToValidate">Zu validierender String.</param>
        /// <param name="validator">Der Validierer, der verwendet werden soll.</param>
        /// <returns>Der (ggf. angepasste/veränderte/korrigierte) Wert, welcher übergeben wurde.</returns>
        /// <exception cref="ArgumentNullException">Falls <c>validator</c> gleich <c>null</c> ist.</exception>
        public static string ValidateOrThrow(this string stringToValidate, IValueValidator<string> validator, string validatedAdtObject = null, string validatedAdtField = null)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return validator.GetValidatedValueOrThrow(stringToValidate, validatedAdtObject, validatedAdtField);
        }

        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="behavior">Das Verhalten des Stringvalidierers dabei.</param>
        /// <param name="allowedChars">Die zulässigen Werte.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, StringValidatorBehavior behavior, char[] allowedChars, string validatedAdtObject = null, string validatedAdtField = null)
        {
            var validator = new StringValidatorByChars(behavior, allowedChars, 0);
            return validator.GetValidatedValueOrThrow(value, validatedAdtObject, validatedAdtField);
        }

        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück,
        /// wobei <see cref="StringValidatorBehavior.UpcaseTrimAllowEmpty"/> verwendet wird.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="allowedChars">Die zulässigen Werte.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, char[] allowedChars, string validatedAdtObject = null, string validatedAdtField = null)
            => ValidateOrThrow(value, StringValidatorBehavior.UpcaseTrimAllowEmpty, allowedChars, validatedAdtObject, validatedAdtField);

        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="behavior">Das Verhalten des Stringvalidierers dabei.</param>
        /// <param name="allowedStrings">Die zulässigen Werte.</param>
        /// <param name="maxLength">Die maximale Länge des Strings oder 0, falls unbegrenzt.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, StringValidatorBehavior behavior, string[] allowedStrings, int maxLength = 0, string validatedAdtObject = null, string validatedAdtField = null)
        {
            var validator = new StringValidatorByStrings(behavior, allowedStrings, maxLength);
            return validator.GetValidatedValueOrThrow(value, validatedAdtObject, validatedAdtField);
        }

        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück,
        /// wobei <see cref="StringValidatorBehavior.UpcaseTrimAllowEmpty"/> verwendet wird.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="allowedStrings">Die zulässigen Werte.</param>
        /// <param name="maxLength">Die maximale Länge des Strings oder 0, falls unbegrenzt.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, string[] allowedStrings, int maxLength = 0, string validatedAdtObject = null, string validatedAdtField = null)
            => ValidateOrThrow(value, StringValidatorBehavior.UpcaseTrimAllowEmpty, allowedStrings, maxLength, validatedAdtObject, validatedAdtField);

        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="behavior">Das Verhalten des Stringvalidierers dabei.</param>
        /// <param name="regexPattern">Das RegEx Pattern für zulässige Werte.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, StringValidatorBehavior behavior, string regexPattern, string validatedAdtObject = null, string validatedAdtField = null)
        {
            var validator = new StringValidatorByRegex(behavior, regexPattern);
            return validator.GetValidatedValueOrThrow(value, validatedAdtObject, validatedAdtField);
        }

        /// <summary>
        /// Prüft, ob ein String ausschließlich alphanumerische Zeichen (also auch keine Leerzeichen) enthält
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="maxLength">Die maximal zulässige Länge des Strings oder <c>0</c>, falls unbegrenzt.</param>
        /// <returns>Den übergebenen String, falls dieser korrekt validiert wurde, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateAlphanumericalOrThrow(this string value, int maxLength = 0, string validatedAdtObject = null, string validatedAdtField = null)
        {
            if(!string.IsNullOrEmpty(value))
            {
                if (maxLength > 0 && value.Length > maxLength)
                {                                 
                    throw new ValidationArgumentException($"Feld erlaubt maximal {maxLength} Zeichen.", validatedAdtObject, validatedAdtField);
                }

                if(alphanumRegex.IsMatch(value) == false)
                {                    
                    throw new ValidationArgumentException($"Ungültiger Wert {value}", validatedAdtObject, validatedAdtField);
                }
            }

            return value;
        }

        /// <summary>
        /// Prüft, ob ein String ausschließlich Zeichen der Alphapets (also auch keine Zahlen, Leerzeichen) enthält
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="maxLength">Die maximal zulässige Länge des Strings oder <c>0</c>, falls unbegrenzt.</param>
        /// <returns>Den übergebenen String, falls dieser korrekt validiert wurde, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateAlphaCharsOnlyOrThrow(this string value, int maxLength = 0, string validatedAdtObject = null, string validatedAdtField = null)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (maxLength > 0 && value.Length > maxLength)
                {
                    throw new ValidationArgumentException($"Feld erlaubt maximal {maxLength} Zeichen.", validatedAdtObject, validatedAdtField);
                }

                if (alphaRegex.IsMatch(value) == false)
                {
                    throw new ValidationArgumentException($"Ungültiger Wert {value}", validatedAdtObject, validatedAdtField);
                }
            }

            return value;
        }

        /// <summary>
        /// Prüft, ob ein String die maximal zulässige Länge überschreitet.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="maxLength">Die maximal zulässige Länge des Strings.</param>
        /// <returns>Den übergebenen String, falls dieser korrekt validiert wurde, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateMaxLength(this string value, int maxLength, string validatedAdtObject = null, string validatedAdtField = null)
        {

            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > maxLength)
                {
                    throw new ValidationArgumentException($"Feld erlaubt maximal {maxLength} Zeichen.", validatedAdtObject, validatedAdtField);
                }
            }

            return value;
        }

        /// <summary>
        /// Prüft, ob ein Stringwert weder leer noch <c>null</c> ist.
        /// </summary>
        /// <param name="value">Zu prüfender Stringwert.</param>
        /// <returns>Der übergebene String.</returns>
        /// <exception cref="ArgumentException">Falls der übergebene String leer oder <c>null</c> war.</exception>
        public static string ValidateNeitherNullNorEmpty(this string value, string validatedAdtObject = null, string validatedAdtField = null)
        {            
            if (string.IsNullOrEmpty(value))
            {
                throw new ValidationArgumentException("NULL oder leere Strings sind nicht zulässig.", validatedAdtObject, validatedAdtField);
            }

            return value;
        }


        


        /// <summary>
        /// Prüft einen String und gibt im Erfolgsfall die gültige (und evt. veränderte) Form zurück,
        /// wobei <see cref="StringValidatorBehavior.UpcaseTrimAllowEmpty"/> verwendet wird.
        /// </summary>
        /// <param name="value">Der zu validierende Wert.</param>
        /// <param name="regexPattern">Das RegEx Pattern für zulässige Werte.</param>
        /// <returns>Einen String in korrekter Form und Schreibweise, andernfalls wird
        /// eine Exception ausgelöst..</returns>
        public static string ValidateOrThrow(this string value, string regexPattern, string validatedAdtObject = null, string validatedAdtField = null)
            => ValidateOrThrow(value, StringValidatorBehavior.UpcaseTrimAllowEmpty, regexPattern, validatedAdtObject, validatedAdtField);


        /// <summary>
        /// Prüft ob ein <see cref="int"/>-Wert innerhalb des angegebenen Bereichs ist,
        /// wobei <paramref name="minInclusive"/> und <paramref name="maxInclusive"/> eingeschlossen werden
        /// </summary>
        /// <param name="val">Der zu prüfende Wert</param>
        /// <param name="minInclusive">Der Startwert</param>
        /// <param name="maxInclusive">Der Maximalwert</param>        
        /// <returns>Den Wert, wenn er innerhalb des angegebenen Bereichs liegt</returns>
        public static int BetweenOrThrow(this int val, int minInclusive, int maxInclusive)
        {            
            if (!(val.CompareTo(minInclusive) >= 0 && val.CompareTo(maxInclusive) <= 0))
                 throw new ValidationArgumentException($"Der Wert {val} liegt außerhalb des angegebenen Bereichs von {minInclusive} und {maxInclusive}");

            return val;
        }

        /// <summary>
        /// Prüft ob ein <see cref="int"/>-Wert innerhalb des angegebenen Bereichs ist,
        /// wobei <paramref name="min"/> und <paramref name="max"/> eingeschlossen werden
        /// </summary>
        /// <param name="val">Der zu prüfende Wert</param>
        /// <param name="min">Der Startwert</param>
        /// <param name="max">Der Maximalwert</param>        
        /// <returns>Den Wert, wenn er innerhalb des angegebenen Bereichs liegt</returns>
        public static decimal BetweenOrThrow(this decimal val, decimal min, decimal max)
        {
            if (!(val.CompareTo(min) >= 0 && val.CompareTo(max) <= 0))
                throw new ValidationArgumentException($"Der Wert {val} liegt außerhalb des angegebenen Bereichs von {min} und {max}");

            return val;
        }

        /// <summary>
        /// Prüft ob ein Datum nicht in der Zukunft liegt
        /// </summary>
        /// <param name="date">Der zu prüfende Datumswert</param>
        /// <returns>Das übergebende Datum, andernfalls wird eine <see cref="ValidationArgumentException"/> ausgelöst.</returns>
        public static DatumTyp ValidateAintInFutureOrThrow(this DatumTyp date, string validatedAdtObject = null, string validatedAdtField = null)
        {
            if (!date.AintInFuture())
            {
                throw new ValidationArgumentException("Das Datum darf nicht in der Zukunft liegen!", validatedAdtObject, validatedAdtField);
            }
            return date;
        }

        /// <summary>
        /// Prüft, dass das Datum nicht in der Zukunft liegt.
        /// </summary>
        /// <param name="date">Das zu prüfende Datum.</param>
        /// <returns>
        /// <c>true</c>, falls das Datum nicht in der Zukunft liegt.
        /// </returns>
        public static bool AintInFuture(this DatumTyp date)
        {
            return !date.HasValue || date <= new DatumTyp(DateTime.Today);
        }
    }
}