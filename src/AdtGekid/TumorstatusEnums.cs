using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufGesamtbeurteilung_Tumorstatus", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TumorstatusGesamt
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        V,        

        T,        

        K,        

        P,        

        D,        

        B,        

        R,        

        U,        

        X,
    }



    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufVerlauf_Lokaler_Tumorstatus", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TumorstatusLokal
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        K,
        
        T,
        
        P,
        
        N,
        
        R,
        
        F,
        
        U,
        
        X,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufVerlauf_Tumorstatus_Lymphknoten", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TumorstatusLymphknoten
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        K,
        
        T,
        
        P,
        
        N,
        
        R,
        
        F,
        
        U,
        
        X,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungVerlaufVerlauf_Tumorstatus_Fernmetastasen", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TumorstatusFernmetastasen
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        K,
        
        M,
        
        T,
        
        P,
        
        N,
        
        R,
        
        F,
        
        U,
        
        X,
    }
}
