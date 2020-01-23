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
    [XmlType("ADT_GEKIDPatientMeldungVerlauf", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Verlauf
    {
        private static char[] AllowedSummaryStatusCodes = "VTKPDBRUX".ToCharArray();
        private static char[] AllowedPrimaryAndLymphStatusCodes = "KTPNRFUX".ToCharArray();
        private static char[] AllowedMetastasisStatusCodes = "KMRTPNFUX".ToCharArray();

        private TumorstatusGesamt _tumorstatusGesamt;
        private TumorstatusLokal _tumorstatusLokal;
        private TumorstatusFernmetastasen _tumorstatusFernmetastasen;
        private TumorstatusLymphknoten _tumorstatusLymphknoten;
        private string _allgemeinerLeistungszustand;
        private string _anmerkung;
        private string _id;

        private string _typeName = typeof(Verlauf).Name;

        /// <summary>
        /// Beurteilung des allgemeinen Leistungszustandes nach ECOG oder Karnofsky in
        /// %
        /// </summary>
        [XmlElement("Allgemeiner_Leistungszustand", Order = 10)]
        public string AllgemeinerLeistungszustand
        {
            get { return _allgemeinerLeistungszustand; }
            set { _allgemeinerLeistungszustand = value.ValidateOrThrow(LeistungszustandValidator.Instance, _typeName, nameof(this.AllgemeinerLeistungszustand)); }
        }

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
        /// Gesamtbeurteilung der Erkrankung unter Berücksichtigung aller Manifestatio-
        /// nen
        /// </summary>
        [XmlIgnore]
        public string TumorstatusGesamt
        {
            get { return _tumorstatusGesamt.ToString(); }
            set { _tumorstatusGesamt = value.TryParseAsEnumOrThrow<TumorstatusGesamt>(_typeName, nameof(this.TumorstatusGesamt)); }
        }

        [XmlElement("Gesamtbeurteilung_Tumorstatus", Order = 5)]
        public TumorstatusGesamt TumorstatusGesamtEnumValue
        {
            get { return _tumorstatusGesamt; }
            set { _tumorstatusGesamt = value; }
        }

        [XmlElement("Histologie", Order = 1)]
        public HistologieTyp Histologie { get; set; }

        [XmlArrayItem("Fernmetastase", IsNullable = false)]
        [XmlArray("Menge_FM", Order = 9)]
        public Fernmetastase[] Fernmetastasen { get; set; }

        //[XmlArrayItem("TNM", IsNullable = false)]
        //[XmlArray("Menge_TNM", Order = 2)]
        //public TnmTyp[] TnmKlassifizierungen { get; set; }

        [XmlElement("TNM", Order = 2)]
        public TnmTyp TnmKlassifizierung { get; set; }

        [XmlArrayItem("Weitere_Klassifikation", IsNullable = false)]
        [XmlArray("Menge_Weitere_Klassifikation", Order = 3)]
        public WeitereKlassifikation[] WeitereKlassifikationen { get; set; }

        [XmlElement("Tod", Order = 11)]
        public VerlaufTod Tod { get; set; }

        /// <summary>
        /// Das Datum, an dem die letzte Untersuchung durchgeführt wurde, die zur Ein-
        /// schätzung des Tumorstatus geführt hat.
        /// </summary>
        [XmlElement("Untersuchungsdatum_Verlauf", Order = 4)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Verlaufsdatensatzes
        /// </summary>
        /// <value>
        /// Alphanumerisch bis 16 Zeichen
        /// </value>
        [XmlAttribute("Verlauf_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Beurteilung der Situation im Primärtumorbereich
        /// </summary>
        [XmlIgnore]
        public string TumorstatusLokal
        {
            get { return _tumorstatusLokal.ToString(); }
            //set { _tumorstatusLokal = value.ValidateOrThrow(AllowedPrimaryAndLymphStatusCodes, _typeName, nameof(this.TumorstatusLokal)); }
            set { _tumorstatusLokal = value.TryParseAsEnumOrThrow<TumorstatusLokal>(_typeName, nameof(this.TumorstatusLokal)); }
        }

        [XmlElement("Verlauf_Lokaler_Tumorstatus", Order = 6)]
        public TumorstatusLokal TumorstatusLokalEnumValue
        {
            get { return _tumorstatusLokal; }
            set { _tumorstatusLokal = value; }
        }

        [XmlIgnore]
        public string TumorstatusFernmetastasen
        {
            get { return _tumorstatusFernmetastasen.ToString(); }
            //set { _tumorstatusFernmetastasen = value.ValidateOrThrow(AllowedMetastasisStatusCodes, _typeName, nameof(this.TumorstatusFernmetastasen)); }
            set { _tumorstatusFernmetastasen = value.TryParseAsEnumOrThrow<TumorstatusFernmetastasen>(_typeName, nameof(this.TumorstatusFernmetastasen)); }
        }

        [XmlElement("Verlauf_Tumorstatus_Fernmetastasen", Order = 8)]
        public TumorstatusFernmetastasen TumorstatusFernmetastasenEnumValue
        {
            get { return _tumorstatusFernmetastasen; }
            set { _tumorstatusFernmetastasen = value; }
        }

        /// <summary>
        /// Beurteilung der Situation im Bereich der regionären Lymphknoten
        /// </summary>
        [XmlIgnore]
        public string TumorstatusLymphknoten
        {
            get { return _tumorstatusLymphknoten.ToString(); }
            //set { _tumorstatusLymphknoten = value.ValidateOrThrow(AllowedPrimaryAndLymphStatusCodes, _typeName, nameof(this.TumorstatusLymphknoten)); }
            set { _tumorstatusLymphknoten = value.TryParseAsEnumOrThrow<TumorstatusLymphknoten>(_typeName, nameof(this.TumorstatusLymphknoten)); }
        }

        [XmlElement("Verlauf_Tumorstatus_Lymphknoten", Order = 7)]
        public TumorstatusLymphknoten TumorstatusLymphknotenEnumValue
        {
            get { return _tumorstatusLymphknoten; }
            set { _tumorstatusLymphknoten = value; }
        }
    }
}
