using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Xml.Serialization;

namespace AdtGekid
{
    public static class EnumHelper
    {
        /// <summary>
        /// Versucht die angegebene Zeichenfolge in den entsprechenden Enumerations-Wert zu übersetzen
        /// und löst eine Exception aus, falls nicht möglich.
        /// Bei leeren Zeichenfolgen wird der Default der Enumeration zurückgegeben, falls <paramref name="allowNullOrEmpty"/>
        /// <c>true</c> ist, leere Zeichenfolgen also erlaubt sind.
        /// </summary>
        /// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
        /// <param name="value">Die Zeichenfolge</param>
        /// <param name="validatedAdtObject">Der Name des betreffenden Objekts des ADT-Datensatzes.</param>
        /// <param name="validatedAdtField">Der Name des betreffenden Felds des ADT-Datensatzes</param>
        /// <param name="allowNullOrEmpty">Gibt an ob leere oder <c>null</c>-Zeichenfolgen erlaubt sind oder nicht</param>
        /// <param name="ignoreCase">Gibt an, ob Case sensitive geparsed werden soll oder nicht.</param>
        /// <param name="tryDeeperParse">Gibt an, ob bei fehlerhaftem Parsen versucht werden soll den Wert über das <see cref="XmlEnumAttribute"/> aufzulösen.</param>
        /// <returns>
        /// Den entsprechenden Enumerations-Wert falls dieser existiert, 
        /// andernfalls wird eine <see cref="System.ArgumentException"/> ausgelöst, wenn
        /// kein Leerwert angegeben wurde und <paramref name="allowNullOrEmpty"/><c>false</c>ist
        /// </returns>
        public static TEnum TryParseAsEnumOrThrow<TEnum>(this string value, string validatedAdtObject = null, string validatedAdtField = null
            , bool allowNullOrEmpty = true, bool ignoreCase = true, bool tryDeeperParse = true)
            where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            if (value.IsNothing() && !allowNullOrEmpty)
                throw new ArgumentException($"{validatedAdtObject}.{validatedAdtField} kann nicht null oder leer sein!");
         
            TEnum enumValue;
            var parsed = Enum.TryParse<TEnum>(value, ignoreCase, out enumValue);

            // Parsing fehlgeschlagen oder geparster Wert ist nicht als Enumerations-Konstante definiert
            if ((!parsed && !value.IsNothing()) || ! Enum.IsDefined(typeof(TEnum),enumValue))
            {
                if (tryDeeperParse)
                {
                    return TryParseEnumByXmlEnumAttributeOrThrow<TEnum>(value, validatedAdtObject, validatedAdtField, ignoreCase);
                }                
                else 
                {
                    throw new ArgumentException($"{validatedAdtObject}.{validatedAdtField} weist einen ungültigen Wert auf!");
                }
            }               
            return enumValue;
        }

        public static TEnum TryParseEnumByXmlEnumAttributeOrThrow<TEnum>(this string value, string validatedAdtObject = null, string validatedAdtField = null, bool ignoreCase = true)
         where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            foreach (var enVal in Enum.GetValues(typeof(TEnum)))
            {
                Type type = enVal.GetType();
                FieldInfo fieldInfo = type.GetField(enVal.ToString());
                var attributes = fieldInfo.GetCustomAttributes(
                    typeof(XmlEnumAttribute), false) as XmlEnumAttribute[];

                StringComparison strComp = ignoreCase
                                    ? StringComparison.OrdinalIgnoreCase
                                    : default(StringComparison);

                var attVal = attributes.Any() ? attributes[0].Name : string.Empty;

                if (value.Equals(attVal, strComp))
                    return (TEnum)enVal;                

            }
            throw new ArgumentException($"{validatedAdtObject}.{validatedAdtField} weist einen ungültigen Wert auf!");
        }


        public static IEnumerable<string> AsStringEnumerable<TEnum>(this System.Collections.ObjectModel.Collection<TEnum> col)
            where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            foreach (var item in col)
            {
                yield return item.ToString();
            }
        }

        public static IEnumerable<TEnum> TryParseAsEnumCollectionOrThrow<TEnum>(this System.Collections.ObjectModel.Collection<string> col)
            where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            foreach (var item in col)
            {
                yield return item.TryParseAsEnumOrThrow<TEnum>();
            }
        }

        public static void ThrowIfNoEnumeration(Type type)
        {
            if (!type.IsEnum)
                throw new InvalidOperationException("Der angegebene Typ ist keine Enumeration!");
        }
             
    }
}
