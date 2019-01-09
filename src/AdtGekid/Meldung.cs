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
    /// <summary>
    /// Enthält die Daten der Meldung.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Meldung
    {
        private static char[] AllowedReasonCodes = "IADWV".ToCharArray();
        private string _begruendung;
        private string _anmerkung;
        private string _id;        
        private string _melderId;

        private string _typeName = typeof(Meldung).Name;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 12)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.Anmerkung)); }
        }

        /// <summary>
        /// Daten zu einer Tumordiagnose, falls zutreffend.
        /// </summary>
        [XmlElement("Diagnose", Order = 5)]
        public Diagnose Diagnose { get; set; }

        /// <summary>
        /// Anlass der Meldung
        /// </summary>
        [XmlElement("Meldeanlass", Order = 3)]
        public Meldeanlass? Anlass
        {
            get; set;
        }

        [XmlIgnore()]
        public bool AnlassSpecified => Anlass.HasValue;
        

        /// <summary>
        /// Widerspruch/Einwilligung des Patienten
        /// </summary>
        [XmlElement("Meldebegruendung", Order = 2)]
        public string Begruendung
        {
            get
            {
                return _begruendung;
            }
            set
            {
                _begruendung = value.ValidateOrThrow(AllowedReasonCodes, _typeName, nameof(this.Begruendung));
            }
        }

        /// <summary>
        /// Datum der Meldung
        /// </summary>
        [XmlElement("Meldedatum", Order = 1)]
        public DatumTyp Datum { get; set; }


        /// <summary>
        /// Eindeutig identifizierendes Merkmal für eine Meldung
        /// </summary>
        [XmlAttribute("Meldung_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Melders
        /// </summary>
        [XmlAttribute("Melder_ID")]
        public string MelderId
        {
            get { return _melderId; }
            set { _melderId = value.ValidateAlphanumericalOrThrow(6, _typeName, nameof(this.MelderId)); }
        }

        /// <summary>
        /// Daten zu Operationen, sofern zutreffend.
        /// </summary>
        [XmlArrayItem("OP", IsNullable = false)]
        [XmlArray("Menge_OP", Order = 6)]
        public Op[] Operationen { get; set; }

        /// <summary>
        /// Daten zu Strahlentherapien, sofern zutreffend.
        /// </summary>
        [XmlArrayItem("ST", IsNullable = false)]
        [XmlArray("Menge_ST", Order = 7)]
        public Strahlentherapie[] Strahlentherapien { get; set; }

        /// <summary>
        /// Daten zu systemischen Therapien, sofern zutreffend.
        /// </summary>
        [XmlArrayItem("SYST", IsNullable = false)]
        [XmlArray("Menge_SYST", Order = 8)]
        public SystemischeTherapie[] SystemischeTherapien { get; set; }

        /// <summary>
        /// Daten zu Tumorkonferenzen, sofern zutreffend.
        /// </summary>
        [XmlArrayItem("Tumorkonferenz", IsNullable = false)]
        [XmlArray("Menge_Tumorkonferenz", Order = 10)]
        public Tumorkonferenz[] Tumorkonferenzen { get; set; }

        /// <summary>
        /// Daten zu Verläufen, sofern zutreffend.
        /// </summary>
        [XmlArrayItem("Verlauf", IsNullable = false)]
        [XmlArray("Menge_Verlauf", Order = 9)]
        public Verlauf[] Verlaeufe { get; set; }

        /// <summary>
        /// Optionale zusätzliche Daten.
        /// </summary>
        [XmlArrayItem("Zusatzitem", IsNullable = false)]
        [XmlArray("Menge_Zusatzitem", Order = 11)]
        public ZusatzItem[] Zusatzitems { get; set; }

        /// <summary>
        /// Die Zuordnung zu einem Tumor, falls die Meldung selbst keine Diagnose
        /// enthält, sich aber auf eine Tumordiagnose einer anderen Meldung bezieht.
        /// </summary>
        [XmlElement("Tumorzuordnung", Order = 4)]
        public Tumorzuordnung Tumorzuordnung { get; set; }
    }
}
