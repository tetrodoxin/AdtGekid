using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module.Prostata
{
    using Validation;

    /// <summary>
    /// Semiquantitative Abschätzung des Prozentsatzes der Gesamtkarzinomfläche/Gesamtstanzzylinderfläche der am schwersten befallenen Stanze
    /// Ausprägungen:
    /// natürliche Zahl in % (1 - 100)
    /// U = Unbekannt
    /// </summary>          
    public class ProstataCaBefallStanze
    {     
        private int? _percentage;
        private ProstataCaBefallStanzeEnum? _nonNumericValue;
        private string _typeName = nameof(ProstataCaBefallStanze);

        [XmlElement("Prozentzahl", typeof(string), DataType = "positiveInteger")]
        [XmlElement("U", typeof(ProstataCaBefallStanze))]
        public object Wert
        {
            get
            {
                if (_percentage.HasValue)
                {
                    return _percentage.Value;
                }                    
                else if (_nonNumericValue.HasValue)
                {
                    return _nonNumericValue.Value.ToXmlEnumAttributeName();
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    var strVal = value.ToString();
                    if (strVal.IsIntegerCompatible())
                    {
                        setPercentage(strVal.ParseNullableInt().Value);                        
                    }
                    else
                    {
                        setNonNumericValue(strVal);
                    }
                }                                
            }
        }

        [XmlIgnore]
        public int? Percentage
        {
            get { return _percentage; }
            set { _percentage = value?.BetweenOrThrow(0, 100); }
        }

        [XmlIgnore]
        public ProstataCaBefallStanzeEnum? NonNumericValue
        {
            get { return _nonNumericValue; }
            set { _nonNumericValue = value; }
        }  

        
        private void setPercentage(int value)
        {
            _percentage = value.BetweenOrThrow(0, 100);
        }

        private void setNonNumericValue(string value)
        {
            _nonNumericValue = value.TryParseAsEnumOrThrow<ProstataCaBefallStanzeEnum>(_typeName, nameof(this.Wert));
        }
    }
}
