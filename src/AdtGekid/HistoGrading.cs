using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

using System.ComponentModel;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("Histologie_TypGrading", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum HistoGrading
    {        

        [XmlEnum("0")]
        Item0 = 0,
        
        [XmlEnum("1")]
        Item1 = 1,
        
        [XmlEnum("2")]
        Item2 = 2,
        
        [XmlEnum("3")]
        Item3 = 3,

        [XmlEnum("4")]
        Item4 = 4,

        X,

        L,

        M,

        H,

        B,

        U,

        T,
    }
}
