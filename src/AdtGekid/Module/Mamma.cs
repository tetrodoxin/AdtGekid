using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AdtGekid.Validation;

namespace AdtGekid.Module
{
       
    /// <summary>
    /// Enthält spezifische Daten zum Mamma-Tumoren
    /// </summary>    
    [Serializable()]
    [XmlType("Modul_Mamma_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class ModulMamma
    {
        private MammaPraethMenospausenstatus? _praethMenopausenstatus;

        private MammaHormonrezeptor? _hormonrezeptorStatusOestrogen;

        private MammaHormonrezeptor? _hormonrezeptorStatusProgesteron;

        private MammaHormonrezeptor? _her2neuStatus;

        private MammaPraeopDrahtmarkierung? _praeopDrahtmarkierung;

        private MammaIntraopPraeparatkontrolle? _intraopPraeparatkontrolle;

        private string _tumorgroesseInvasiv;

        private string _tumorgroesseDCIS;

        private string _typeName = nameof(ModulMamma);

        /// <summary>
        /// Prätherapeutischer Menopausenstatus der Patientin
        /// Postmenopausal bedeutet mehr als ein Jahr keine Menstruationsblutung 
        /// oder Estradiol(E 2) und Follikelstimulierendes Hormon(FSH) im eindeutigen 
        /// post-menopausalen Bereich
        /// </summary>
        [XmlIgnore]
        public string PraethMenopausenstatus
        {
            get { return _praethMenopausenstatus?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _praethMenopausenstatus = value.TryParseAsEnumOrThrow<MammaPraethMenospausenstatus>(_typeName, nameof(this.PraethMenopausenstatus));
            }
        }

        [XmlElement("Praetherapeutischer_Menopausenstatus", Order = 1)]
        public MammaPraethMenospausenstatus? PraethMenopausenstatusEnumValue
        {
            get { return _praethMenopausenstatus; }
            set { _praethMenopausenstatus = value; }
        }

        [XmlIgnore]
        public bool PraethMenopausenstatusEnumValueSpecified => PraethMenopausenstatusEnumValue.HasValue;

        /// <summary>
        /// HormonrezeptorStatus: Östrogen
        /// Rezeptorstatus Positiv/Negativ (gemäß: Immunreaktiver Score (IRS) Remmele W et al. 1987)
        /// </summary>
        [XmlIgnore]
        public string HormonrezeptorStatusOestrogen 
        {
            get { return _hormonrezeptorStatusOestrogen?.ToString(); }
            set
            {
                if (!value.IsNothing())
                    _hormonrezeptorStatusOestrogen = value.TryParseAsEnumOrThrow<MammaHormonrezeptor>(_typeName, nameof(this.HormonrezeptorStatusOestrogen));
            }
        }


        [XmlElement("HormonrezeptorStatus_Oestrogen", Order = 2)]
        public MammaHormonrezeptor? HormonrezeptorStatusOestrogenEnumValue
        {
            get 
            {
                return _hormonrezeptorStatusOestrogen;
            }
            set
            {
                _hormonrezeptorStatusOestrogen = value;
            }
        }

        [XmlIgnore]
        public bool HormonrezeptorStatusOestrogenEnumValueSpecified => _hormonrezeptorStatusOestrogen.HasValue;

        /// <summary>
        /// HormonrezeptorStatus: Progesteron
        /// Rezeptorstatus Positiv/Negativ (gemäß: Immunreaktiver Score (IRS) Remmele W et al. 1987). 
        /// Bei unterschiedlichem Ausfall für Östrogen und Progesteron ist der höhere Score 
        /// zu dokumentieren.
        /// </summary>
        [XmlIgnore]
        public string HormonrezeptorStatusProgesteron
        {
            get { return _hormonrezeptorStatusProgesteron?.ToString(); }
            set
            {
                if (!value.IsNothing())
                    _hormonrezeptorStatusProgesteron = value.TryParseAsEnumOrThrow<MammaHormonrezeptor>(_typeName, nameof(this.HormonrezeptorStatusProgesteron));
            }
        }

        [XmlElement("HormonrezeptorStatus_Progesteron", Order = 3)]
        public MammaHormonrezeptor? HormonrezeptorStatusProgesteronEnumValue
        {
            get
            {
                return _hormonrezeptorStatusProgesteron;
            }
            set
            {
                _hormonrezeptorStatusProgesteron = value;
            }
        }

        [XmlIgnore]
        public bool HormonrezeptorStatusProgesteronEnumValueSpecified => _hormonrezeptorStatusProgesteron.HasValue;

        /// <summary>
        /// Her2neu Status:
        /// Rezeptorstatus Positiv/Negativ (gemäß Immunreaktiven Scores nach Leitlinie)
        /// </summary>
        [XmlIgnore]
        public string Her2neuStatus
        {
            get { return _her2neuStatus?.ToString(); }
            set
            {
                if (!value.IsNothing())
                    _her2neuStatus = value.TryParseAsEnumOrThrow<MammaHormonrezeptor>(_typeName, nameof(this.Her2neuStatus));
            }
        }

        [XmlElement("Her2neuStatus", Order = 4)]        
        public MammaHormonrezeptor? Her2neuStatusEnumValue
        {
            get
            {
                return _her2neuStatus;
            }
            set
            {
                _her2neuStatus = value;
            }
        }

        [XmlIgnore]
        public bool Her2neuStatusEnumValueSpecified => _her2neuStatus.HasValue;


        /// <summary>
        /// Angabe präoperative Drahtmarkierung gesteuert durch das angegebene bildgebende Verfahren durchgeführt.
        /// </summary>
        [XmlIgnore]
        public string PraeopDrahtmarkierung
        {
            get { return _praeopDrahtmarkierung?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _praeopDrahtmarkierung = value.TryParseAsEnumOrThrow<MammaPraeopDrahtmarkierung>(_typeName, nameof(this.PraeopDrahtmarkierung));
            }
        }

        [XmlElement("PraeopDrahtmarkierung", Order = 5)]
        public MammaPraeopDrahtmarkierung? PraeopDrahtmarkierungEnumValue
        {
            get
            {
                return _praeopDrahtmarkierung;
            }
            set
            {
                _praeopDrahtmarkierung = value;
            }
        }

        [XmlIgnore]
        public bool PraeopDrahtmarkierungEnumValueSpecified => _praeopDrahtmarkierung.HasValue;

        /// <summary>
        /// Intraoperatives Präparatröntgen/Sonographie:
        /// Angabe ob Präparat intraoperativ mammografiert/sonografiert nach präoperativer 
        /// Drahtmarkierung durch Mammografie oder Sonographie
        /// </summary>
        [XmlIgnore]
        public string IntraopPraeparatkontrolle 
        {
            get { return _intraopPraeparatkontrolle?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _intraopPraeparatkontrolle = value.TryParseAsEnumOrThrow<MammaIntraopPraeparatkontrolle>(_typeName, nameof(this.IntraopPraeparatkontrolle));
            }
        }

        [XmlElement("IntraopPraeparatkontrolle", Order = 6)]
        public MammaIntraopPraeparatkontrolle? IntraopPraeparatkontrolleEnumValue
        {
            get
            {
                return _intraopPraeparatkontrolle;
            }
            set
            {
                _intraopPraeparatkontrolle = value;
            }
        }

        [XmlIgnore]
        public bool IntraopPraeparatkontrolleEnumValueSpecified => _intraopPraeparatkontrolle.HasValue;


        /// <summary>
        /// Maximaler Durchmesser des invasiven Karzinoms in mm. 
        /// Bei mehreren Herden ist der größte Durchmesser anzugeben.
        /// </summary>
        [XmlElement("TumorgroesseInvasiv", Order = 7)]
        public string TumorgroesseInvasiv
        {
            get
            {
                return _tumorgroesseInvasiv;
            }
            set
            {
                _tumorgroesseInvasiv = value.ValidateOrThrow(ThreeDigitNumberValidator.Instance, _typeName, nameof(this.TumorgroesseInvasiv));
            }           
        }

        /// <summary>
        /// Maximaler Durchmesser des DCIS in mm, wenn kein invasiver Anteil vorliegt.
        /// </summary>
        [XmlElement("TumorgroesseDCIS", Order = 8)]
        public string TumorgroesseDCIS
        {
            get
            {
                return _tumorgroesseDCIS;
            }
            set
            {
                _tumorgroesseDCIS = value.ValidateOrThrow(ThreeDigitNumberValidator.Instance, _typeName, nameof(this.TumorgroesseDCIS)); ;
            }
        }
    }
}
   