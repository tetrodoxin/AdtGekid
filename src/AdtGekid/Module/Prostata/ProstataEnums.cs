using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module.Prostata
{
    [Serializable]
    public enum ProstataGleasonGradPrimaer
    {
        [XmlEnum("1")]
        Grad1 = 1,


        [XmlEnum("2")]
        Grad2 = 2,


        [XmlEnum("3")]
        Grad3 = 3,


        [XmlEnum("4")]
        Grad4 = 4,


        [XmlEnum("5")]
        Grad5 = 5,
    }

    [Serializable]
    public enum ProstataGleasonGradSekundaer
    {
        [XmlEnum("1")]
        Grad1 = 1,

        [XmlEnum("2")]
        Grad2 = 2,

        [XmlEnum("3")]
        Grad3 = 3,

        [XmlEnum("4")]
        Grad4 = 4,

        [XmlEnum("5")]
        Grad5 = 5,
    }
   
    public enum ProstataGleasonScoreErgebnis
    {

        [XmlEnum("2")]
        Grad2 = 2,

        [XmlEnum("3")]
        Grad3 = 3,

        [XmlEnum("4")]
        Grad4 = 4,

        [XmlEnum("5")]
        Grad5 = 5,


        [XmlEnum("6")]
        Grad6 = 6,


        [XmlEnum("7")]
        Grad7 = 7,


        [XmlEnum("7a")]
        Grad7a,


        [XmlEnum("7b")]
        Grad7b,


        [XmlEnum("8")]
        Grad8,


        [XmlEnum("9")]
        Grad9,


        [XmlEnum("10")]
        Grad10,
    }


    
    public enum ProstataAnlassGleasonScore
    {
        /// <summary>
        /// Wert = O
        /// </summary>
        [XmlEnum("O")]
        OP,

        /// <summary>
        /// Wert = S
        /// </summary>
        [XmlEnum("S")]
        Stanze,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

    [Serializable]
    public enum ProstataCaBefallStanzeEnum
    {
        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }
}
