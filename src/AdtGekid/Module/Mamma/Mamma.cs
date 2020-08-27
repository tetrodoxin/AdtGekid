using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AdtGekid.Validation;

namespace AdtGekid.Module.Mamma
{
    /// <summary>
    /// Enthält Daten zu einer Tumordiagnose.
    /// </summary>
    [Serializable()]
    [XmlType("Modul_Mamma_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public partial class ModulMamma
    {

        private MammaPraethMenospausenstatus? _praethMenopausenstatus;

        private MammaHormonrezeptor? _hormonrezeptorStatusOestrogen;

        private MammaHormonrezeptor? _hormonrezeptorStatusProgesteron;

        private MammaHormonrezeptor? _her2neuStatus;

        private MammaPraeopDrahtmarkierung? _praeopDrahtmarkierung;

        private MammaIntraopPraeparatkontrolle? _intraopPraeparatkontrolle;

        private string _tumorgroesseInvasiv;

        private string _tumorgroesseDCIS;

        private string _typeName = "ModulMamma";

        [XmlIgnore]
        public string PraethMenopausenstatus
        {
            get { return _praethMenopausenstatus.ToString(); }
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


        [XmlIgnore]
        public string HormonrezeptorStatusOestrogen 
        {
            get { return _hormonrezeptorStatusOestrogen.ToString(); }
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

        [XmlIgnore]
        public string Her2neuStatus
        {
            get { return _her2neuStatus.ToString(); }
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

        [XmlIgnore]
        public string PraeopDrahtmarkierung
        {
            get { return _praeopDrahtmarkierung.ToString(); }
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

        [XmlIgnore]
        public string IntraopPraeparatkontrolle 
        {
            get { return _intraopPraeparatkontrolle.ToString(); }
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
   