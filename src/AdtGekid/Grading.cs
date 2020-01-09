using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Histologie_TypGrading", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Grading
    {
        [XmlEnum("0")]
        Item0,
        
        [XmlEnum("1")]
        Item1,
        
        [XmlEnum("2")]
        Item2,
        
        [XmlEnum("3")]
        Item3,

        [XmlEnum("4")]
        Item4,

        X,

        L,

        M,

        H,

        B,

        U,

        T,
    }
}
