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
    public partial class ModulAllgemein
    {
        /// <summary>
        /// Datum des ersten Sozialdienstkontaktes im Rahmen der Erst- oder Rezidivbehandlung
        /// </summary>
        public DatumTyp DatumSozialdienstkontakt { get; set; }


        /// <summary>
        /// Teilnahme an einer Studie mit Ethikvotum
        /// </summary>
        public DatumTyp DatumStudienrekrutierung { get; set; }     
    }

}
