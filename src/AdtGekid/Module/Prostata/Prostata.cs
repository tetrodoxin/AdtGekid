using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module.Prostata
{
    using Validation;

    /// <summary>
    /// Enthält spezifische Daten zum Prostata-Tumoren
    /// </summary>    
    [Serializable()]
    [XmlType("Modul_Prostata_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class ModulProstata
    {

        private ProstataGleasonScore _gleasonScoreField;

        private ProstataAnlassGleasonScore? _anlassGleasonScore;        

        private int? _anzahlStanzen;

        private int? _anzahlPosStanzen;

        private ProstataCaBefallStanze _caBefallStanze;

        private decimal? _psaValue;

        private JnuTyp? _komplPostOp;

        private string _typeName = nameof(ModulProstata);

        /// <summary>
        /// Wert des Gleason-Score (Malignitätskriterium, therapieentscheidend)
        /// Muster 1 + Muster 2 = Gleason-Score mod.nach ISUP 2005 bei primärem Ca-Nachweis und im OP-Präparat
        /// </summary>
        [XmlElement("GleasonScore", Order = 1)]
        public ProstataGleasonScore GleasonScore
        {
            get
            {
                return _gleasonScoreField;
            }
            set
            {
                _gleasonScoreField = value;
            }
        }

        /// <summary>
        /// Anlass der Bestimmung des Scores (Malignitätskriterium, therapieentscheidend)
        /// </summary>
        [XmlIgnore]
        public string AnlassGleasonScore
        {
            get { return _anlassGleasonScore?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _anlassGleasonScore = value.TryParseAsEnumOrThrow<ProstataAnlassGleasonScore>(_typeName, nameof(this.AnlassGleasonScore));
            }
        }

        /// <summary>
        /// Anlass der Bestimmung des Scores (Malignitätskriterium, therapieentscheidend)
        /// </summary>
        [XmlElement("AnlassGleasonScore", Order = 2)]
        public ProstataAnlassGleasonScore? AnlassGleasonScoreEnumValue
        {
            get
            {
                return _anlassGleasonScore;
            }
            set
            {
                _anlassGleasonScore = value;
            }
        }


        [XmlIgnore]
        public bool AnlassGleasonScoreEnumValueSpecified => AnlassGleasonScoreEnumValue.HasValue;



        /// <summary>
        /// Datum der Entnahme der Stanzen (Zuordnung der Stanzen im Therapieverlauf
        /// für die Qualitätsindikatoren 1 und 3 der S3-Leitlinie)       
        /// </summary>
        [XmlElement("DatumStanzen", DataType = "date", Order = 3)]        
        public DateTime? DatumStanzen { get; set; }


        [XmlIgnore]
        public bool DatumStanzenSpecified
        {
            get { return DatumStanzen.HasValue; }
           
        }

        /// <summary>
        /// Anzahl der entnommenen Stanzen
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("AnzahlStanzen", DataType = "nonNegativeInteger", Order = 4)]
        public string AnzahlStanzenString
        {
            get { return this._anzahlStanzen?.ToString(); }
            set { this._anzahlStanzen = value.ParseNullableInt()?.BetweenOrThrow(0,99); }
        }

        /// <summary>
        /// Anzahl der entnommenen Stanzen
        /// </summary>
        [XmlIgnore]
        public int? AnzahlStanzen
        {
            get { return _anzahlStanzen; }
            set { _anzahlStanzen = value?.BetweenOrThrow(0,99); }
        }

        /// <summary>
        /// Anzahl der positiven Stanzen
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("AnzahlPosStanzen", DataType = "nonNegativeInteger", Order = 5)]
        public string AnzahlPositiveStanzenString
        {
            get { return _anzahlPosStanzen?.ToString(); }
            set { _anzahlPosStanzen = value.ParseNullableInt()?.BetweenOrThrow(0, 99); }
        }

        /// <summary>
        /// Anzahl der positiven Stanzen
        /// </summary>
        [XmlIgnore]
        public int? AnzahlPositiveStanzen
        {
            get { return _anzahlPosStanzen; }
            set { _anzahlPosStanzen = value?.BetweenOrThrow(0, 99); }
        }


        /// <summary>
        /// Semiquantitative Abschätzung des Prozentsatzes der Gesamtkarzinomfläche/
        /// Gesamtstanzzylinderfläche der am schwersten befallenen Stanze(erforderlich
        /// lt. Leitlinie, um die Möglichkeit von Active Surveillance zu dokumentieren
        /// </summary>
        [XmlElement("CaBefallStanze", Order = 6)]
        public ProstataCaBefallStanze CaBefallStanze
        {
            get
            {
                return this._caBefallStanze;
            }
            set
            {
                this._caBefallStanze = value;
            }
        }

        /// <summary>
        /// Aktuell relevanter PSA-Wert (1 – 100000 = PSA-Wert in ng/ml, Fließkommazahl mit max. 3 Dezimalstellen)
        /// </summary>
        [XmlElement("PSA", Order = 7)]
        public decimal? PSA
        {
            get { return _psaValue; }
            set { _psaValue = value?.BetweenOrThrow(0, 99999); }
        }

       
        [XmlIgnore]
        public bool PSASpecified
        {
            get { return _psaValue.HasValue; }           
        }

        /// <summary>
        /// Datum der Blutentnahme zur PSA-Bestimmung        
        /// </summary>
        [XmlElement("DatumPSA", DataType = "date" , Order = 8)]        
        public DateTime? DatumPSA { get; set; }

        [XmlIgnore]
        public bool DatumPSASpecified => DatumPSA.HasValue;

        /// <summary>
        /// Postoperative Komplikation der Clavien-Dindo Grade III oder 
        /// IV innerhalb der ersten 6 Monate nach Radikaler Prostatektomie
        /// </summary> 
        [XmlElement("KomplPostOP_ClavienDindo", Order = 9)]
        public JnuTyp? KomplikationPostOp
        {
            get
            {
                return _komplPostOp;
            }
            set
            {
                _komplPostOp = value;
            }
        }

       
        [XmlIgnore]
        public bool KomplikationPostOpSpecified
        {
            get { return _komplPostOp.HasValue; }           
        }
    }

   
    

   
    

   
   

   
   
}
