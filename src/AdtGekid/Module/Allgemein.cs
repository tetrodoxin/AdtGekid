using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module
{

    /// <summary>
    /// Enthält allgemeine Daten für alle Entitäten
    /// </summary>
    [Serializable()]
    [XmlType("Modul_Allgemein_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class ModulAllgemein
    {
        /// <summary>
        /// Datum des ersten Sozialdienstkontaktes im Rahmen der Erst- oder Rezidivbehandlung
        /// </summary>        
        [XmlElement("DatumSozialdienstkontakt", Order = 1)]
        public DatumNuTyp DatumSozialdienstkontakt { get; set; }


        /// <summary>
        /// Datum der Teilnahme an einer Studie mit Ethikvotum
        /// </summary>
        [XmlElement("DatumStudienrekrutierung", Order = 2)]
        public DatumNuTyp DatumStudienrekrutierung { get; set; }     
    }

}
