using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSYSTSYST_Intention", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SystemTherapieIntention
    {

        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        K,
       
        P,
       
        S,
       
        X,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSYSTSYST_Stellung_OP", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SystemTherapieStellungOp
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        O,
       
        A,
       
        N,
       
        I,
       
        S,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSYSTSYST_Therapieart", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SystemTherapieart
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        CH,
        
        HO,       

        IM,       

        KM,       

        WS,       

        AS,       

        ZS,       

        SO,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSYSTSYST_Ende_Grund", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum SystemTherapieEndeGrund
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        A,
       
        E,
       
        V,
       
        R,
       
        P,
       
        S,
       
        U,
    }
}
