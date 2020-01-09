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
        [XmlEnum("31")]
        Item31,
     
        [XmlEnum("32")]
        Item32,
    }
}
