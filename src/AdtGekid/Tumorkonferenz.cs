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
using System.Linq;
using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungTumorkonferenz", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Tumorkonferenz
    {
        private static string[] _allowedTypes = new string[] { "praeth","postop","postth" };

        private string typ;
        private string _anmerkung;
        private string _id;

        private string _typeName = typeof(Tumorkonferenz).Name;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 3)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.Anmerkung)); }
        }

        /// <summary>
        /// Datum der Durchführung der Tumorkonferenz
        /// </summary>
        [XmlElement("Tumorkonferenz_Datum", Order = 1)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal der Tumorkonferenz
        /// </summary>
        /// <value>
        /// Alphanumerischer Wert bis 16 Zeichen.
        /// </value>
        [XmlAttribute("Tumorkonferenz_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Typ der Tumorkonferenz
        /// </summary>
        [XmlElement("Tumorkonferenz_Typ", Order = 2)]
        public string Typ
        {
            get
            {
                return typ;
            }

            set
            {
                typ = value.ValidateOrThrow(StringValidatorBehavior.LowcaseTrimAllowEmpty
                    , _allowedTypes, 0, _typeName, nameof(this.typ));
            }
        }
    }
}
