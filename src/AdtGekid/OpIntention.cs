using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungOPOP_Intention", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum OpIntention
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        K,
        
        P,
        
        D,
        
        R,
        
        S,
        
        X,
    }
}
