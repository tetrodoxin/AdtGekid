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
        // <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

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
        [XmlEnum("3.3.+")]
        Item3_3Plus,
        [XmlEnum("3.3.-")]
        Item3_3Minus,
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
        Item4Plus,        
        [XmlEnum("4.-")]
        Item4Minus,        
        [XmlEnum("4.1.")]
        Item4_1,        
        [XmlEnum("4.1.+")]
        Item4_1Plus,        
        [XmlEnum("4.1.-")]
        Item4_1Minus,        
        [XmlEnum("4.2.")]
        Item4_2,        
        [XmlEnum("4.2.+")]
        Item4_2Plus,        
        [XmlEnum("4.2.-")]
        Item4_2Minus,        
        [XmlEnum("4.3.")]
        Item4_3,        
        [XmlEnum("4.3.+")]
        Item4_3Plus,        
        [XmlEnum("4.3.-")]
        Item4_3Minus,        
        [XmlEnum("4.4.")]
        Item4_4,        
        [XmlEnum("4.4.+")]
        Item4_4Plus,        
        [XmlEnum("4.4.-")]
        Item4_4Minus,        
        [XmlEnum("4.5.")]
        Item45,        
        [XmlEnum("4.5.+")]
        Item4_5Plus,        
        [XmlEnum("4.5.-")]
        Item4_5Minus,        
        [XmlEnum("4.6.")]
        Item4_6,        
        [XmlEnum("4.6.+")]
        Item4_6Plus,        
        [XmlEnum("4.6.-")]
        Item4_6Minus,        
        [XmlEnum("4.7.")]
        Item4_7,        
        [XmlEnum("4.8.")]
        Item4_8,        
        [XmlEnum("4.8.+")]
        Item4_8Plus,        
        [XmlEnum("4.8.-")]
        Item4_8Minus,        
        [XmlEnum("4.9.")]
        Item4_9,        
        [XmlEnum("4.9.+")]
        Item4_9Plus,        
        [XmlEnum("4.9.-")]
        Item4_9Minus,        
        [XmlEnum("5.")]
        Item5,        
        [XmlEnum("5.+")]
        Item5Plus,        
        [XmlEnum("5.-")]
        Item5Minus,        
        [XmlEnum("5.1.")]
        Item5_1,        
        [XmlEnum("5.1.+")]
        Item5_1Plus,        
        [XmlEnum("5.1.-")]
        Item5_1Minus,        
        [XmlEnum("5.2.")]
        Item5_2,        
        [XmlEnum("5.2.+")]
        Item5_2Plus,        
        [XmlEnum("5.2.-")]
        Item5_2Minus,        
        [XmlEnum("5.3.")]
        Item5_3,        
        [XmlEnum("5.3.+")]
        Item5_3Plus,        
        [XmlEnum("5.3.-")]
        Item5_3Minus,        
        [XmlEnum("5.4.")]
        Item5_4,        
        [XmlEnum("5.4.+")]
        Item5_4Plus,        
        [XmlEnum("5.4.-")]
        Item5_4Minus,        
        [XmlEnum("5.5.")]
        Item5_5,        
        [XmlEnum("5.5.+")]
        Item5_5Plus,        
        [XmlEnum("5.5.-")]
        Item5_5Minus,        
        [XmlEnum("5.6.")]
        Item5_6,        
        [XmlEnum("5.6.+")]
        Item5_6Plus,        
        [XmlEnum("5.6.-")]
        Item5_6Minus,        
        [XmlEnum("5.7.")]
        Item5_7,        
        [XmlEnum("5.7.+")]
        Item5_7Plus,        
        [XmlEnum("5.7.-")]
        Item5_7Minus,        
        [XmlEnum("5.7.1.")]
        Item5_7_1,        
        [XmlEnum("5.7.1.+")]
        Item5_7_1Plus,        
        [XmlEnum("5.7.1.-")]
        Item5_7_1Minus,        
        [XmlEnum("5.7.2.")]
        Item5_7_2,        
        [XmlEnum("5.7.2.+")]
        Item5_7_2Plus,        
        [XmlEnum("5.7.2.-")]
        Item5_7_2Minus,        
        [XmlEnum("5.8.")]
        Item5_8,        
        [XmlEnum("5.8.+")]
        Item5_8Plus,        
        [XmlEnum("5.8.-")]
        Item5_8Minus,        
        [XmlEnum("5.9.")]
        Item5_9,        
        [XmlEnum("5.9.+")]
        Item5_9Plus,        
        [XmlEnum("5.9.-")]
        Item5_9Minus,        
        [XmlEnum("5.10.")]
        Item5_10,        
        [XmlEnum("5.10.+")]
        Item5_10Plus,        
        [XmlEnum("5.10.-")]
        Item5_10Minus,        
        [XmlEnum("5.11.")]
        Item5_11,        
        [XmlEnum("5.11.+")]
        Item5_11Plus,        
        [XmlEnum("5.11.-")]
        Item5_11Minus,        
        [XmlEnum("5.12.")]
        Item5_12,        
        [XmlEnum("6.")]
        Item6,        
        [XmlEnum("6.1.")]
        Item6_1,        
        [XmlEnum("6.2.")]
        Item6_2,        
        [XmlEnum("6.3.")]
        Item6_3,        
        [XmlEnum("6.4.")]
        Item6_4,        
        [XmlEnum("6.5.")]
        Item6_5,        
        [XmlEnum("6.6.")]
        Item6_6,        
        [XmlEnum("6.7.")]
        Item6_7,        
        [XmlEnum("6.8.")]
        Item6_8,        
        [XmlEnum("6.9.")]
        Item6_9,        
        [XmlEnum("6.10.")]
        Item6_10,        
        [XmlEnum("6.11.")]
        Item6_11,        
        [XmlEnum("6.12.")]
        Item6_12,        
        [XmlEnum("6.13.")]
        Item6_13,        
        [XmlEnum("6.14.")]
        Item6_14,        
        [XmlEnum("6.15.")]
        Item6_15,        
        [XmlEnum("6.16.")]
        Item6_16,        
        [XmlEnum("7.")]
        Item7,        
        [XmlEnum("7.+")]
        Item7Plus,        
        [XmlEnum("7.-")]
        Item7Minus,        
        [XmlEnum("7.1.")]
        Item7_1,        
        [XmlEnum("7.2.")]
        Item7_2,        
        [XmlEnum("8.")]
        Item8,        
        [XmlEnum("8.1.")]
        Item8_1,        
        [XmlEnum("8.2.")]
        Item8_2,
    }


    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlungST_Seite_Zielgebiet", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungSeiteZielgebiet
    {
        // <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

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
        // <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

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
        // <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        Gy,

        GBq,
    }

    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTST_Ende_Grund", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum BestrahlungEndeGrund
    {
        // <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        A,

        E,

        V,

        P,

        S,

        U,
    }
}
