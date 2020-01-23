using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlungST_Zielgebiet", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungZielgebiet
    {        
        [XmlEnum("1.")]
        Item1,        
        [XmlEnum("1.1.")]
        Item1_1,        
        [XmlEnum("1.2.")]
        Item1_2,        
        [XmlEnum("1.3.")]
        Item1_3,        
        [XmlEnum("2.")]
        Item2,        
        [XmlEnum("2.+")]
        Item2Plus,        
        [XmlEnum("2.-")]
        Item2Minus,        
        [XmlEnum("2.1.")]
        Item2_1,        
        [XmlEnum("2.1.+")]
        Item2_1Plus,        
        [XmlEnum("2.1.-")]
        Item2_1Minus,        
        [XmlEnum("2.2.")]
        Item2_2,        
        [XmlEnum("2.2.+")]
        Item2_2Plus,        
        [XmlEnum("2.2.-")]
        Item2_2Minus,        
        [XmlEnum("2.3.")]
        Item2_3,        
        [XmlEnum("2.3.+")]
        Item2_3Plus,        
        [XmlEnum("2.3.-")]
        Item2_3Minus,        
        [XmlEnum("2.4.")]
        Item2_4,        
        [XmlEnum("2.4.+")]
        Item2_4Plus,        
        [XmlEnum("2.4.-")]
        Item2_4Minus,        
        [XmlEnum("2.5.")]
        Item2_5,        
        [XmlEnum("2.5.+")]
        Item2_5Plus,        
        [XmlEnum("2.5.-")]
        Item2_5Minus,        
        [XmlEnum("2.6.")]
        Item2_6,        
        [XmlEnum("2.6.+")]
        Item2_6Plus,        
        [XmlEnum("2.6.-")]
        Item2_6Minus,        
        [XmlEnum("2.7.")]
        Item2_7,        
        [XmlEnum("2.7.+")]
        Item2_7Plus,        
        [XmlEnum("2.7.-")]
        Item2_7Minus,        
        [XmlEnum("2.8.")]
        Item2_8,        
        [XmlEnum("2.8.+")]
        Item2_8Plus,        
        [XmlEnum("2.8.-")]
        Item2_8Minus,        
        [XmlEnum("2.9.")]
        Item2_9,        
        [XmlEnum("3.")]
        Item3,        
        [XmlEnum("3.+")]
        Item3Plus,        
        [XmlEnum("3.-")]
        Item3Minus,        
        [XmlEnum("3.1.")]
        Item3_1,        
        [XmlEnum("3.1.+")]
        Item3_1Plus,        
        [XmlEnum("3.1.-")]
        Item3_1Minus,        
        [XmlEnum("3.2.")]
        Item3_2,        
        [XmlEnum("3.2.+")]
        Item3_2Plus,        
        [XmlEnum("3.2.-")]
        Item3_2Minus,        
        [XmlEnum("3.3.")]
        Item3_3,        
        [XmlEnum("3.4.")]
        Item3_4,        
        [XmlEnum("3.4.+")]
        Item3_4Plus,        
        [XmlEnum("3.4.-")]
        Item3_4Minus,        
        [XmlEnum("3.5.")]
        Item3_5,        
        [XmlEnum("3.5.+")]
        Item3_5Plus,        
        [XmlEnum("3.5.-")]
        Item3_5Minus,        
        [XmlEnum("3.6.")]
        Item3_6,        
        [XmlEnum("3.6.+")]
        Item3_6Plus,        
        [XmlEnum("3.6.-")]
        Item3_6Minus,        
        [XmlEnum("3.7.")]
        Item3_7,        
        [XmlEnum("4.")]
        Item4,        
        [XmlEnum("4.+")]
        Item41,        
        [XmlEnum("4.-")]
        Item42,        
        [XmlEnum("4.1.")]
        Item411,        
        [XmlEnum("4.1.+")]
        Item412,        
        [XmlEnum("4.1.-")]
        Item413,        
        [XmlEnum("4.2.")]
        Item421,        
        [XmlEnum("4.2.+")]
        Item422,        
        [XmlEnum("4.2.-")]
        Item423,        
        [XmlEnum("4.3.")]
        Item43,        
        [XmlEnum("4.3.+")]
        Item431,        
        [XmlEnum("4.3.-")]
        Item432,        
        [XmlEnum("4.4.")]
        Item44,        
        [XmlEnum("4.4.+")]
        Item441,        
        [XmlEnum("4.4.-")]
        Item442,        
        [XmlEnum("4.5.")]
        Item45,        
        [XmlEnum("4.5.+")]
        Item451,        
        [XmlEnum("4.5.-")]
        Item452,        
        [XmlEnum("4.6.")]
        Item46,        
        [XmlEnum("4.6.+")]
        Item461,        
        [XmlEnum("4.6.-")]
        Item462,        
        [XmlEnum("4.7.")]
        Item47,        
        [XmlEnum("4.8.")]
        Item48,        
        [XmlEnum("4.8.+")]
        Item481,        
        [XmlEnum("4.8.-")]
        Item482,        
        [XmlEnum("4.9.")]
        Item49,        
        [XmlEnum("4.9.+")]
        Item491,        
        [XmlEnum("4.9.-")]
        Item492,        
        [XmlEnum("5.")]
        Item5,        
        [XmlEnum("5.+")]
        Item51,        
        [XmlEnum("5.-")]
        Item52,        
        [XmlEnum("5.1.")]
        Item511,        
        [XmlEnum("5.1.+")]
        Item512,        
        [XmlEnum("5.1.-")]
        Item513,        
        [XmlEnum("5.2.")]
        Item521,        
        [XmlEnum("5.2.+")]
        Item522,        
        [XmlEnum("5.2.-")]
        Item523,        
        [XmlEnum("5.3.")]
        Item53,        
        [XmlEnum("5.3.+")]
        Item531,        
        [XmlEnum("5.3.-")]
        Item532,        
        [XmlEnum("5.4.")]
        Item54,        
        [XmlEnum("5.4.+")]
        Item541,        
        [XmlEnum("5.4.-")]
        Item542,        
        [XmlEnum("5.5.")]
        Item55,        
        [XmlEnum("5.5.+")]
        Item551,        
        [XmlEnum("5.5.-")]
        Item552,        
        [XmlEnum("5.6.")]
        Item56,        
        [XmlEnum("5.6.+")]
        Item561,        
        [XmlEnum("5.6.-")]
        Item562,        
        [XmlEnum("5.7.")]
        Item57,        
        [XmlEnum("5.7.+")]
        Item571,        
        [XmlEnum("5.7.-")]
        Item572,        
        [XmlEnum("5.7.1.")]
        Item5711,        
        [XmlEnum("5.7.1.+")]
        Item5712,        
        [XmlEnum("5.7.1.-")]
        Item5713,        
        [XmlEnum("5.7.2.")]
        Item5721,        
        [XmlEnum("5.7.2.+")]
        Item5722,        
        [XmlEnum("5.7.2.-")]
        Item5723,        
        [XmlEnum("5.8.")]
        Item58,        
        [XmlEnum("5.8.+")]
        Item581,        
        [XmlEnum("5.8.-")]
        Item582,        
        [XmlEnum("5.9.")]
        Item59,        
        [XmlEnum("5.9.+")]
        Item591,        
        [XmlEnum("5.9.-")]
        Item592,        
        [XmlEnum("5.10.")]
        Item510,        
        [XmlEnum("5.10.+")]
        Item5101,        
        [XmlEnum("5.10.-")]
        Item5102,        
        [XmlEnum("5.11.")]
        Item5111,        
        [XmlEnum("5.11.+")]
        Item5112,        
        [XmlEnum("5.11.-")]
        Item5113,        
        [XmlEnum("5.12.")]
        Item5121,        
        [XmlEnum("6.")]
        Item6,        
        [XmlEnum("6.1.")]
        Item61,        
        [XmlEnum("6.2.")]
        Item62,        
        [XmlEnum("6.3.")]
        Item63,        
        [XmlEnum("6.4.")]
        Item64,        
        [XmlEnum("6.5.")]
        Item65,        
        [XmlEnum("6.6.")]
        Item66,        
        [XmlEnum("6.7.")]
        Item67,        
        [XmlEnum("6.8.")]
        Item68,        
        [XmlEnum("6.9.")]
        Item69,        
        [XmlEnum("6.10.")]
        Item610,        
        [XmlEnum("6.11.")]
        Item611,        
        [XmlEnum("6.12.")]
        Item612,        
        [XmlEnum("6.13.")]
        Item613,        
        [XmlEnum("6.14.")]
        Item614,        
        [XmlEnum("6.15.")]
        Item615,        
        [XmlEnum("6.16.")]
        Item616,        
        [XmlEnum("7.")]
        Item7,        
        [XmlEnum("7.+")]
        Item71,        
        [XmlEnum("7.-")]
        Item72,        
        [XmlEnum("7.1.")]
        Item711,        
        [XmlEnum("7.2.")]
        Item721,        
        [XmlEnum("8.")]
        Item8,        
        [XmlEnum("8.1.")]
        Item81,        
        [XmlEnum("8.2.")]
        Item82,
    }


    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlungST_Seite_Zielgebiet", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungSeiteZielgebiet
    {
      
        L,  
        
        R,      

        B,      

        M,      

        U,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlungST_Applikationsart", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungApplikationsart
    {        
        P,
        
        PRCJ,
        
        PRCN,
        
        K,
        
        KHDR,
        
        KPDR,
        
        KLDR,
        
        I,
        
        IHDR,
        
        IPDR,
        
        ILDR,
        
        M,
        
        MSIRT,
        
        MPRRT,
        
        S,    }

    [Serializable()]
    [XmlType("Strahlendosis_TypEinheit", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum StrahlendosisEinheit
    {
      
        Gy,

        GBq,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTST_Ende_Grund", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungEndeGrund
    {
        A,

        E,

        V,

        P,

        S,

        U,
    }
}
