using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Xml.Serialization;

using System.Collections.ObjectModel;

namespace AdtGekid
{
    public static class EnumStaticHelpers
    {
        /// <summary>
        /// Versucht die angegebene Zeichenfolge in den entsprechenden Enumerations-Wert zu übersetzen
        /// und löst eine Exception aus, falls nicht möglich.
        /// Bei leeren Zeichenfolgen wird der Default (0) der Enumeration zurückgegeben, falls <paramref name="allowNullOrEmpty"/>
        /// <c>true</c> ist, leere Zeichenfolgen also erlaubt sind.
        /// !!ACHTUNG!!: Bei NUMERISCHEN Werten, die in eine Enumeration übersetzt werden sollen,
        /// sollte sicherheitshalber die dem zu SERIALISIERENDEN WERT ENTSPRECHENDE Nummer 
        /// hinter die jeweilige Enum-Konstante geschrieben werden,
        /// da in diesem Fall zuerst versucht wird, über die Nummer zu parsen.
        /// Damit ist gewährleistet, dass korrekt geparst wird.
        /// </summary>
        /// <example>
        /// <code>
        /// public enum Diagnosesicherung
        /// {
        ///     NotSpecified = 0,
        ///     [XmlEnum("1")]
        ///     Item1 = 1,
        ///     [XmlEnum("2")]
        ///     Item2 = 2
        ///     [XmlEnum("3")]
        ///     Item3 = 3,
        /// }
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        /// </code>
        /// </example>
        /// <typeparam name="TEnum">Der Typ der Enumeration</typeparam>
        /// <param name="value">Die Zeichenfolge</param>
        /// <param name="validatedAdtObject">Der Name des betreffenden Objekts des ADT-Datensatzes.</param>
        /// <param name="validatedAdtField">Der Name des betreffenden Felds des ADT-Datensatzes</param>
        /// <param name="allowNullOrEmpty">Gibt an ob leere oder <c>null</c>-Zeichenfolgen erlaubt sind oder nicht
        /// und wirft bei <code>false</code> eine <see cref="System.ArgumentException"/> falls ein Leerwert geparst werden soll
        /// </param>
        /// <param name="ignoreCase">Gibt an, ob Case sensitive geparsed werden soll oder nicht. 
        /// Bei Case sensitiven Werten wird der Wert nicht aufgelöst, wenn die Groß/Kleinschreibung nicht übereinstimmt.</param>
        /// <param name="tryDeeperParse">Gibt an, ob nach fehlerhaftem Parsen versucht werden 
        /// soll den Wert über das <see cref="XmlEnumAttribute"/> aufzulösen.
        /// Dies kann nötig werden, wenn die zu serialisierende Zeichenfolge vom Namen der Enumerations-Konstanten
        /// abweicht, da diese z.B. nicht Compiler-kompatibel ist, z.B. Zielgebiet-Wert einer Bestrahlung "1."
        /// </param>
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

                var attributes = ((TEnum)enVal).GetAttributes<TEnum, XmlEnumAttribute>();

                StringComparison strComp = ignoreCase
                                    ? StringComparison.OrdinalIgnoreCase
                                    : default(StringComparison);

                var attVal = attributes.Any() ? attributes[0].Name : string.Empty;

                if (value.Equals(attVal, strComp))
                    return (TEnum)enVal;                

            }
            throw new ArgumentException($"{validatedAdtObject}.{validatedAdtField} weist einen ungültigen Wert auf!");
        }
        


        public static Collection<string> AsStringCollection<TEnum>(this Collection<TEnum> col)
            where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            if (col == null)
                return null;

            var stringCol = new Collection<string>();

            foreach (var item in col)
            {
                stringCol.Add(item.ToString());
            }

            return stringCol;
        }
        
        public static TAttribute[] GetAttributes<TEnum, TAttribute>(this TEnum enumValue)
            where TEnum : struct
            where TAttribute : Attribute
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            return fieldInfo.GetCustomAttributes(typeof(TAttribute), false) as TAttribute[]; 
        }

        public static Collection<TEnum> TryParseAsEnumCollectionOrThrow<TEnum>(this Collection<string> col)
            where TEnum : struct
        {
            if (col == null)
                return null;

            ThrowIfNoEnumeration(typeof(TEnum));
           
            var enumCol = new Collection<TEnum>();
            foreach (var item in col)
            {
                enumCol.Add(item.TryParseAsEnumOrThrow<TEnum>());                
            }

            return enumCol;
        }       

        /// <summary>
        /// Übersetzt den angegebenen Enumerationswert in die zu serialisierende
        /// Zeichenfolge, die unter <see cref="XmlEnumAttribute.Name"/>
        /// über der Enumerationskonstante festgelegt ist.
        /// Dies kann nötig sein, wenn der Konstantenname der Enumeration 
        /// von der zu serialisierenden Zeichenfolge abweicht oder abweichen muss,
        /// falls die Zeichenfolge nicht Compiler-kompatibel ist.
        /// </summary>
        /// <typeparam name="TEnum">Der zu übersetzende Enumerationswert</typeparam>
        /// <returns></returns>
        public static string ToXmlEnumAttributeName<TEnum>(this TEnum enumValue)
            where TEnum : struct
        {
            ThrowIfNoEnumeration(typeof(TEnum));

            var attributes = enumValue.GetAttributes<TEnum, XmlEnumAttribute>();

            if (attributes == null || !attributes.Any())
                return enumValue.ToString();


            return attributes[0].Name;
        }


        /// <summary>
        /// Wirft eine <see cref="InvalidOperationException"/>
        /// wenn der angegebene Typ kein Enumerations-Typ ist.
        /// </summary>
        /// <param name="type"></param>
        public static void ThrowIfNoEnumeration(Type type)
        {
            if (!type.IsEnum)
                throw new InvalidOperationException("Der angegebene Typ ist keine Enumeration!");
        }
             
    }
}
