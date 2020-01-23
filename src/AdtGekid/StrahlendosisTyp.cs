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
        [XmlElement("Dosis", Order = 1)]        
        public decimal Dosis { get; set; }

        [XmlElement("Einheit", Order = 2)]
        public StrahlendosisEinheit Einheit { get; set; }
    }
}
