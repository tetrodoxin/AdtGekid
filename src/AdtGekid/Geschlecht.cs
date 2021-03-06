﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientPatienten_StammdatenPatienten_Geschlecht", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum Geschlecht
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        NotSpecified = 0,

        M,

        
        W,

        
        S,

        
        U,
    }

}
