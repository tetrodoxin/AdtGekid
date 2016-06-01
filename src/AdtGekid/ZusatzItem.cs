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
using AdtGekid.Validation;
using System;
using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungZusatzitem", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class ZusatzItem
    {
        private string _art;
        private string _bemerkung;
        private string _wert;

        /// <summary>
        /// Art des Zusatzitems.
        /// </summary>
        /// <value>
        /// Erlaubt ist derzeit nur 'Untersuchungsanlass'
        /// </value>
        [XmlElement("Art", Order = 2)]
        public string Art
        {
            get
            {
                return _art;
            }
            set
            {
                if (!string.Equals(value, "Untersuchungsanlass", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Erlaubt ist derzeit nur 'Untersuchungsanlass'");
                _art = value;
            }
        }

        /// <summary>
        /// Anmerkung zum Zusatzitem
        /// </summary>
        [XmlElement("Bemerkung", Order = 4)]
        public string Bemerkung
        {
            get { return _bemerkung; }
            set { _bemerkung = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Datum des Zusatzitems bei Angabe der Art „Untersuchungsanlass“
        /// </summary>
        [XmlElement("Datum", Order = 1)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Wert des Zusatzitems bei Angabe der Art „Untersuchungsanlass“
        /// </summary>
        [XmlElement("Wert", Order = 3)]
        public string Wert
        {
            get { return _wert; }
            set { _wert = value.ValidateOrThrow("NTBOSDZPSU".ToCharArray()); }
        }
    }
}