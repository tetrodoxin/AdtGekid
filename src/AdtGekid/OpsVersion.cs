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

        [XmlEnum("2004")]
        Item2004 = 2004,

        [XmlEnum("2005")]
        Item2005 = 2005,

        [XmlEnum("2006")]
        Item2006 = 2006,

        [XmlEnum("2007")]
        Item2007 = 2007,

        [XmlEnum("2008")]
        Item2008 = 2008,

        [XmlEnum("2009")]
        Item2009 = 2009,

        [XmlEnum("2010")]
        Item2010 = 2010,

        [XmlEnum("2011")]
        Item2011 = 2011,

        [XmlEnum("2012")]
        Item2012 = 2012,              
        
        [XmlEnum("2013")]
        Item2013 = 2013,
    
        [XmlEnum("2014")]
        Item2014 = 2014,
    
        [XmlEnum("2015")]
        Item2015 = 2015,
    
        [XmlEnum("2016")]
        Item2016 = 2016,
    
        [XmlEnum("2017")]
        Item2017 = 2017,
    
        [XmlEnum("2018")]
        Item2018 = 2018,

        [XmlEnum("2019")]
        Item2019 = 2019,

        [XmlEnum("2020")]
        Item2020 = 2020,

        [XmlEnum("2021")]
        Item2021 = 2021,

        [XmlEnum("2022")]
        Item2022 = 2022
    }
}
