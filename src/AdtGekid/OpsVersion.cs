using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungOPOP_OPS_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum OpsVersion
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        /// <remarks/>
        [XmlEnum("2013")]
        Item2013 = 2013,
    
        /// <remarks/>
        [XmlEnum("2014")]
        Item2014 = 2014,
    
        /// <remarks/>
        [XmlEnum("2015")]
        Item2015 = 2015,
    
        /// <remarks/>
        [XmlEnum("2016")]
        Item2016 = 2016,
    
        /// <remarks/>
        [XmlEnum("2017")]
        Item2017 = 2017,
    
        /// <remarks/>
        [XmlEnum("2018")]
        Item2018 = 2018,
    }
}
