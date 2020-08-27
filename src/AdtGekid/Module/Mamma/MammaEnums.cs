using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AdtGekid.Module.Mamma
{   
    public enum MammaPraethMenospausenstatus
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Prä/Perimenopausal
        /// </summary>
        [XmlEnum("1")]
        PraeAndPerimenopausal = 1,
        
        /// <summary>
        /// Postmenopausal
        /// </summary>
        [XmlEnum("3")]
        Postmenopausal = 3,
        
        U,
    }

   
    public enum MammaHormonrezeptor
    {

        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Positiv
        /// </summary>
        [XmlEnum("P")]
        P,

        /// <summary>
        /// Negativ
        /// </summary>
        [XmlEnum("N")]
        N,


        /// <summary>
        /// Unbekannt
        /// </summary>
        [XmlEnum("U")]
        U,
    }

    
    public enum MammaPraeopDrahtmarkierung
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        [XmlEnum("M")]
        M,

        [XmlEnum("S")]
        S,

        [XmlEnum("T")]
        T,

        [XmlEnum("N")]
        N,

        [XmlEnum("U")]
        U,
    }

   
    public enum MammaIntraopPraeparatkontrolle
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        [XmlEnum("M")]
        M,

        [XmlEnum("S")]
        S,

        [XmlEnum("N")]
        N,

        [XmlEnum("U")]
        U,
    }

}
