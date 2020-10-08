using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module
{

    public enum DarmRektumQualitaetTME
    {      
        /// <summary>
        /// Grad 1 (gut)
        /// </summary>
        [XmlEnum("1")]
        Grad1_gut = 1,

        /// <summary>
        /// Grad 2 (moderat)
        /// </summary>
        [XmlEnum("2")]
        Grad2_moderat = 2,

        /// <summary>
        /// Grad 3 (schlecht)
        /// </summary>
        [XmlEnum("3")]
        Grad3_schlecht = 3,

        /// <summary>
        /// PME durchgeführt
        /// </summary>
        [XmlEnum("P")]
        PME,

        /// <summary>
        /// Lokale Exzision durchgeführt
        /// </summary>
        [XmlEnum("L")]
        LokaleExzision,

        /// <summary>
        /// Andere Operation durchgeführt
        /// </summary>
        [XmlEnum("A")]
        AndereOP,

        /// <summary>
        /// Unbekannt
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

   
    /// <summary>
    /// Modalität der Eingriffsdurchführung bei Darm-OP
    /// </summary>
    public enum DarmArtEingriff
    {
        [XmlEnum("E")]
        Elektiv,

        [XmlEnum("N")]
        Notfall,

        [XmlEnum("U")]
        Unbekannt,
    }

    /// <summary>
    /// Präoperative Anzeichnung der Stomaposition bei Rektum
    /// </summary>
    public enum DarmRektumAnzeichnungStomaposition
    {
        /// <summary>
        /// Anzeichnung durchgeführt (D)
        /// </summary>
        [XmlEnum("D")]
        Durchgefuehrt,

        /// <summary>
        /// Anzeichnung nicht durchgeführt (N)
        /// </summary>
        [XmlEnum("N")]
        NichtDurchgefuehrt,

        /// <summary>
        /// Kein Stoma
        /// </summary>
        [XmlEnum("K")]
        KeinStoma,

        /// <summary>
        /// Stoma angelegt, Anzeichnung nicht bekannt (S)
        /// </summary>
        [XmlEnum("S")]
        StomaAngelegt,

        /// <summary>
        /// Unbekannt (U)
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }


    /// <summary>
    /// Rektum:
    /// Grad A(keine therapeutische Konsequenz)
    /// Grad B(Antibiotikagabe oder interventionelle Drainage oder transanale Lava-ge/Drainage)
    /// Grad C((Re)-Laparotomie)Anastomoseninsuffizienz nach elektivem Eingriff mit Anastomosenanlage
    /// </summary>
    public enum DarmRektumGradAnastomoseninsuffizienz
    {
        /// <summary>
        /// Wert = B
        /// </summary>
        [XmlEnum("B")]
        Grad_B,

        /// <summary>
        /// Wert = C
        /// </summary>
        [XmlEnum("C")]
        Grad_C,

        /// <summary>
        /// Wert = K
        /// </summary>
        [XmlEnum("K")]
        KeineInsuffizienzMaximalGradA,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,
    }

    /// <summary>
    /// Einstufung des Patienten nach der ASA-Klassifikation 
    /// bei präoperativer Untersuchung durch den Anästhesisten.
    /// </summary>
    public enum DarmKlassifizierungASA
    {
        /// <summary>
        /// normaler, ansonsten gesunder Patient (Wert = 1)
        /// </summary>
        [XmlEnum("1")]
        Grad1 = 1,

        /// <summary>
        /// Patient mit leichter Allgemeinerkrankung (Wert = 2)
        /// </summary>
        [XmlEnum("2")]
        Grad2 = 2,

        /// <summary>
        /// Patient mit schwerer Allgemeinerkrankung und Leistungseinschränkung
        /// (Wert = 3)
        /// </summary>
        [XmlEnum("3")]
        Grad3 = 3,

        /// <summary>
        /// Patient mit inaktivierender Allgemeinerkrankung, ständige Lebensbedro-hung
        /// (Wert = 4)
        /// </summary>
        [XmlEnum("4")]
        Grad4 = 4,

        /// <summary>
        /// moribunder Patient (Wert = 5)
        /// </summary>
        [XmlEnum("5")]
        Grad5 = 5,
    }

    /// <summary>
    /// Vorliegen einer Mutation im K-ras-Onkogen
    /// </summary>
    public enum DarmRASMutation
    {
        /// <summary>
        /// Wert = W
        /// </summary>
        [XmlEnum("W")]
        Wildtyp,

        /// <summary>
        /// Wert = M
        /// </summary>
        [XmlEnum("M")]
        Mutation,

        /// <summary>
        /// Wert = U
        /// </summary>
        [XmlEnum("U")]
        Unbekannt,

        /// <summary>
        /// Wert = N
        /// </summary>
        [XmlEnum("N")]
        NichtUntersucht,
    }
}
