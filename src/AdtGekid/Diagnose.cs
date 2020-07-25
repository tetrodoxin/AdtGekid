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

        private SeitenlokalisationTyp? _seitenlokalisation;
        private string _icdoCode;
        private Diagnosesicherung _diagnosesicherung;
        private AllgemeinerLeistungszustandTyp? _allgemeinerLeistungszustand;
        private string _anmerkung;
        private string _fruehereTumorerkrankungen;
        private string _text;
        private IcdVersionTyp? _icdVersion;
        private string _icdoFreitext;
        private TopographieIcdOVersionTyp? _icdoVersion;
        private string _id;
        private IcdTyp _icdCode;

        private string _entity = typeof(Diagnose).Name;

        /// <summary>
        /// Gibt an, ob die Länge von <see cref="Text"/> oder <see cref="Anmerkung"/> validiert werden soll oder nicht.
        /// Default: true
        /// </summary>
        public static bool TextLengthValidationEnabled = true;



        /// <summary>
        /// Beurteilung des allgemeinen Leistungszustandes nach ECOG oder Karnofsky in %.
        /// </summary>
        /// <value>
        /// 0 - 4 für ECOG oder 0% bis 100% für Karnofsky oder 'U' für unbekannt.</value>
        [XmlIgnore]
        public string AllgemeinerLeistungszustand
        {
            get { return _allgemeinerLeistungszustand?.ToXmlEnumAttributeName(); }
            //set { _allgemeinerLeistungszustand = value.ValidateOrThrow(LeistungszustandValidator.Instance, _entity, nameof(this.AllgemeinerLeistungszustand)); }
            set { _allgemeinerLeistungszustand = value.TryParseAsEnumOrThrow<AllgemeinerLeistungszustandTyp>(_entity, nameof(AllgemeinerLeistungszustand)); }
        }

        [XmlElement("Allgemeiner_Leistungszustand", Order = 16)]
        public AllgemeinerLeistungszustandTyp? AllgemeinerLeistungszustandEnumValue
        {
            get { return _allgemeinerLeistungszustand; }
            set { _allgemeinerLeistungszustand = value; }
        }

        public bool AllgemeinerLeistungszustandEnumValueSpecified => AllgemeinerLeistungszustandEnumValue.HasValue;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 17)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set
            {
                _anmerkung = TextLengthValidationEnabled
                  ? value.ValidateMaxLength(500, _entity, nameof(this.Anmerkung))
                  : value;
            }
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
        [XmlIgnore]
        public string Diagnosesicherung
        {
            get
            {
                return ((int)_diagnosesicherung).ToString();
            }
            //set
            //{
            //    _diagnosesicherung = value.ValidateOrThrow(AllowedConfirmCodes, _entity, nameof(this.Diagnosesicherung));
            //}
            set { _diagnosesicherung = value.TryParseAsEnumOrThrow<Diagnosesicherung>(_entity, nameof(this.Diagnosesicherung)); }
        }

        [XmlElement("Diagnosesicherung", Order = 8)]
        public Diagnosesicherung DiagnosesicherungEnumValue
        {
            get
            {
                return _diagnosesicherung;
            }
            set
            {
                _diagnosesicherung = value;
            }
        }

        /// <summary>
        /// Tumorerkrankungen, die in der Anamnese zu einem früheren Zeitpunkt
        /// diagnnostiziert/behandelt wurden.
        /// </summary>
        //[XmlElement("Fruehere_Tumorerkrankungen", Order = 10)]
        //public string FruehereTumorerkrankungen
        //{
        //    get { return _fruehereTumorerkrankungen; }
        //    set { _fruehereTumorerkrankungen = value.ValidateMaxLength(500, _entity, nameof(this.FruehereTumorerkrankungen)); }
        //}

        /// <summary>
        /// Array mit Tumorerkrankungen, die in der Anamnese zu einem früheren Zeitpunkt
        /// diagnostiziert/behandelt wurden.
        /// </summary>
        [XmlArrayItem("Fruehere_Tumorerkrankung", IsNullable = false)]
        [XmlArray("Menge_Fruehere_Tumorerkrankung", Order = 10)]
        public FruehereTumorerkrankung[] FruehereTumorerkrankungen { get; set; }

        /// <summary>
        /// Array mit histologischen Angaben zur Diagnose nach ICD-O
        /// </summary>
        [XmlArrayItem("Histologie", IsNullable = false)]
        [XmlArray("Menge_Histologie", Order = 11)]
        public HistologieTyp[] Histologien { get; set; }

        /// <summary>
        /// Array mit Angaben über Fernmetastasen zum Diagnosezeitpunkt.
        /// </summary>
        [XmlArrayItem("Fernmetastase", IsNullable = false)]
        [XmlArray("Menge_FM", Order = 12)]
        public Fernmetastase[] Fernmetastasen { get; set; }

       
        /// <summary>
        /// Array mit Angaben zur Klassifizierung nach TNM.
        /// </summary>
        //[XmlArrayItem("TNM", IsNullable = false)]
        //[XmlArray("Menge_TNM", Order = 13)]
        //public TnmTyp[] TnmKlassifizierungen { get; set; }

        [XmlElement("cTNM", Order = 13)]
        public TnmTyp TnmKlassifizierungKlinisch{ get; set; }

        [XmlElement("pTNM", Order = 14)]
        public TnmTyp TnmKlassifizierungPathologisch { get; set; }


        /// <summary>
        /// Array mit Angaben zur Einstufung nach anderen Klassifikationen.
        /// </summary>
        [XmlArrayItem("Weitere_Klassifikation", IsNullable = false)]
        [XmlArray("Menge_Weitere_Klassifikation", Order = 15)]
        public WeitereKlassifikation[] WeitereKlassifikationen { get; set; }

        /// <summary>
        /// Bezeichnung der meldepflichtigen Tumorerkrankung.
        /// </summary>
        [XmlElement("Primaertumor_Diagnosetext", Order = 3)]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = TextLengthValidationEnabled
                      ? value.ValidateMaxLength(500, _entity, nameof(this.Text))
                      : value;
            }
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
        [XmlIgnore]
        public string IcdVersion
        {
            get { return _icdVersion?.ToXmlEnumAttributeName(); }
            set { _icdVersion = value.TryParseAsEnumOrThrow<IcdVersionTyp>(_entity, nameof(this.IcdVersion), false); }

        }

        [XmlElement("Primaertumor_ICD_Version", Order = 2)]
        public IcdVersionTyp? IcdVersionEnumValue
        {
            get { return _icdVersion; }
            set { _icdVersion = value;  }
        }

        /// <summary>
        /// Zur Möglichkeit der Steuerung der Serialisierung:
        /// Bei <c>false</c> wird das entsprechende Element im XML nicht geschrieben
        /// </summary>
        [XmlIgnore]
        public bool IcdVersionEnumValueSpecified => IcdVersionEnumValue.HasValue;



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
                _icdoCode = value.ValidateOrThrow(@"^C\d\d\.\d(\d)?$", _entity, nameof(this.IcdoCode));                
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
            set { _icdoFreitext = value.ValidateMaxLength(500, _entity, nameof(this.IcdoFreitext)); }
        }

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-O Version
        /// </summary>
        [XmlIgnore]
        public string IcdoVersion
        {
            get { return ((int?)_icdoVersion).ToString(); }
            //set { _icdoVersion = value.ValidateMaxLength(25, _entity, nameof(this.IcdoVersion)); }
            set { _icdoVersion = value.TryParseAsEnumOrThrow<TopographieIcdOVersionTyp>(_entity, nameof(this.IcdoVersion)); }
        }

        [XmlElement("Primaertumor_Topographie_ICD_O_Version", Order = 5)]
        public TopographieIcdOVersionTyp? IcdoVersionEnumValue
        {
            get { return _icdoVersion; }
            set { _icdoVersion = value; }
        }


        /// <summary>
        /// Zur Möglichkeit der Steuerung der Serialisierung:
        /// Bei <c>false</c> wird das entsprechende Element im XML nicht geschrieben
        /// </summary>
        [XmlIgnore]
        public bool IcdoVersionEnumValueSpecified => IcdoVersionEnumValue.HasValue;

        /// <summary>
        /// Angabe der betroffenen organspezifischen Seite.
        /// </summary>

        [XmlIgnore]
        public string Seitenlokalisation
        {
            get { return _seitenlokalisation.ToString(); }
            //set { _seitenlokalisation = value.ValidateOrThrow(SeitenlokalisationValidator.Instance, _entity, nameof(this.Seitenlokalisation)); }
            set { _seitenlokalisation = value.TryParseAsEnumOrThrow<SeitenlokalisationTyp>(_entity, nameof(this.Seitenlokalisation)); }

        }

        [XmlElement("Seitenlokalisation", Order = 9)]
        public SeitenlokalisationTyp? SeitenlokalisationEnumValue
        {
            get { return _seitenlokalisation; }
            set { _seitenlokalisation = value; }
        }

        /// <summary>
        /// Zur Möglichkeit der Steuerung der Serialisierung:
        /// Bei <c>false</c> wird das entsprechende Element im XML nicht geschrieben
        /// </summary>
        [XmlIgnore]
        public bool SeitenlokalisationEnumValueSpecified => SeitenlokalisationEnumValue.HasValue;

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Tumors.
        /// </summary>
        [XmlAttribute("Tumor_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16, _entity, nameof(this.Id)); }
        }

        /// <summary>
        /// Miscellaneous property, die nicht serialisiert wird.
        /// Kann dazu verwendet werden, im ADT-Objektmodell 
        /// vorzuhalten, ob die <see cref="Id"/> neu ist oder bereits 
        /// verwendet/gemeldet wurde
        /// </summary>
        [XmlIgnore]
        public bool MiscHasNewId { get; set; }        
    }
}
