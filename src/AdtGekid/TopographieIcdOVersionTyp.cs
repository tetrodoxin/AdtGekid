using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungDiagnosePrimaertumor_Topographie_ICD_O_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TopographieIcdOVersionTyp
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        [XmlEnum("31")]
        ICD_O_3_Auflage1_2003 = 31,
     
        [XmlEnum("32")]
        ICD_O_3_Revision_1_2014 = 32,
        
        [XmlEnum("33")]
        ICD_O_3_Revision_2_2019 = 33,
    }
}
