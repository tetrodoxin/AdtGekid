using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungDiagnoseDiagnosesicherung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Diagnosesicherung
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        [XmlEnum("1")]
        Item1 = 1,
        
        [XmlEnum("2")]
        Item2 = 2,
        
        [XmlEnum("4")]
        Item4 = 4,
        
        [XmlEnum("5")]
        Item5 = 5,
        
        [XmlEnum("6")]
        Item6 = 6,

        [XmlEnum("7")]
        Item7 = 7,
        
        [XmlEnum("9")]
        Item9 = 9,
    }
}
