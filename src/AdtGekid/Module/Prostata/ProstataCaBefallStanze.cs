using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module.Prostata
{
    using Validation;

    /// <summary>
    /// Semiquantitative Abschätzung des Prozentsatzes der Gesamtkarzinomfläche/Gesamtstanzzylinderfläche 
    /// der am schwersten befallenen Stanze bei Prostata-Ca.
    /// Ausprägungen:
    /// natürliche Zahl in % (1 - 100)
    /// U = Unbekannt
    /// </summary>   
    [Serializable()]
    public class ProstataCaBefallStanze
    {     
        private int? _percentage;
        private ProstataCaBefallStanzeEnum? _nonNumericValue;
        private string _typeName = nameof(ProstataCaBefallStanze);
        private object _value;

        public ProstataCaBefallStanze()
        {
            _percentage = null;
            _nonNumericValue = null;
            _value = null;
        }

        public ProstataCaBefallStanze(int percentage) : this()
        {
            _percentage = percentage;
            _value = percentage;
        }

        public ProstataCaBefallStanze(ProstataCaBefallStanzeEnum nonNumericValue) : this()
        {
            _nonNumericValue = nonNumericValue;
            _value = nonNumericValue;
        }

        /// <summary>
        /// Prozent-Wert oder Unbekannt-Wert für die Ca-Befall-Stanze(Property für XML-Choice)
        /// </summary>
        [XmlElement("Prozentzahl", typeof(int))]
        [XmlElement("U", typeof(ProstataCaBefallStanzeEnum))]
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Angabe für Prozentwert <see cref="int"/>
        /// </summary>
        [XmlIgnore]
        public int? Percentage
        {
            get { return _percentage; }
            set 
            {               
                if (value != null)
                    setPercentage(value.Value);
            }
        }

        /// <summary>
        /// Angabe <see cref="ProstataCaBefallStanzeEnum.Unbekannt"/>
        /// </summary>
        [XmlIgnore]
        public ProstataCaBefallStanzeEnum? NonNumericValue
        {
            get { return _nonNumericValue; }
            set 
            { 
                _nonNumericValue = value;
                _value = _nonNumericValue;
            }
        }  

        
        private void setPercentage(int value)
        {
            _percentage = value.BetweenOrThrow(0, 100);
            _value = _percentage;
        }

        private void setNonNumericValue(string value)
        {
            _nonNumericValue = value.TryParseAsEnumOrThrow<ProstataCaBefallStanzeEnum>(_typeName, nameof(this.Value));
            _value = _nonNumericValue;
        }
    }
}
