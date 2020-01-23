using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTST_Intention", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum StrahlentherapieIntention
    {
            
        K,

        P,

        S,

        X,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlungST_Zielgebiet", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum StrahlentherapieStellungOp
    {

        O,

        A,

        N,

        I,

        S,
    }    
}
