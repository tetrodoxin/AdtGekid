using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module
{
    using Validation;

    /// <summary>
    /// Enthält spezifische Daten zum Darm-Tumoren (Kolorektal-Ca.)
    /// </summary>
    [Serializable()]
    [XmlType("Modul_Darm_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class ModulDarm
    {
        private string _rektumAbstandAnokutanlinie;

        private string _rektumAbstandAboralerResektionsrand;

        private string _rektumAbstandCircResektionsebene;

        private DarmRektumQualitaetTME? _rektumQualitaetTME;

        private string _rektumMRTDuennschichtAngabemesorektaleFaszie;

        private DarmArtEingriff? _artEingriff;

        private DarmRektumAnzeichnungStomaposition? _rektumAnzeichnungStomaposition;

        private DarmRektumGradAnastomoseninsuffizienz? _rektumGradAnastomoseninsuffizienz;

        private DarmKlassifizierungASA? _klassifizierungASA;

        private DarmRASMutation? _mutationRAS;

        private string _typeName = nameof(ModulDarm);

        /// <summary>
        /// Höhe des Sitzes des Rektumkarzinoms ab Anokutanlinie bei Rektum-Ca.
        /// </summary>
        [XmlElement("RektumAbstandAnokutanlinie", Order = 1)]
        public string RektumAbstandAnokutanlinie
        {
            get
            {
                return _rektumAbstandAnokutanlinie;
            }
            set
            {
                _rektumAbstandAnokutanlinie = value.ValidateOrThrow(ThreeDigitNumberValidator.Instance, _typeName, nameof(this.RektumAbstandAnokutanlinie));
            }
        }

        /// <summary>
        /// Minimaler Abstand des aboralen Tumorrandes zum aboralen Resektionsrand in mm bei Rektum-Ca.
        /// </summary>
        [XmlElement("RektumAbstandAboralerResektionsrand", Order = 2)]
        public string RektumAbstandAboralerResektionsrand
        {
            get
            {
                return _rektumAbstandAboralerResektionsrand;
            }
            set
            {
                _rektumAbstandAboralerResektionsrand = value.ValidateOrThrow(ThreeDigitNumberValidator.Instance, _typeName, nameof(this.RektumAbstandAboralerResektionsrand));
            }
        }

        /// <summary>
        /// Minimaler Abstand des Tumors zur circumferentiellen mesorektalen Resektionsebene in mm
        /// bei Rektum-Ca.
        /// </summary>
        [XmlElement("RektumAbstandCircResektionsebene", Order = 3)]
        public string RektumAbstandCircResektionsebene
        {
            get
            {
                return _rektumAbstandCircResektionsebene;
            }
            set
            {
                _rektumAbstandCircResektionsebene = value.ValidateOrThrow(ThreeDigitNumberValidator.Instance, _typeName, nameof(this.RektumAbstandCircResektionsebene)); ;
            }
        }

        /// <summary>
        /// Qualität des TME-Präparats bei Rektum-Ca.
        /// </summary>
        [XmlIgnore]
        public string RektumQualitaetTME
        {
            get { return _rektumQualitaetTME?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _rektumQualitaetTME = value.TryParseAsEnumOrThrow<DarmRektumQualitaetTME>(_typeName, nameof(this.RektumQualitaetTME));
            }
        }

        [XmlElement("RektumQualitaetTME", Order = 4)]
        public DarmRektumQualitaetTME? RektumQualitaetTMEEnumValue
        {
            get
            {
                return _rektumQualitaetTME;
            }
            set
            {
                _rektumQualitaetTME = value;
            }
        }

        [XmlIgnore]
        public bool RektumQualitaetTMEEnumValueSpecified => _rektumQualitaetTME.HasValue;


        /// <summary>
        /// Rektum-Ca.: Angabe des Abstands des Tumors zur mesorektalen Faszie, 
        /// wenn eine MRT oder Dünnschicht-CT Untersuchung durchgeführt wird.
        /// </summary>
        [XmlElement("RektumMRTDuennschichtAngabemesorektaleFaszie", Order = 5)]
        public string RektumMRTDuennschichtAngabemesorektaleFaszie
        {
            get
            {
                return _rektumMRTDuennschichtAngabemesorektaleFaszie;
            }
            set
            {
                _rektumMRTDuennschichtAngabemesorektaleFaszie = value.ValidateOrThrow(@"^[1-9](\d)?|[DNU]?$", _typeName, nameof(this.RektumMRTDuennschichtAngabemesorektaleFaszie)); ;
            }
        }


        /// <summary>
        /// Modalität der Eingriffsdurchführung
        /// </summary>
        [XmlIgnore]
        public string ArtEingriff
        {
            get { return _artEingriff?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _artEingriff = value.TryParseAsEnumOrThrow<DarmArtEingriff>(_typeName, nameof(this.ArtEingriff));
            }
        }

        [XmlElement("ArtEingriff", Order = 6)]
        public DarmArtEingriff? ArtEingriffEnumValue
        {
            get
            {
                return _artEingriff;
            }
            set
            {
                _artEingriff = value;
            }
        }

        [XmlIgnore]
        public bool ArtEingriffEnumValueSpecified => _artEingriff.HasValue;

        /// <summary>
        /// Präoperative Anzeichnung der Stomaposition bei Rektum-Ca.
        /// </summary>
        [XmlIgnore]
        public string RektumAnzeichnungStomaposition
        {
            get { return _rektumAnzeichnungStomaposition?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _rektumAnzeichnungStomaposition = value.TryParseAsEnumOrThrow<DarmRektumAnzeichnungStomaposition>(_typeName, nameof(this.RektumAnzeichnungStomaposition));
            }
        }

        [XmlElement("RektumAnzeichnungStomaposition", Order = 7)]
        public DarmRektumAnzeichnungStomaposition? RektumAnzeichnungStomapositionEnumValue
        {
            get
            {
                return _rektumAnzeichnungStomaposition;
            }
            set
            {
                _rektumAnzeichnungStomaposition = value;
            }
        }

        [XmlIgnore]
        public bool RektumAnzeichnungStomapositionEnumValueSpecified => _rektumAnzeichnungStomaposition.HasValue;


        /// <summary>
        /// Anastomoseninsuffizienz bei Rektum-Ca.
        /// Grad A (keine therapeutische Konsequenz)
        /// Grad B (Antibiotikagabe oder interventionelle Drainage oder transanale Lavage/Drainage)
        /// Grad C ((Re)-Laparotomie)Anastomoseninsuffizienz nach elektivem Eingriff mit Anastomosenanlage
        /// </summary>
        [XmlIgnore]
        public string RektumGradAnastomoseninsuffizienz
        {
            get { return _rektumGradAnastomoseninsuffizienz?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _rektumGradAnastomoseninsuffizienz = value.TryParseAsEnumOrThrow<DarmRektumGradAnastomoseninsuffizienz>(_typeName, nameof(this.RektumGradAnastomoseninsuffizienz));
            }
        }

        [XmlElement("GradRektumAnastomoseninsuffizienz", Order = 8)]
        public DarmRektumGradAnastomoseninsuffizienz? RektumGradAnastomoseninsuffizienzEnumValue
        {
            get
            {
                return _rektumGradAnastomoseninsuffizienz;
            }
            set
            {
                _rektumGradAnastomoseninsuffizienz = value;
            }
        }

        [XmlIgnore]
        public bool RektumGradAnastomoseninsuffizienzEnumValueSpecified => _rektumGradAnastomoseninsuffizienz.HasValue;

        /// <summary>
        /// Einstufung des Patienten nach der ASA-Klassifikation bei präoperativer Untersuchung durch den Anästhesisten.
        /// </summary>
        [XmlIgnore]
        public string KlassifizierungASA
        {
            get { return _klassifizierungASA?.ToString("d"); }
            set
            {
                if (!value.IsNothing())
                    _klassifizierungASA = value.TryParseAsEnumOrThrow<DarmKlassifizierungASA>(_typeName, nameof(this.KlassifizierungASA));
            }
        }

        [XmlElement("ASA", Order = 9)]
        public DarmKlassifizierungASA? KlassifizierungASAEnumValue
        {
            get
            {
                return _klassifizierungASA;
            }
            set
            {
                _klassifizierungASA = value;
            }
        }

        [XmlIgnore]
        public bool KlassifizierungASAEnumValueSpecified => _klassifizierungASA.HasValue;


        /// <summary>
        /// Angabe Vorliegen einer Mutation im K-RAS-Onkogen
        /// </summary>
        [XmlIgnore]
        public string RASMutation
        {
            get { return _mutationRAS?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _mutationRAS = value.TryParseAsEnumOrThrow<DarmRASMutation>(_typeName, nameof(this.RASMutation));
            }
        }

        [XmlElement("RASMutation", Order = 10)]
        public DarmRASMutation? RASMutationEnumValue
        {
            get
            {
                return _mutationRAS;
            }
            set
            {
                _mutationRAS = value;
            }
        }

        [XmlIgnore]
        public bool RASMutationEnumValueSpecified => _mutationRAS.HasValue;


    }

}
