using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Seitenlokalisation_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SeitenlokalisationTyp
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,
        
        L,
        
        R,
        
        B,
        
        M,
        
        U,
        
        T,
    }
}
