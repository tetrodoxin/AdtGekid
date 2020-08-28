using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AdtGekid.Module
{

    /// <summary>
    /// Prätherapeutischer Menopausenstatus der Patientin bei Mamma-Ca.
    /// Postmenopausal bedeutet mehr als ein Jahr keine Menstruationsblutung 
    /// oder Estradiol(E 2) und Follikelstimulierendes Hormon(FSH) im eindeutigen 
    /// post-menopausalen Bereich
    /// </summary>
    public enum MammaPraethMenospausenstatus
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Prä/Perimenopausal (Wert = 1)
        /// </summary>
        [XmlEnum("1")]
        PraeAndPerimenopausal = 1,
        
        /// <summary>
        /// Postmenopausal (Wert = 3)
        /// </summary>
        [XmlEnum("3")]
        Postmenopausal = 3,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

   /// <summary>
   /// Hormonrezeptor bei Mamma-Ca. (z.B. Ösrogen/Progesteron/HER2
   /// </summary>
    public enum MammaHormonrezeptor
    {

        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Positiv (Wert = P)
        /// </summary>
        [XmlEnum("P")]
        Positiv,

        /// <summary>
        /// Negativ (Wert = N)
        /// </summary>
        [XmlEnum("N")]
        Negativ,


        /// <summary>
        /// Unbekannt (Wert = U)
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

    /// <summary>
    /// Präoperative Drahtmarkierung durch Bildgebung gesteuert
    /// </summary>
    public enum MammaPraeopDrahtmarkierung
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Wert = M
        /// </summary>
        [XmlEnum("M")]
        Mammographie,

        /// <summary>
        /// Wert = S
        /// </summary>
        [XmlEnum("S")]
        Sonographie,

        /// <summary>
        /// MRT (Wert = T)
        /// </summary>
        [XmlEnum("T")]
        MRT,

        /// <summary>
        /// Wert = N
        /// </summary>
        [XmlEnum("N")]
        KeineMarkierungDurchBildgebung,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

    /// <summary>
    /// Intraoperatives Präparatröntgen/Sonographie
    /// </summary>
    public enum MammaIntraopPraeparatkontrolle
    {
        /// <summary>
        /// Default (keine XML-Repräsentation!)
        /// </summary>        
        NotSpecified = 0,

        /// <summary>
        /// Wert = M
        /// </summary>
        [XmlEnum("M")]
        Mammographie,

        /// <summary>
        /// Wert = S
        /// </summary>
        [XmlEnum("S")]
        Sonographie,

        /// <summary>
        /// Wert = N
        /// </summary>
        [XmlEnum("N")]
        Nein,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

}
