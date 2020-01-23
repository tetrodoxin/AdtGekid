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
        Item10,
        
        [XmlEnum("20%")]
        Item20,
        
        [XmlEnum("30%")]
        Item30,
        
        [XmlEnum("40%")]
        Item40,
        
        [XmlEnum("50%")]
        Item50,
        
        [XmlEnum("60%")]
        Item60,
        
        [XmlEnum("70%")]
        Item70,
        
        [XmlEnum("80%")]
        Item80,
        
        [XmlEnum("90%")]
        Item90,
        
        [XmlEnum("100%")]
        Item100,
    }
}
