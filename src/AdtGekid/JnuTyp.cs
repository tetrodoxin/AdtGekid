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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AdtGekid
{
    /// <summary>
    /// Typ zum Kodieren eines erweiterten Boolean Typs mit 'Ja', 'Nein' und 'Unbekannt',
    /// welcher auch einen Unset-Zustand erlaubt (quasi <c>null</c>).
    /// Ein Casting des Typs zu <see cref="string"/> gibt den Code des Zustands zurück,
    /// <see cref="ToString"/> hingegen die Erläuterung des Zustands.
    /// </summary>
    public struct JnuTyp : IXmlSerializable, IEquatable<JnuTyp>
    {                
        private static Lazy<JnuTyp> _lazyYes = new Lazy<JnuTyp>(() => new JnuTyp(1, "J", "Ja"));
        private static Lazy<JnuTyp> _lazyNo = new Lazy<JnuTyp>(() => new JnuTyp(2, "N", "Nein"));
        private static Lazy<JnuTyp> _lazyUnknown = new Lazy<JnuTyp>(() => new JnuTyp(3, "U", "Unbekannt"));
        private string _code;

        /// <summary>
        /// Code des aktuellen Zustands ("J", "N", "U" oder <c>null</c>)
        /// </summary>
        public string Code => _code;

        private int _id;

        private string _label;

        /// <summary>
        /// Instanz für JA (trifft zu)
        /// </summary>
        public static JnuTyp Ja => _lazyYes.Value;

        /// <summary>
        /// Instanz für NEIN (trifft nicht zu)
        /// </summary>
        public static JnuTyp Nein => _lazyNo.Value;


        /// <summary>
        /// Instanz für UNBEKANNT
        /// </summary>
        public static JnuTyp Unbekannt => _lazyUnknown.Value;

        private JnuTyp(int id, string code, string label) : this()
        {
            _id = id;
            _code = code;
            _label = label;
        }

        /// <summary>
        /// Vergleicht zwei <see cref="JnuTyp"/>-Werte.
        /// </summary>
        /// <param name="x">Erster Wert.</param>
        /// <param name="y">Vergleichswert.</param>
        /// <returns><c>true</c> wenn beide Werte gleich sind, sonst <c>false</c>.</returns>
        public static bool Equals(JnuTyp x, JnuTyp y) => x._id == y._id;

        /// <summary>
        /// Gibt den Hashcode für eine <see cref="JnuTyp"/> Instanz zurück.
        /// </summary>
        /// <param name="o">Die Instanz, deren Hashcode ermittelt werden soll.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public static int GetHashCode(JnuTyp o) => o._id;

        /// <summary>
        /// Wandelt eine <see cref="JnuTyp"/> Instanz in einen <see cref="string"/> um, 
        /// der den Code des Zustands enthält, oder <c>null</c>, falls kein Zustand gesetzt.
        /// </summary>
        /// <param name="obj">Die zu konvertierende Instanz.</param>
        /// <returns>
        /// Ein <see cref="string"/> Objekt mit dem Code des Zustands oder <c>null</c>.
        /// </returns>
        public static implicit operator string(JnuTyp obj) => obj._code;

        /// <summary>
        /// Implementiert den != Operator.
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator !=(JnuTyp x, JnuTyp y) => !Equals(x, y);

        /// <summary>
        /// Implementiert den == Operator.
        /// </summary>
        /// <param name="x">Linker Operand.</param>
        /// <param name="y">Rechter Operand.</param>
        /// <returns>Das Ergebnis des Vergleichs.</returns>
        public static bool operator ==(JnuTyp x, JnuTyp y) => Equals(x, y);

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is JnuTyp)
            {
                return Equals((JnuTyp)obj, this);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(JnuTyp other) => Equals(this, other);

        public override int GetHashCode() => GetHashCode(this);

        XmlSchema IXmlSerializable.GetSchema() => null;

        /// <summary>
        /// Realisiert das Deserialisieren des Datentyps via <see cref="XmlSerializer"/>
        /// </summary>
        /// <param name="reader">Der zu verwendende Reader.</param>
        /// <exception cref="FormatException">Erlaubte Zeichen sind 'J', 'N' und 'U'.</exception>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                var readCode = reader.ReadString();
                reader.ReadEndElement();
                if (string.IsNullOrEmpty(readCode))
                {
                    return;
                }
                else if (readCode.Length == 1)
                {
                    switch (readCode.ToUpper())
                    {
                        case "J":
                            absorb(Ja);
                            return;

                        case "N":
                            absorb(Nein);
                            return;

                        case "U":
                            absorb(Unbekannt);
                            return;
                    }
                }
            }
            else
            {
                reader.Read();
            }

            throw new FormatException("Erlaubte Zeichen sind 'J', 'N' und 'U'.");
        }

        /// <summary>
        /// Realisiert das Serialisieren des Datentyps via <see cref="XmlSerializer"/>
        /// </summary>
        /// <param name="writer">Der zu verwendende Writer.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (_id > 0 && !string.IsNullOrEmpty(_code))
            {
                writer.WriteString(_code);
            }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        public override string ToString() => _label ?? "Nicht gesetzt";

        private void absorb(JnuTyp o)
        {
            _id = o._id;
            _code = o._code;
            _label = o._label;
        }
    }
}
