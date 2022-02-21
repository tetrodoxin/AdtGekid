using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDSchema_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SchemaVersion
    {       
        [XmlEnum("2.0.0")]
        Item_2_0_0,
        [XmlEnum("2.0.1")]
        Item_2_0_1,
        [XmlEnum("2.1.0")]
        Item_2_1_0,
        [XmlEnum("2.1.1")]
        Item_2_1_1,
        [XmlEnum("2.2.1")]
        Item_2_2_1,
    }
}
