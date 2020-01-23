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
        [XmlIgnore]
        NotSpecified = 0,

        K,
        
        [XmlEnum("3")]
        Item3,
        
        [XmlEnum("4")]
        Item4,
        
        [XmlEnum("5")]
        Item5,
        
        U,
    }

    [Serializable()]
    [XmlType("Nebenwirkung_TypNebenwirkung_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum NebenwirkungVersion
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("4")]
        Item4,
        
        [XmlEnum("4.03")]
        Item403,
        
        [XmlEnum("5.0")]
        Item50,
        
        Sonstige,
    }
}
