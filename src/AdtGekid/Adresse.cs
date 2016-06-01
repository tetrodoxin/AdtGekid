#region license

//MIT License

//Copyright(c) 2016 Andreas Huebner

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion 
using System;
using System.Xml.Serialization;
using AdtGekid.Validation;

namespace AdtGekid
{
    /// <summary>
    /// Datentyp für eine Anschrift eines Patienten.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientPatienten_StammdatenAdresse", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Adresse
    {
        private string _hausnummer;
        private string _land;
        private string _ort;
        private string _pLZ;
        private string _strasse;

        /// <summary>
        /// Gültigkeit der Adresse: bis Datum
        /// </summary>
        [XmlElement("Gueltig_bis", Order = 7)]
        public DatumTyp GueltigBis { get; set; }

        /// <summary>
        /// Gültigkeit der Adresse: seit Datum
        /// </summary>
        [XmlElement("Gueltig_von", Order = 6)]
        public DatumTyp GueltigVon { get; set; }

        /// <summary>
        /// Die Nummer des Hauses, in dem der Patient wohnt
        /// </summary>
        [XmlElement("Patienten_Hausnummer", Order = 2)]
        public string Hausnummer
        {
            get { return _hausnummer; }
            set { _hausnummer = value.ValidateOrThrow(@"^[a-zA-z0-9\.\-]*$"); }
        }

        /// <summary>
        /// Das aktuelle Land (Wohnort) des Patienten zum Zeitpunkt der Meldung
        /// als Code der KFZ-Nationalitätskennzeichen.
        /// </summary>
        [XmlElement("Patienten_Land", Order = 3)]
        public string Land
        {
            get { return _land; }
            set { _land = value.ValidateAlphaCharsOnlyOrThrow(4); }
        }

        /// <summary>
        /// Der Wohnort des Patienten zum Zeitpunkt der Meldung oder Diagnosestellung
        /// </summary>
        [XmlElement("Patienten_Ort", Order = 5)]
        public string Ort
        {
            get { return _ort; }
            set { _ort = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// Aktuelle Postleitzahl von der Anschrift des Patienten zum Zeitpunkt der Meldung
        /// </summary>
        [XmlElement("Patienten_PLZ", Order = 4)]
        public string PLZ
        {
            get { return _pLZ; }
            set { _pLZ = value.ValidateMaxLength(10); }
        }

        /// <summary>
        /// Die aktuelle Anschrift des Patienten zum Zeitpunkt der Meldung
        /// </summary>
        [XmlElement("Patienten_Strasse", Order = 1)]
        public string Strasse
        {
            get { return _strasse; }
            set { _strasse = value.ValidateMaxLength(60); }
        }
    }
}