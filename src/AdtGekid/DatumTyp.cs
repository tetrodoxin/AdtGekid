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

using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AdtGekid
{
    /// <summary>
    /// Datentyp für Datumsangaben nach ADT_GEKID.
    /// Dient der geforderten Formatierung und der Vermeidung
    /// des Verwendens von <see cref="string"/> für Daten.
    /// </summary>
    /// <seealso cref="System.Xml.Serialization.IXmlSerializable" />
    /// <seealso cref="System.IEquatable{AdtGekid.DatumTyp}" />
    public class DatumTyp : IXmlSerializable, IEquatable<DatumTyp>
    {
        private const string DateFormatString = "dd.MM.yyyy";
        private DateTime? _date;

        private bool _monthUnknown = false;
        private bool _dayUnknown = false;

        /// <summary>
        /// Gibt an, ob das Datum einen Wert enthält.
        /// </summary>
        public bool HasValue => _date.HasValue;

        /// <summary>
        /// Gibt an, ob der Monat unbekannt ist.
        /// </summary>
        public bool MonthUnknown => _monthUnknown;

        /// <summary>
        /// Gibt an, ob der Tag unbekannt ist.
        /// </summary>
        public bool DayUnknown => _dayUnknown;

        /// <summary>
        /// Erstellt eine leere Instanz von <see cref="DatumTyp"/>.
        /// </summary>
        public DatumTyp()
        {
            _date = null;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumTyp"/> mit gegebenem Datum.
        /// </summary>
        /// <param name="date">Der zu verwendende Datumswert.</param>
        public DatumTyp(DateTime date) : this()
        {
            _date = date;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumTyp"/> mit gegebenem Datum.
        /// </summary>
        /// <param name="date">Der zu verwendende Datumswert.</param>
        public DatumTyp(DateTime date, bool monthUnknown, bool dayUnknown) : this()
        {
            _date = date;
            _monthUnknown = monthUnknown;
            _dayUnknown = dayUnknown;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumTyp"/> mit gegebenem Datum.
        /// </summary>
        /// <param name="year">Das Jahr des Datums.</param>
        /// <param name="month">Der Monat des Datums (0 für unbekannt).</param>
        /// <param name="day">Der Tag des Datums (0 für geschätzt).</param>
        public DatumTyp(int year, int month, int day) : this(new DateTime(year, Math.Max(1, month), Math.Max(1, day)))
        {
            _monthUnknown = month == 0;
            _dayUnknown = day == 0;
        }

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="DatumTyp" /> mit gegebenem Datum,
        /// welches als String übergeben und geparst wird.
        /// </summary>
        /// <param name="datumString">Der zu parsende Datums-String (Format <c>dd.MM.yyyy</c>).</param>
        public DatumTyp(string datumString) : this()
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
        public static bool Equals(DatumTyp x, DatumTyp y)
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
                return x._date.Value == y._date.Value
                    && x._dayUnknown == y._dayUnknown
                    && x._monthUnknown == y._monthUnknown;
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
        public static int GetHashCode(DatumTyp o)
        {
            if ((object)o != null && o._date.HasValue)
            {
                return o._date.Value.GetHashCode();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumTyp"/> nach <see cref="DateTime"/>.
        /// </summary>
        /// <param name="d">Zu konvertierendes Datum.</param>
        /// <returns>Das konvertierte Datum als <see cref="DateTime"/>.</returns>
        /// <exception cref="InvalidCastException">Falls das übergebene Datum <c>null</c> war.</exception>
        public static implicit operator DateTime(DatumTyp d)
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
        /// Implizite Konvertierung von <see cref="DatumTyp"/> nach <see cref="System.Nullable{DateTime}"/>.
        /// </summary>
        /// <param name="d">Zu konvertierendes Datum.</param>
        /// <returns>Das konvertierte Datum als <see cref="DateTime?"/>,
        /// das auch <c>null</c> sein kann.</returns>
        public static implicit operator DateTime? (DatumTyp d) => d != null ? d._date : null;

        /// <summary>
        /// Implizite Konvertierung von <see cref="DatumTyp"/> in <see cref="string"/>.
        /// </summary>
        /// <param name="obj">Zu konvertierendes Datum.</param>
        /// <returns>Das Datum als String (Format <c>dd.MM.yyyy</c>) oder <c>null</c>.</returns>
        public static implicit operator string(DatumTyp obj) => obj != null ? obj.ToString() : null;

        /// <summary>
        /// Implizite Konvertierung von <see cref="DateTime"/> in <see cref="DatumTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierendes Datum.</param>
        /// <returns>Konvertiertes Datum als <see cref="DatumTyp"/>.</returns>
        public static implicit operator DatumTyp(DateTime date) => new DatumTyp(date);

        /// <summary>
        /// Implizite Konvertierung von <see cref="DateTime?"/> in <see cref="DatumTyp"/>.
        /// </summary>
        /// <param name="date">Zu konvertierendes Datum, das auch <c>null</c> sein kann.</param>
        /// <returns>Konvertiertes Datum als <see cref="DatumTyp"/> oder <c>null</c>.</returns>
        public static implicit operator DatumTyp(DateTime? date)
        {
            if (date.HasValue)
            {
                return new DatumTyp(date.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Implizite Konvertierung von <see cref="string"/> in <see cref="DatumTyp"/>,
        /// wobei der <c>string</c> im Format <c>dd.MM.yyyy</c> geparst wird.
        /// </summary>
        /// <param name="datumString">Zu parsender/konvertierender Datumsstring.</param>
        /// <returns>Das Ergebnis des Parsens als <see cref="DatumTyp"/> oder <c>null</c>.</returns>
        /// <exception cref="FormatException">Falls das Format des Datumsstrings nicht erkannt werden konnte.</exception>
        public static implicit operator DatumTyp(string datumString)
        {
            if (!string.IsNullOrEmpty(datumString))
            {
                return new DatumTyp(datumString);
            }

            return null;
        }

        /// <summary>
        /// Implementiert den != Operator
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator !=(DatumTyp x, DatumTyp y) => !Equals(x, y);

        /// <summary>
        /// Implementiert den == Operator
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator ==(DatumTyp x, DatumTyp y) => Equals(x, y);

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => Equals(obj as DatumTyp, this);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(DatumTyp other) => Equals(this, other);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => GetHashCode(this);

        XmlSchema IXmlSerializable.GetSchema() => null;

        /// <summary>
        /// Realisiert das Deserialisieren dieses Datentyps mittels <see cref="XmlSerializer"/>.
        /// </summary>
        /// <param name="reader">Der zu verwendende Reader.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            var isEmpty = reader.IsEmptyElement;
            if (!isEmpty)
            {
                var datumString = reader.ReadString();
                reader.ReadEndElement();
                if (string.IsNullOrEmpty(datumString))
                {
                    return;
                }
                else
                {
                    setDateFromString(datumString, true);
                }
            }
            else
            {
                reader.Read();
            }
        }

        /// <summary>
        /// Realisiert das Serialisieren des Datentyps mittels des <see cref="XmlSerializer"/>
        /// </summary>
        /// <param name="writer">Der zu verwendende Writer.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (_date.HasValue)
            {
                writer.WriteString(getDateString());
            }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            if (_date.HasValue)
            {
                return getDateString();
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
            _dayUnknown = false;
            _monthUnknown = false;

            if (throwExpections) throwFormatException(datumString);
            return false;
        }

        private bool setDateFromString(string datumString, bool throwExceptions)
        {
            if (datumString == null)
            {
                _date = null;
                _dayUnknown = false;
                _monthUnknown = false;
                return true;
            }

            datumString = datumString.Trim();
            var parts = datumString.Split('.');

            if (parts.Length != 3) return returnNullOrThrow(throwExceptions, datumString);

            var nums = parts.Select(p => parsePositiveInt(p))
                .Where(p => p >= 0)
                .ToList();

            if (nums.Count != 3) return returnNullOrThrow(throwExceptions, datumString);
            if (nums[2] == 0) return returnNullOrThrow(throwExceptions, datumString);

            try
            {
                _date = new DateTime(nums[2], Math.Max(nums[1], 1), Math.Max(nums[0], 1));
                _monthUnknown = nums[1] == 0;
                _dayUnknown = nums[0] == 0;
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
            if (!_dayUnknown && !_monthUnknown)
            {
                return _date.Value.ToString(DateFormatString);
            }
            else
            {
                var d = _date.Value;
                return $"{(_dayUnknown ? 0 : d.Day):D2}.{(_monthUnknown ? 0 : d.Month):D2}.{d.Year:D4}";
            }
        }
    }
}