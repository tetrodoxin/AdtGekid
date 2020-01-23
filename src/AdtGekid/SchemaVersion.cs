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
        Item_2_0_0
    }
}
