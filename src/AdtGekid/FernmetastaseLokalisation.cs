using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Menge_FM_TypFernmetastaseFM_Lokalisation", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum FernmetastaseLokalisation
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>      
        NotSpecified = 0,

        PUL,

        OSS,

        HEP,

        BRA,

        LYM,

        MAR,

        PLE,

        PER,

        ADR,

        SKI,

        OTH,

        GEN
    }
}