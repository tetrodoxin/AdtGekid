using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdtGekid
{
    public static class EntityHelper
    {
        /// <summary>
        /// Erzwingt die Kleinschreibung von Strings.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Ein <see cref="string"/>, der nur Kleinschreibung enthält und dem führende und angehängte
        /// Leerzeichen entfernt wurden (Trim) bzw. <c>null</c>, falls der übergebene String leer oder ebenfalls <c>null</c> war.</returns>
        public static string EnforceTrimmedLowerCase(string str)
        {
            if (isNothing(str))
            {
                return null;
            }
            else
            {
                return str.Trim().ToLower();
            }
        }

        /// <summary>
        /// Erzwingt die Großschreibung von Strings.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Ein <see cref="string"/>, der nur Großschreibung enthält und dem führende und angehängte
        /// Leerzeichen entfernt wurden (Trim) bzw. <c>null</c>, falls der übergebene String leer oder ebenfalls <c>null</c> war.</returns>
        public static string EnforceTrimmedUpperCase(string str)
        {
            if (isNothing(str))
            {
                return null;
            }
            else
            {
                return str.Trim().ToUpper();
            }
        }

        /// <summary>
        /// Prüft einen String gegen eine Menge erlaubter Einzelzeichen-Codes.
        /// </summary>
        /// <param name="value">Der zu prüfende String.</param>
        /// <param name="allowEmpty">Gibt an, ob der übergebene String <c>null</c> oder leer sein darf.</param>
        /// <param name="allowedValues">Die erlaubten Codes.</param>
        /// <returns>Den übergebenen String, sofern dieser gültig war.</returns>
        /// <exception cref="System.ArgumentException">
        /// NULL oder leerer String nicht erlaubt.
        /// oder
        /// Übergebener Wert nicht in der Liste der erlaubten Zeichen.
        /// </exception>
        public static string ValidateStringOrThrow(string value, bool allowEmpty, params char[] allowedValues)
        {
            if (value == null)
            {
                return null;
            }

            if (isNothing(value))
            {
                if (allowEmpty)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException("NULL oder leerer String nicht erlaubt.");
                }
            }

            string newVal = value.Trim();
            if (newVal.Length != 1 || !allowedValues.Contains(newVal.[0])
            {
                throw new ArgumentException(String.Format("Unerlaubter Wert '{0}'", value));
            }

            return newVal;
        }

        /// <summary>
        /// Prüft einen String gegen eine Menge erlaubter Einzelzeichen-Codes,
        /// wobei leere Strings und <c>null</c> erlaubt sind.
        /// </summary>
        /// <param name="value">Der zu prüfende String.</param>
        /// <param name="allowedValues">Die erlaubten Codes.</param>
        /// <returns>Den übergebenen String, sofern dieser gültig war.</returns>
        /// <exception cref="System.ArgumentException">
        /// Übergebener Wert nicht in der Liste der erlaubten Zeichen.
        /// </exception>
        public static string ValidateStringOrThrow(string value, char[] allowedValues)
        {
            return ValidateStringOrThrow(value, false, allowedValues);
        }

        /// <summary>
        /// Prüft einen String gegen eine Menge erlaubter String-Codes.
        /// </summary>
        /// <param name="value">Der zu prüfende String.</param>
        /// <param name="allowEmpty">Gibt an, ob der übergebene String <c>null</c> oder leer sein darf.</param>
        /// <param name="allowedValues">Die erlaubten Codes. Davon darf keiner leer oder <c>null</c> sein.</param>
        /// <returns>Den übergebenen String, sofern dieser gültig war.</returns>
        /// <exception cref="System.ArgumentException">
        /// NULL oder leerer String nicht erlaubt.
        /// oder
        /// Übergebener Wert nicht in der Liste der erlaubten String-Codes.
        /// oder
        /// [allowedValues] enthielt einen leeren String oder <c>null</c>
        /// </exception>
        public static string ValidateStringOrThrow(string value, bool allowEmpty, params string[] allowedValues)
        {
            if (allowedValues.Any(p => string.IsNullOrEmpty(p)))
            {
                throw new ArgumentException("Leere String oder NULL nicht erlaubt.", "allowedValues");
            };

            if (value == null)
            {
                return null;
            }

            if (isNothing(value))
            {
                if (allowEmpty)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException("NULL oder leerer String nicht erlaubt.");
                }
            }

            string newVal = value.Trim();
            if (!allowedValues.Contains(newVal))
            {
                throw new ArgumentException(String.Format("Unerlaubter Wert '{0}'", value));
            }

            return newVal;
        }

        /// <summary>
        /// Prüft einen String gegen eine Menge erlaubter String-Codes,
        /// wobei leere Strings und <c>null</c> erlaubt sind.
        /// </summary>
        /// <param name="value">Der zu prüfende String.</param>
        /// <param name="allowedValues">Die erlaubten Codes. Davon darf keiner leer oder <c>null</c> sein.</param>
        /// <returns>Den übergebenen String, sofern dieser gültig war.</returns>
        /// <exception cref="System.ArgumentException">
        /// NULL oder leerer String nicht erlaubt.
        /// oder
        /// Übergebener Wert nicht in der Liste der erlaubten String-Codes.
        /// oder
        /// [allowedValues] enthielt einen leeren String oder <c>null</c>
        /// </exception>
        public static string ValidateStringOrThrow(string value, params string[] allowedValues)
        {
            return ValidateStringOrThrow(value, false, allowedValues);
        }

        /// <summary>
        /// Prüft einen String gegen eine Menge erlaubter String-Codes.
        /// </summary>
        /// <param name="value">Der zu prüfende String.</param>
        /// <param name="regexPattern">Das RegEx-Pattern als Validierungskriterium. Dieses wird niemals gegen leere Strings geprüft.</param>
        /// <param name="allowEmpty">Gibt an, ob der übergebene String <c>null</c> oder leer sein darf.</param>
        /// <returns>
        /// Den übergebenen String, sofern dieser gültig war.
        /// </returns>
        /// <exception cref="System.ArgumentException">NULL oder leerer String nicht erlaubt.
        /// oder
        /// Übergebener Wert nicht in der Liste der erlaubten String-Codes.
        /// oder
        /// [regexPattern] war leerer String, <c>null</c> oder enthielt ein ungültiges Pattern.
        /// </exception>
        public static string ValidateStringOrThrow(string value, string regexPattern, bool allowEmpty = false)
        {
            if (string.IsNullOrEmpty(regexPattern))
            {
                throw new ArgumentException("Leerer String oder NULL nicht erlaubt.", "regexPattern");
            };

            if (value == null)
            {
                return null;
            }

            var regex = new Regex(regexPattern);

            if (isNothing(value))
            {
                if (allowEmpty)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException("NULL oder leerer String nicht erlaubt.");
                }
            }

            string newVal = value.Trim().ToUpper();
            if (!regex.IsMatch(newVal))
            {
                throw new ArgumentException(String.Format("Unerlaubter Wert '{0}'", value));
            }

            return newVal;
        }

        private static bool isNothing(string str)
        {
            if(str == null)
            {
                return true;
            }

            str = str.Trim();
            return string.IsNullOrEmpty(str);
        }
    }
}