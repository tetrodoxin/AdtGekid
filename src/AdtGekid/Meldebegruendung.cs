using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungMeldebegruendung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Meldebegruendung
    {
        
        I,
        
        A,

        D,

        W,

        V,
    }
}
