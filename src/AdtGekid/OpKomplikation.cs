using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungOPOP_Komplikation", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum OpKomplikation
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        N,
        
        U,
        
        ABD,
        
        ABS,
        
        ASF,
        
        ANI,
        
        AEP,
        
        ALR,
        
        ANS,
        
        AEE,
        
        API,
        
        BIF,
        
        BOG,
        
        BOE,
        
        BSI,
        
        CHI,
        
        DAI,
        
        DPS,
        
        DIC,
        
        DEP,
        
        DLU,
        
        DSI,
        
        ENF,
        
        GER,
        
        HEM,
        
        HUR,
        
        HAE,
        
        HFI,
        
        HNK,
        
        HZI,
        
        HRS,
        
        HNA,
        
        HOP,
        
        HYB,
        
        HYF,
        
        IFV,
        
        KAS,
        
        KES,
        
        KIM,
        
        KRA,
        
        KDS,
        
        LEV,
        
        LOE,
        
        LYF,
        
        LYE,
        
        MES,
        
        MIL,
        
        MED,
        
        MAT,
        
        MYI,
        
        RNB,
        
        NAB,
        
        NIN,
        
        OES,
        
        OSM,
        
        PAF,
        
        PIT,
        
        PAB,
        
        PPA,
        
        PAV,
        
        PER,
        
        PLB,
        
        PEY,
        
        PLE,
        
        PMN,
        
        PNT,
        
        PDA,
        
        PAE,
        
        RPA,
        
        RIN,
        
        SKI,
        
        SES,
        
        SFH,
        
        SON,
        
        STK,
        
        TZP,
        
        TIA,
        
        TRZ,
        
        WUH,
        
        WSS,
    }
}
