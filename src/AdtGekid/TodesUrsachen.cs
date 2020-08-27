using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufTodMenge_Todesursache", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class MengeTodesursache
    {
        private Collection<string> _ursachenIcdCodes;
        private IcdVersionTyp? _ursachenIcdVersion;
        private string _typeName = typeof(MengeTodesursache).Name;

        [XmlElement("Todesursache_ICD", Order = 1)]
        public Collection<string> UrsachenIcdCodes
        {
            get { return _ursachenIcdCodes; }
            set { _ursachenIcdCodes = value.EnsureValidatedStringList().WithValidator(new StringValidatorByRegex(@"^[A-Z]\d\d(\.\d(\d)?)?$")); }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool UrsachenIcdCodesSpecified =>
                UrsachenIcdCodes != null && UrsachenIcdCodes.Count > 0;

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-10-GM Version
        /// </summary>
        [XmlIgnore]
        public string UrsachenIcdVersion
        {
            get { return _ursachenIcdVersion?.ToXmlEnumAttributeName(); }
            set 
            {
                if (!value.IsNothing())
                    _ursachenIcdVersion = value.TryParseAsEnumOrThrow<IcdVersionTyp>(_typeName, nameof(this.UrsachenIcdVersion), false); 
            }

        }
        
        [XmlElement("Todesursache_ICD_Version", Order =2)]
        public IcdVersionTyp? UrsachenIcdVersionEnumValue
        {
            get
            {
                return this._ursachenIcdVersion;
            }
            set
            {
                this._ursachenIcdVersion = value;
            }
        }

        [XmlIgnore]
        public bool UrsacheIcdVersionEnumValueSpecified => UrsachenIcdVersionEnumValue.HasValue;
        
    }
}
