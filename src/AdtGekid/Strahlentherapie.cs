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
using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungST", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Strahlentherapie
    {
        private string _intention;
        private string _stellungOp;
        private string _endeGrund;
        private string _anmerkung;
        private string _id;

        private string _typeName = typeof(Strahlentherapie).Name;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 7)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.Anmerkung)); }
        }

        [XmlArrayItem("Bestrahlung", IsNullable = false)]
        [XmlArray("Menge_Bestrahlung", Order = 3)]
        public Bestrahlung[] Bestrahlungen { get; set; }

        [XmlArrayItem("ST_Nebenwirkung", IsNullable = false)]
        [XmlArray("Menge_Nebenwirkung", Order = 6)]
        public NebenwirkungTyp[] Nebenwirkungen { get; set; }

        [XmlElement("Residualstatus", Order = 5)]
        public ResidualstatusTyp Residualstatus { get; set; }

        /// <summary>
        /// Gibt den Grund an, warum die Strahlentherapie beendet wurde.
        /// </summary>
        [XmlElement("ST_Ende_Grund", Order = 4)]
        public string EndeGrund
        {
            get { return _endeGrund; }
            set { _endeGrund = value.ValidateOrThrow(TherapieEndeGrundValidator.StrahlenTherapie, _typeName, nameof(this.EndeGrund)); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal der Strahlentherapie
        /// </summary>
        /// <value>
        /// Alphanumerischer Wert bis 16 Zeichen.
        /// </value>
        [XmlAttribute("ST_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Gibt an, mit welcher Intention die Strahlentherapie durchgeführt wird.
        /// </summary>
        [XmlElement("ST_Intention", Order = 1)]
        public string Intention
        {
            get { return _intention; }
            set { _intention = value.ValidateOrThrow(TherapieIntentionValidator.NichtOP, _typeName, nameof(this.Intention)); }
        }

        /// <summary>
        /// Gibt an, in welchem Bezug zu einer operativen Therapie die Bestrahlung steht.
        /// </summary>
        [XmlElement("ST_Stellung_OP", Order = 2)]
        public string StellungOp
        {
            get { return _stellungOp; }
            set { _stellungOp = value.ValidateOrThrow(StellungOpValidator.Instance, _typeName, nameof(this.StellungOp)); }
        }
    }
}
