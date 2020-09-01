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

#endregion license

using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace AdtGekid
{

    public enum DatumNuNonNumericValues
    {

        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Nein
        /// </summary>
        [XmlEnum("N")]
        N,

        /// <summary>
        /// Unbekannt
        /// </summary>
        [XmlEnum("U")]
        U,
    }


    /// <summary>
    /// Datentyp für DatumNU-Typen nach ADT_GEKID
    /// (z.B. für Datum Studienrekrutierung/Sozialdienstkontakt)
    /// Dient der geforderten Formatierung und der Vermeidung
    /// des Verwendens von <see cref="string"/> für Daten.
    /// </summary>
    /// <seealso cref="System.Xml.Serialization.IXmlSerializable" />
    /// <seealso cref="System.IEquatable{AdtGekid.DatumNuTyp}" />
    [Serializable()]
    [XmlType("Datum_NU_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    //public class DatumNuTyp : IXmlSerializable, IEquatable<DatumNuTyp>
    public class DatumNuTyp : IEquatable<DatumNuTyp>
    {
        private string _typeName = nameof(DatumNuTyp);
        private const string DateFormatString = "dd.MM.yyyy";
        private DateTime? _date;
        private DatumNuNonNumericValues? _nonNumericValue;


        /// <summary>
        /// Gibt an, ob das Datum einen Wert enthält.
        /// </summary>
        [XmlIgnore]
        public bool HasDateValue => _date.HasValue;

        [XmlElement("Datum", typeof(System.DateTime), DataType = "date")]
        [XmlElement("NU", typeof(DatumNuNonNumericValues))]
        public object Item
        {
            get
            {
                if (_date.HasValue)
                    return _date.Value;
                else if (_nonNumericValue.HasValue)
                    return _nonNumericValue.Value;

                return null;
            }
        }

        [XmlIgnore]
        public bool ItemSpecified => Item != null;

        //[XmlElement("Datum")]
        [XmlIgnore]
        public string Datum 
        { 
            get { return getDateString(); }        
        }

        [XmlIgnore]
        public DateTime? DateValue
        {
            get { return _date; }
            set { _date = value; }
        }

        [XmlIgnore]
        public string NonNumericValue 
        { 
            get { return _nonNumericValue?.ToString(); } 
            set
            {
                if (!value.IsNothing())
                {
                    _nonNumericValue = value.TryParseAsEnumOrThrow<DatumNuNonNumericValues>(_typeName, nameof(this.NonNumericValue));
                    _date = null;
                }
            } 
        }

        [XmlIgnore]
        public DatumNuNonNumericValues? NonNumericValueEnum
        {
            get { return _nonNumericValue; } 
            set { 
                _nonNumericValue = value;
                if (value != null)
                    _date = null;
            }
        }

        /// <summary>
        /// Gibt an, ob der erweiterte Wert serialisiert werden soll.
        /// Dies soll z.B. dann nicht geschehen, wenn das Datum vorhanden ist.
        /// </summary>
        [XmlIgnore]
        public bool ExtendedValueSpecified => _nonNumericValue.HasValue && !_date.HasValue;

        /// <summary>
        /// Erstellt eine leere Instanz von <see cref="DatumNuTyp"/>.
        /// </summary>
        public DatumNuTyp()
        {
            _date = null;
            _nonNumericValue = null;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumNuTyp"/> mit gegebenem Enumerations-Wert.
        /// </summary>
        /// <param name="date">Der zu verwendende Werts der Enumeration <see cref="DatumNuTyp"/></param>
        public DatumNuTyp(DatumNuNonNumericValues extendedValue) : this()
        {            
            _nonNumericValue = extendedValue;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumNuTyp"/> mit gegebenem Datum.
        /// </summary>
        /// <param name="date">Der zu verwendende Datumswert.</param>
        public DatumNuTyp(DateTime date) : this()
        {
            _date = date;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumNuTyp"/> mit gegebenem Datum.
        /// </summary>
        /// <param name="year">Das Jahr des Datums.</param>
        /// <param name="month">Der Monat des Datums</param>
        /// <param name="day">Der Tag des Datums</param>
        public DatumNuTyp(int year, int month, int day) 
            : this(new DateTime(year, month, day))
        {           
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumNuTyp" /> mit gegebenem Datum,
        /// welches als String übergeben und geparst wird.
        /// </summary>
        /// <param name="datumString">Der zu parsende Datums-String (Format <c>dd.MM.yyyy</c>).</param>
        public DatumNuTyp(string datumString) : this()
        {
            if (!string.IsNullOrEmpty(datumString))
            {
                setDateFromString(datumString, true);
            }
        }

        /// <summary>
        /// Vergleicht zwei Daten.
        /// </summary>
        /// <param name="x">Erstes Datum.</param>
        /// <param name="y">Datum, mit dem das erste verglichen werden soll.</param>
        /// <returns><c>true</c> falls beide Daten gleich sind, andernfalls <c>false</c>.</returns>
        public static bool Equals(DatumNuTyp x, DatumNuTyp y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if ((object)x == null || (object)y == null)
            {
                return false;
            }

            if (x._date.HasValue && y._date.HasValue)
            {
                return x._date.Value == y._date.Value;
            }
            else if (x._nonNumericValue.HasValue && y._nonNumericValue.HasValue)
            {
                return x._nonNumericValue == y._nonNumericValue;
            }
            else
            {
                return x._date.HasValue == y._date.HasValue;
            }
        }

        /// <summary>
        /// Gibt einen Hashcode für einen Datumswert zurück.
        /// </summary>
        /// <param name="o">Das Datum, dessen Hashwert ermittelt werden soll.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public static int GetHashCode(DatumNuTyp o)
        {
            if ((object)o != null && o._date.HasValue)
            {
                return o._date.Value.GetHashCode();
            }
            else if ((object)o != null && o._nonNumericValue.HasValue)
            {
                return o._nonNumericValue.Value.GetHashCode();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuTyp"/> nach <see cref="DateTime"/>.
        /// </summary>
        /// <param name="d">Zu konvertierendes Datum.</param>
        /// <returns>Das konvertierte Datum als <see cref="DateTime"/>.</returns>
        /// <exception cref="InvalidCastException">Falls das übergebene Datum <c>null</c> war.</exception>
        public static implicit operator DateTime(DatumNuTyp d)
        {
            if ((object)d != null && d._date.HasValue)
            {
                return d._date.Value;
            }
            else
            {
                throw new InvalidCastException("Kann Null-Datum nicht zu DateTime konvertieren. Eventuell DateTime? verwenden!");
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuTyp"/> nach <see cref="DatumNuNonNumericValues"/>.
        /// </summary>
        /// <param name="d">Zu konvertierender Wert.</param>
        /// <returns>Das konvertierte Wert als <see cref="DatumNuNonNumericValues"/>.</returns>
        /// <exception cref="InvalidCastException">Falls der übergebene Wert <c>null</c> war.</exception>
        public static implicit operator DatumNuNonNumericValues(DatumNuTyp d)
        {
            if ((object)d != null && d._date.HasValue)
            {
                return d._nonNumericValue.Value;
            }
            else
            {
                throw new InvalidCastException($"Kann Null nicht zu {nameof(DatumNuNonNumericValues)} konvertieren. Eventuell {nameof(DatumNuNonNumericValues)} verwenden!");
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuTyp"/> nach <see cref="System.Nullable{DateTime}"/>.
        /// </summary>
        /// <param name="d">Zu konvertierendes Datum.</param>
        /// <returns>Das konvertierte Datum als <see cref="DateTime?"/>,
        /// das auch <c>null</c> sein kann.</returns>
        public static implicit operator DateTime? (DatumNuTyp d) => d != null ? d._date : null;

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuTyp"/> nach <see cref="System.Nullable{DatumNuExtendedValues}"/>.
        /// </summary>
        /// <param name="d">Zu konvertierendes Wert</param>
        /// <returns>Das konvertierte Wert als <see cref="DatumNuNonNumericValues?"/>,
        /// das auch <c>null</c> sein kann.</returns>
        public static implicit operator DatumNuNonNumericValues?(DatumNuTyp d) => d != null ? d._nonNumericValue : null;

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuTyp"/> in <see cref="string"/>.
        /// </summary>
        /// <param name="obj">Zu konvertierendes Datum.</param>
        /// <returns>Das Datum als String (Format <c>dd.MM.yyyy</c>) oder <c>null</c>.</returns>
        public static implicit operator string(DatumNuTyp obj) 
            => obj != null ? obj.ToString() : null;
        

        /// <summary>
        /// Implizite Konvertierung von <see cref="DateTime"/> in <see cref="DatumNuTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierendes Datum.</param>
        /// <returns>Konvertiertes Datum als <see cref="DatumTyp"/>.</returns>
        public static implicit operator DatumNuTyp(DateTime date) => new DatumNuTyp(date);

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuNonNumericValues"/> in <see cref="DatumNuTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierender Wert.</param>
        /// <returns>Konvertierter Wert als <see cref="DatumNuNonNumericValues"/>.</returns>
        public static implicit operator DatumNuTyp(DatumNuNonNumericValues extendedValue) => new DatumNuTyp(extendedValue);

        /// <summary>
        /// Implizite Konvertierung von <see cref="DateTime?"/> in <see cref="DatumNuTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierendes Datum, das auch <c>null</c> sein kann.</param>
        /// <returns>Konvertiertes Datum als <see cref="DatumTyp"/> oder <c>null</c>.</returns>
        public static implicit operator DatumNuTyp(DateTime? date)
        {
            if (date.HasValue)
            {
                return new DatumNuTyp(date.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumNuNonNumericValues?"/> in <see cref="DatumNuTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierender Wert, das auch <c>null</c> sein kann.</param>
        /// <returns>Konvertierter Wert als <see cref="DatumNuNonNumericValues"/> oder <c>null</c>.</returns>
        public static implicit operator DatumNuTyp(DatumNuNonNumericValues? extendedValue)
        {
            if (extendedValue.HasValue)
            {
                return new DatumNuTyp(extendedValue.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="string"/> in <see cref="DatumNuTyp"/>,
        /// wobei der <c>string</c> im Format <c>dd.MM.yyyy</c> geparst wird.
        /// </summary>
        /// <param name="datumString">Zu parsender/konvertierender Datumsstring.</param>
        /// <returns>Das Ergebnis des Parsens als <see cref="DatumNuTyp"/> oder <c>null</c>.</returns>
        /// <exception cref="FormatException">Falls das Format des Datumsstrings nicht erkannt werden konnte.</exception>
        public static implicit operator DatumNuTyp(string datumString)
        {
            if (!string.IsNullOrEmpty(datumString))
            {
                return new DatumNuTyp(datumString);
            }

            return null;
        }        

        /// <summary>
        /// Implementiert den != Operator
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator !=(DatumNuTyp x, DatumNuTyp y) => !Equals(x, y);

        /// <summary>
        /// Implementiert den == Operator
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator ==(DatumNuTyp x, DatumNuTyp y) => Equals(x, y);

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => Equals(obj as DatumNuTyp, this);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(DatumNuTyp other) => Equals(this, other);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => GetHashCode(this);

        //XmlSchema IXmlSerializable.GetSchema() => null;

        /// <summary>
        /// Realisiert das Deserialisieren dieses Datentyps mittels <see cref="XmlSerializer"/>.
        /// </summary>
        /// <param name="reader">Der zu verwendende Reader.</param>
        //void IXmlSerializable.ReadXml(XmlReader reader)
        //{
        //    var isEmpty = reader.IsEmptyElement;
        //    if (!isEmpty)
        //    {
        //        var datumString = reader.ReadString();
        //        reader.ReadEndElement();
        //        if (string.IsNullOrEmpty(datumString))
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            setDateFromString(datumString, true);
        //        }
        //    }
        //    else
        //    {
        //        reader.Read();
        //    }
        //}

        /// <summary>
        /// Realisiert das Serialisieren des Datentyps mittels des <see cref="XmlSerializer"/>
        /// </summary>
        /// <param name="writer">Der zu verwendende Writer.</param>
        //void IXmlSerializable.WriteXml(XmlWriter writer)
        //{
        //    if (_date.HasValue)
        //    {
        //        writer.WriteString(getDateString());
        //    }
        //}

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            if (_date.HasValue)
            {
                return getDateString();
            }
            else if (_nonNumericValue.HasValue)
            {
                return _nonNumericValue.ToString();
            }
            else
            {
                return null;
            }
        }

        private static void throwFormatException(string str = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new FormatException("Nicht unterstütztes Datumsformat. Erwartet [dd.MM.yyyy]");
            }
            else
            {
                throw new FormatException($"Nicht unterstütztes Datumsformat. Erwartet [dd.MM.yyyy]. Bekommen '{str}'");
            }
        }

        private bool returnNullOrThrow(bool throwExpections, string datumString)
        {
            _date = null;            

            if (throwExpections) 
                throwFormatException(datumString);

            return false;
        }

        private bool setDateFromString(string datumString, bool throwExceptions)
        {
            if (datumString == null)
            {
                _date = null;
               
                return true;
            }

            datumString = datumString.Trim();
            var parts = datumString.Split('.');

            if (parts.Length != 3) 
                return returnNullOrThrow(throwExceptions, datumString);

            var nums = parts.Select(p => parsePositiveInt(p))
                .Where(p => p >= 0)
                .ToList();

            if (nums.Count != 3) 
                return returnNullOrThrow(throwExceptions, datumString);

            if (nums[2] == 0) 
                return returnNullOrThrow(throwExceptions, datumString);

            try
            {
                _date = new DateTime(nums[2], Math.Max(nums[1], 1), Math.Max(nums[0], 1));               
                return true;
            }
            catch
            {
                return returnNullOrThrow(throwExceptions, datumString);
            }
        }

        private static int parsePositiveInt(string str)
        {
            int r = 0;
            if (int.TryParse(str, out r))
            {
                return r;
            }
            else
            {
                return -1;
            }
        }

        private string getDateString()
        {
           return _date.Value.ToString(DateFormatString);           
        }
    }
}