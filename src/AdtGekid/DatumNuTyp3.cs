using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid
{
    /// <summary>
    /// ToDo: For future use mit Anpassung
    /// Datentyp für DatumNU-Typen nach ADT_GEKID
    /// (z.B. für Datum Studienrekrutierung/Sozialdienstkontakt)
    /// Dient der geforderten Formatierung und der Vermeidung
    /// des Verwendens von <see cref="string"/> für Daten.
    /// </summary>   
    /// <seealso cref="System.IEquatable{AdtGekid.DatumNuTyp}" />
    [Serializable()]
    [XmlType("Datum_NU_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class DatumNuTyp
    {
        private object _value;
        private DateTime? _date;
        private DatumNuNonNumericValues? _nonNumericValue;


        public DatumNuTyp()
        {
            _date = null;
            _nonNumericValue = null;
            _value = null;
        }

        public DatumNuTyp(DateTime date)
        {
            _date = date;
            _value = date;
        }

        public DatumNuTyp(DatumNuNonNumericValues nonNumericValues)
        {
            _nonNumericValue = nonNumericValues;
            _value = nonNumericValues;
        }

        [XmlElement("Datum", typeof(DateTime), DataType = "date")]
        [XmlElement("NU", typeof(DatumNuNonNumericValues))]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        [XmlIgnore]
        public DateTime? Datum
        {
            get { return _date; }
            set 
            { 
                _date = value;
                _value = value;
            }
        }

        [XmlIgnore]
        public DatumNuNonNumericValues? NonNumericValue
        {
            get { return _nonNumericValue; }
            set
            {
                _nonNumericValue = value;
                _value = value;
            }
        }
    }
}
