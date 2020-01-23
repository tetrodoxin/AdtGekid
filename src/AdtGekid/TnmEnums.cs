using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AdtGekid
{
    /// <summary>
    /// Aufzählung für die Angabe der TNM-Version
    /// </summary>
    [Serializable()]
    [XmlType("TNM_TypTNM_Version", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmVersion
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("6")]
        Item6 = 6,

        [XmlEnum("7")]
        Item7 = 7,

        [XmlEnum("8")]
        Item8 = 8
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_y_Symbol", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmSymbolY
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("y")]
        y,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_r_Symbol", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmSymbolR
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("r")]
        r,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_a_Symbol", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmSymbolA
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("a")]
        a,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_c_p_u_Praefix_T", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmPrefixesForT
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("c")]
        c,

        [XmlEnum("p")]
        p,

        [XmlEnum("u")]
        u,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_c_p_u_Praefix_N", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmPrefixesForN
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("c")]
        c,

        [XmlEnum("p")]
        p,

        [XmlEnum("u")]
        u,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_c_p_u_Praefix_M", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmPrefixesForM
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("c")]
        c,

        [XmlEnum("p")]
        p,

        [XmlEnum("u")]
        u,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_L", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmCategoryL
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("LX")]
        LX,

        [XmlEnum("L0")]
        L0,

        [XmlEnum("L1")]
        L1,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_V", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmCategoryV
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("VX")]
        VX,

        [XmlEnum("V0")]
        V0,

        [XmlEnum("V1")]
        V1,

        [XmlEnum("V2")]
        V2,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_Pn", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmCategoryPn
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("PnX")]
        PnX,

        [XmlEnum("Pn0")]
        Pn0,

        [XmlEnum("Pn1")]
        Pn1,
    }

    [Serializable()]
    [XmlType("TNM_TypTNM_S", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public enum TnmCategoryS
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>
        [XmlIgnore]
        NotSpecified = 0,

        [XmlEnum("SX")]
        SX,

        [XmlEnum("S0")]
        S0,

        [XmlEnum("S1")]
        S1,

        [XmlEnum("S2")]
        S2,

        [XmlEnum("S3")]
        S3,
    }
}
