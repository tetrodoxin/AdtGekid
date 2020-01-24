using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Nebenwirkung_TypNebenwirkung_Grad", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum NebenwirkungGrad
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

              
        [XmlEnum("3")]
        Item3 = 3,
        
        [XmlEnum("4")]
        Item4 = 4,
        
        [XmlEnum("5")]
        Item5 = 5,

        K,

        U,
    }

    [Serializable()]
    [XmlType("Nebenwirkung_TypNebenwirkung_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum NebenwirkungVersion
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        [XmlEnum("4")]
        Item4 = 4,
                      
        [XmlEnum("5.0")]
        Item5_0 = 5,

        [XmlEnum("4.03")]
        Item4_0_3,

        [XmlEnum]
        Sonstige,
    }
}
