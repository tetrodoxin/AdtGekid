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
    /// Enthält Angaben über den Melder.
    /// </summary>
    [Serializable()]
    [XmlType("Melder_Typ", Namespace = Root.GekidNamespace)]
    public class MelderTyp
    {
        private string _id;
        private string _iKNR;
        private string _lANR;
        private string _bSNR;
        private string _meldendeStelle;
        private string _klinikStationPraxis;
        private string _arztname;
        private string _anschrift;
        private string _pLZ;
        private string _ort;
        private string _bankname;
        private string _kontoinhaber;
        private string _bIC;
        private string _iBAN;

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Melders
        /// </summary>
        [XmlAttribute("Melder_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(6); }
        }

        /// <summary>
        /// Institutionskennzeichen der meldenden Einrichtung.
        /// </summary>
        [XmlElement("Melder_IKNR", Order = 1)]
        public string IKNR
        {
            get { return _iKNR; }
            set { _iKNR = value.ValidateAlphanumericalOrThrow(9); }
        }

        /// <summary>
        /// lebenslange Arztnummer des verantwortlichen/meldenden Arztes
        /// </summary>
        [XmlElement("Melder_LANR", Order = 2)]
        public string LANR
        {
            get { return _lANR; }
            set { _lANR = value.ValidateAlphanumericalOrThrow(9); }
        }

        /// <summary>
        /// Betriebsstättennummer der meldenden Einrichtung.
        /// </summary>
        [XmlElement("Melder_BSNR", Order = 3)]
        public string BSNR
        {
            get { return _bSNR; }
            set { _bSNR = value.ValidateAlphanumericalOrThrow(9); }
        }

        /// <summary>
        /// <para>
        /// Angaben zum Leistungserbringer.
        /// </para>
        /// <para>
        /// Einen zwar beliebigen aber eindeutigen Melderschlüssel vergeben durch das
        /// für den Behandlungsort zuständige Krebsregister.
        /// </para>
        /// </summary>
        [XmlElement("Meldende_Stelle", Order = 4)]
        public string MeldendeStelle
        {
            get { return _meldendeStelle; }
            set { _meldendeStelle = value.ValidateAlphanumericalOrThrow(20); }
        }

        /// <summary>
        /// Angaben über das meldende Krankenhaus, Abteilung, 
        /// Station oder (Teil eines) Praxisname(ns)
        /// </summary>
        [XmlElement("Melder_KH_Abt_Station_Praxis", Order = 5)]
        public string KlinikStationPraxis
        {
            get { return _klinikStationPraxis; }
            set { _klinikStationPraxis = value.ValidateMaxLength(70); }
        }

        /// <summary>
        /// Name und Vorname des verantwortlichen/meldenden Arztes
        /// </summary>
        [XmlElement("Melder_Arztname", Order = 6)]
        public string Arztname
        {
            get { return _arztname; }
            set { _arztname = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// Anschrift (Straße, Hausnummer und Postfix) der meldenden Einrichtung
        /// </summary>
        [XmlElement("Melder_Anschrift", Order = 7)]
        public string Anschrift
        {
            get { return _anschrift; }
            set { _anschrift = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// Postleitzahl der (deutschen) meldenden Einrichtung.
        /// </summary>
        [XmlElement("Melder_PLZ", Order = 8)]
        public string PLZ
        {
            get { return _pLZ; }
            set { _pLZ = value.ValidateMaxLength(5).ValidateOrThrow("0123456789".ToCharArray()); }
        }

        /// <summary>
        /// Ort der (deutschen) meldenden Einrichtung
        /// </summary>
        [XmlElement("Melder_Ort", Order = 9)]
        public string Ort
        {
            get { return _ort; }
            set { _ort = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// Name der Bank des Kontoinhabers (Leistungserbringer/Melder)
        /// </summary>
        [XmlElement("Melder_Bankname", Order = 10)]
        public string Bankname
        {
            get { return _bankname; }
            set { _bankname = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// Name des Kontoinhabers (Leistungserbringer/Melder)
        /// </summary>
        [XmlElement("Melder_Kontoinhaber", Order = 11)]
        public string Kontoinhaber
        {
            get { return _kontoinhaber; }
            set { _kontoinhaber = value.ValidateMaxLength(50); }
        }

        /// <summary>
        /// BIC (Bank Identifier Code) des Kontoinhabers (Leistungserbringer/Melder)
        /// </summary>
        [XmlElement("Melder_BIC", Order = 12)]
        public string BIC
        {
            get { return _bIC; }
            set { _bIC = value.ValidateAlphanumericalOrThrow(11); }
        }

        /// <summary>
        /// IBAN (International Bank Account Number) des Kontoinhabers (Leistungser-
        /// bringer/Melder)
        /// </summary>
        [XmlElement("Melder_IBAN", Order = 13)]
        public string IBAN
        {
            get { return _iBAN; }
            set { _iBAN = value.ValidateAlphanumericalOrThrow(22); }
        }
    }
}
