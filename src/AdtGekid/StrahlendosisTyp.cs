using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Strahlendosis_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public partial class StrahlendosisTyp
    {
        private StrahlendosisEinheit _einheit;

        [XmlElement("Dosis", Order = 1)]        
        public decimal Dosis { get; set; }

        [XmlIgnore]
        public string Einheit
        {
            get { return _einheit.ToString(); }
            set { _einheit = value.TryParseAsEnumOrThrow<StrahlendosisEinheit>(typeof(StrahlendosisEinheit).Name,nameof(this.Einheit)); }
        }

        /// <summary>
        /// Angabe der Dosiseinheit. Muss gefüllt sein 
        /// wenn Dosis vorhanden -> keine Leerwerte hier zulässig
        /// </summary>
        [XmlElement("Einheit", Order = 2)]
        public StrahlendosisEinheit EinheitEnumValue
        {
            get { return _einheit; }
            set { _einheit = value; }
        }
    }
}
