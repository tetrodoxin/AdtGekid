using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Allgemeiner_Leistungszustand_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum AllgemeinerLeistungszustandTyp
    {           
        NotSpecified = 0,

        [XmlEnum("0")]
        Item0,
        
        [XmlEnum("1")]
        Item1,
        
        [XmlEnum("2")]
        Item2,
        
        [XmlEnum("3")]
        Item3,
        
        [XmlEnum("4")]
        Item4,

        [XmlEnum("U")]
        U,        

        [XmlEnum("10%")]
        Item10Percent,
        
        [XmlEnum("20%")]
        Item20Percent,
        
        [XmlEnum("30%")]
        Item30Percent,
        
        [XmlEnum("40%")]
        Item40Percent,
        
        [XmlEnum("50%")]
        Item50Percent,
        
        [XmlEnum("60%")]
        Item60Percent,
        
        [XmlEnum("70%")]
        Item70Percent,
        
        [XmlEnum("80%")]
        Item80Percent,
        
        [XmlEnum("90%")]
        Item90Percent,
        
        [XmlEnum("100%")]
        Item100Percent,
    }
}
