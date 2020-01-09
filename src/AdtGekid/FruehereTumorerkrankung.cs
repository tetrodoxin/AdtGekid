using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{

    // <summary>
    /// Enthält Daten zur Diagnose einer frühereren Tumorerkrankung
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungDiagnoseFruehere_Tumorerkrankung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class FruehereTumorerkrankung
    {

        private string _freitext;

        private IcdTyp _icdCode;

        private IcdVersionTyp _icdVersion;

        private bool _icdVersionSpecified;

        private DatumTyp _diagnoseDatum;

        private string _entity = typeof(FruehereTumorerkrankung).Name;


        [XmlElement("Freitext", Order = 1)]
        public string Freitext
        {
            get
            {
                return _freitext;
            }
            set
            {
                _freitext = value.ValidateMaxLength(500, _entity, nameof(this.Freitext));
            }
        }

        /// <summary>
        /// Kodierung einer meldepflichtigen Tumorerkrankung nach der aktuellen ICD-10-GM
        /// </summary>
        [XmlElement("ICD_Code", Order = 2)]
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
            get { return _icdVersion.ToString(); }
            set { _icdVersion = value.TryParseAsEnumOrThrow<IcdVersionTyp>(_entity, nameof(this.IcdVersion)); }

        }

        [XmlElement("ICD_Version", Order = 3)]
        public IcdVersionTyp IcdVersionEnumValue
        {
            get { return _icdVersion; }
            set { _icdVersion = value; }
        }


        [XmlIgnore()]
        public bool IcdVersionSpecified
        {
            get
            {
                return _icdVersionSpecified;
            }
            set
            {
                _icdVersionSpecified = value;
            }
        }

        [XmlElement("Diagnosedatum", Order = 4)]
        public DatumTyp Diagnosedatum
        {
            get
            {
                return _diagnoseDatum;
            }
            set
            {
                _diagnoseDatum = value;
            }
        }
    }
}
