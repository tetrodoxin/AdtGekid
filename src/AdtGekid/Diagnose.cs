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
    /// Enthält Daten zu einer Tumordiagnose.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungDiagnose", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Diagnose
    {
        private static char[] AllowedConfirmCodes = "1245679".ToCharArray();

        private string _seitenlokalisation;
        private string _icdoCode;
        private string _diagnosesicherung;
        private string _allgemeinerLeistungszustand;
        private string _anmerkung;
        private string _fruehereTumorerkrankungen;
        private string _text;
        private string _icdVersion;
        private string _icdoFreitext;
        private string _icdoVersion;
        private string _id;
        private IcdTyp _icdCode;

        /// <summary>
        /// Beurteilung des allgemeinen Leistungszustandes nach ECOG oder Karnofsky in %.
        /// </summary>
        /// <value>
        /// 0 - 4 für ECOG oder 0% bis 100% für Karnofsky oder 'U' für unbekannt.</value>
        [XmlElement("Allgemeiner_Leistungszustand", Order = 15)]
        public string AllgemeinerLeistungszustand
        {
            get { return _allgemeinerLeistungszustand; }
            set { _allgemeinerLeistungszustand = value.ValidateOrThrow(LeistungszustandValidator.Instance); }
        }

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 16)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Zeitpunkt, angegeben in Tag, Monat und Jahr, an dem die meldepflichtige Di-
        /// agnose erstmals durch einen Arzt klinisch oder mikroskopisch diagnostiziert
        /// wurde
        /// </summary>
        [XmlElement("Diagnosedatum", Order = 7)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Höchste erreichte Diagnosesicherheit zum Diagnosedatum (siehe ICD-O-3, S. 60)
        /// </summary>
        [XmlElement("Diagnosesicherung", Order = 8)]
        public string Diagnosesicherung
        {
            get
            {
                return _diagnosesicherung;
            }
            set
            {
                _diagnosesicherung = value.ValidateOrThrow(AllowedConfirmCodes);
            }
        }

        /// <summary>
        /// Tumorerkrankungen, die in der Anamnese zu einem früheren Zeitpunkt
        /// diagnnostiziert/behandelt wurden.
        /// </summary>
        [XmlElement("Fruehere_Tumorerkrankungen", Order = 10)]
        public string FruehereTumorerkrankungen
        {
            get { return _fruehereTumorerkrankungen; }
            set { _fruehereTumorerkrankungen = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Array mit Angaben über Fernmetastasen zum Diagnosezeitpunkt.
        /// </summary>
        [XmlArrayItem("Fernmetastase", IsNullable = false)]
        [XmlArray("Menge_FM", Order = 12)]
        public Fernmetastase[] Fernmetastasen { get; set; }

        /// <summary>
        /// Array mit histologischen Angaben zur Diagnose nach ICD-O
        /// </summary>
        [XmlArrayItem("Histologie", IsNullable = false)]
        [XmlArray("Menge_Histologie", Order = 11)]
        public HistologieTyp[] Histologien { get; set; }

        /// <summary>
        /// Array mit Angaben zur Klassifizierung nach TNM.
        /// </summary>
        [XmlArrayItem("TNM", IsNullable = false)]
        [XmlArray("Menge_TNM", Order = 13)]
        public TnmTyp[] TnmKlassifizierungen { get; set; }

        /// <summary>
        /// Array mit Angaben zur Einstufung nach anderen Klassifikationen.
        /// </summary>
        [XmlArrayItem("Weitere_Klassifikation", IsNullable = false)]
        [XmlArray("Menge_Weitere_Klassifikation", Order = 14)]
        public WeitereKlassifikation[] WeitereKlassifikationen { get; set; }

        /// <summary>
        /// Bezeichnung der meldepflichtigen Tumorerkrankung.
        /// </summary>
        [XmlElement("Primaertumor_Diagnosetext", Order = 3)]
        public string Text
        {
            get { return _text; }
            set { _text = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Kodierung einer meldepflichtigen Tumorerkrankung nach der aktuellen ICD-10-GM
        /// </summary>
        [XmlElement("Primaertumor_ICD_Code", Order = 1)]
        public IcdTyp IcdCode
        {
            get { return _icdCode; }
            set { _icdCode = value; }
        }

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-10-GM Version
        /// </summary>
        [XmlElement("Primaertumor_ICD_Version", Order = 2)]
        public string IcdVersion
        {
            get { return _icdVersion; }
            set { _icdVersion = value.ValidateMaxLength(25); }
        }

        /// <summary>
        /// (Lokalisations-)Code der Topographie (Sitz des Primärtumors) einer meldepflichtigen
        /// Tumorerkrankung nach der aktuellen ICD-O (derzeit (2015) O-3) Version.
        /// </summary>
        [XmlElement("Primaertumor_Topographie_ICD_O", Order = 4)]
        public string IcdoCode
        {
            get
            {
                return _icdoCode;
            }
            set
            {
                _icdoCode = value.ValidateOrThrow(@"^C\d\d\.\d(\d)?$");
            }
        }

        /// <summary>
        /// Bezeichnung einer meldepflichtigen Erkrankung: 
        /// Topographie nach der aktuellen ICD-O
        /// </summary>
        [XmlElement("Primaertumor_Topographie_ICD_O_Freitext", Order = 6)]
        public string IcdoFreitext
        {
            get { return _icdoFreitext; }
            set { _icdoFreitext = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-O Version
        /// </summary>
        [XmlElement("Primaertumor_Topographie_ICD_O_Version", Order = 5)]
        public string IcdoVersion
        {
            get { return _icdoVersion; }
            set { _icdoVersion = value.ValidateMaxLength(25); }
        }

        /// <summary>
        /// Angabe der betroffenen organspezifischen Seite.
        /// </summary>
        [XmlElement("Seitenlokalisation", Order = 9)]
        public string Seitenlokalisation
        {
            get { return _seitenlokalisation; }
            set { _seitenlokalisation = value.ValidateOrThrow(SeitenlokalisationValidator.Instance); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Tumors.
        /// </summary>
        [XmlAttribute("Tumor_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16); }
        }
    }
}
