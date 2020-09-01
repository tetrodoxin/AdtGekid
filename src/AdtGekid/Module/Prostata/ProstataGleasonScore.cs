using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace AdtGekid.Module.Prostata
{

    /// <summary>
    /// Enthält Daten über den Gleason-Score, der bei Prostata-Ca. therapieentscheidend ist
    /// </summary>
    [Serializable]    
    public class ProstataGleasonScore
    {
        private ProstataGleasonGradPrimaer? _gleasonGradPrimaer;

        private ProstataGleasonGradSekundaer? _gleasonGradSekundaer;

        private ProstataGleasonScoreErgebnis? _gleasonScoreErgebnis;

        private string _typeName = nameof(ProstataGleasonScore);


        /// <summary>
        /// Primärer Gleason Grad zum Gleason-Score (1-5)
        /// </summary>
        [XmlIgnore]
        public string GleasonGradPrimaer
        {
            get 
            { 
                return _gleasonGradPrimaer?.ToString("d"); 
            }
            set
            {
                if (!value.IsNothing())
                    _gleasonGradPrimaer = value.TryParseAsEnumOrThrow<ProstataGleasonGradPrimaer>(_typeName, nameof(GleasonGradPrimaer));
            }
        }

        [XmlElement("GleasonGradPrimaer", Order = 1)]
        public ProstataGleasonGradPrimaer? GleasonGradPrimaerEnumValue
        {
            get
            {
                return this._gleasonGradPrimaer;
            }
            set
            {
                this._gleasonGradPrimaer = value;
            }
        }


        [XmlIgnore]
        public bool GleasonGradPrimaerEnumValueSpecified => _gleasonGradPrimaer.HasValue;


        /// <summary>
        /// Sekundärer Gleason Grad zum Gleason-Score (1-5)
        /// </summary>
        [XmlIgnore]
        public string GleasonGradSekundaer
        {
            get { return _gleasonGradSekundaer?.ToString("d"); }
            set
            {
                if (!value.IsNothing())
                    _gleasonGradSekundaer = value.TryParseAsEnumOrThrow<ProstataGleasonGradSekundaer>(_typeName, nameof(GleasonGradSekundaer));
            }
        }

        [XmlElement("GleasonGradSekundaer", Order = 2)]
        public ProstataGleasonGradSekundaer? GleasonGradSekundaerEnumValue
        {
            get
            {
                return _gleasonGradSekundaer;
            }
            set
            {
                _gleasonGradSekundaer = value;
            }
        }


        [XmlIgnore]
        public bool GleasonGradSekundaerEnumValueSpecified => _gleasonGradSekundaer.HasValue;

        /// <summary>
        /// Wert des Gleason-Score (Malignitätskriterium, therapieentscheidend) (Werte 2-10, 7a,7b)
        /// </summary>
        [XmlIgnore]
        public string GleasonScoreErgebnis
        {
            get { return _gleasonScoreErgebnis?.ToXmlEnumAttributeName(); }
            set
            {
                if (!value.IsNothing())
                    _gleasonScoreErgebnis = value.TryParseAsEnumOrThrow<ProstataGleasonScoreErgebnis>(_typeName, nameof(GleasonScoreErgebnis));
            }
        }

        [XmlElement("GleasonScoreErgebnis", Order = 3)]
        public ProstataGleasonScoreErgebnis? GleasonScoreErgebnisEnumValue
        {
            get
            {
                return _gleasonScoreErgebnis;
            }
            set
            {
                _gleasonScoreErgebnis = value;
            }
        }


        [XmlIgnore]
        public bool GleasonScoreErgebnisEnumValueSpecified => _gleasonScoreErgebnis.HasValue;
       
    }
}
