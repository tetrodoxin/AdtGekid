using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Seitenlokalisation_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Seitenlokalisation_Typ
    {
        
        L,
        
        R,
        
        B,
        
        M,
        
        U,
        
        T,
    }
}
