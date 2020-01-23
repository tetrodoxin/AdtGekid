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
using System.Linq;
using System.Xml.Serialization;
using AdtGekid.Validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdtGekid
{
    /// <summary>
    /// Enthält Angaben zur Person des Patienten.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientPatienten_Stammdaten", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Stammdaten 
    {
        private static char[] AllowedSexCodes = "MWSU".ToCharArray();

        private Geschlecht geschlecht;        
        private string _familienangehoerigenNr;
        private string _krankenkassenNr;
        private string _krankenversichertenNr;
        private string _id;
        private string _geburtsname;
        private string _nachname;
        private string _namenszusatz;
        private string _titel;
        private string _vornamen;
        private Collection<string> _fruehereNamen;

        private string _typeName = typeof(Stammdaten).Name;

        /// <summary>
        /// Gibt an, ob der Wert der <see cref="KrankenkassenNr"/> validiert werden soll oder nicht.
        /// Default: true
        /// </summary>
        public static bool KrankenkassenNrValidationEnabled = true;

        /// <summary>
        /// Gibt an, ob der Wert der <see cref="KrankenversichertenNr"/> validiert werden soll oder nicht.
        /// Default: true
        /// </summary>
        public static bool KrankenversichertenNrValidationEnabled = true;

       


        /// <summary>
        /// <para>
        /// Der Bezug eines Angehörigen zum Mitglied wird über die 
        /// Familienangehörigennummer hergestellt.
        /// </para>
        /// Ebenfalls optionaler Teil der eindeutigen KV-Nr eines deutschen Bürgers.
        /// </summary>
        [XmlElement("FamilienangehoerigenNr", Order = 2)]
        public string FamilienangehoerigenNr
        {
            get { return _familienangehoerigenNr; }
            set { _familienangehoerigenNr = value.ValidateOrThrow(
                    new StringValidatorByRegex(
                        StringValidatorBehavior.TrimAllowEmpty, @"[a-zA-Z][0-9]{1,9}")
                        , _typeName
                        , nameof(this.FamilienangehoerigenNr)
                );
            }
                
        }

        /// <summary>
        /// Eindeutige Bezeichnung der jeweiligen Krankenkasse oder Versicherung.
        /// Dies ist der 9-stellige, veränderliche Teil der eindeutigen KV-Nr eines deutschen Bürgers.
        /// </summary>
        [XmlElement("KrankenkassenNr", Order = 3)]
        public string KrankenkassenNr
        {
            get { return _krankenkassenNr; }
            set
            {
                _krankenkassenNr = (
                    KrankenkassenNrValidationEnabled 
                        ? value.ValidateOrThrow(IkNrValidator.Instance, _typeName, nameof(this.KrankenkassenNr))
                        : value
                    )
                ;
            }
        }

        /// <summary>
        /// Eindeutige Versicherten-Nummer des Patienten (10 stellig).
        /// </summary>
        [XmlElement("KrankenversichertenNr", Order = 1)]
        public string KrankenversichertenNr
        {
            get { return _krankenversichertenNr; }
            set
            {
                _krankenversichertenNr = (
                    KrankenversichertenNrValidationEnabled 
                    ? value.ValidateOrThrow(KvNrValidator.Instance, _typeName, nameof(this.KrankenversichertenNr))
                    : value
                   )
                ;
            }
        }

        [XmlArrayItem("Patienten_Frueherer_Name", IsNullable = false)]
        [XmlArray("Menge_Frueherer_Name", Order = 9)]
        public Collection<string> FruehereNamen
        {
            get { return _fruehereNamen; }
            set { _fruehereNamen = value.EnsureValidatedStringList().WithValidator(StringValidatorByLength.Max100); }
        }


        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool FruehereNamenSpecified => 
            FruehereNamen == null || FruehereNamen.Count == 0 ? false : true;
      


        /// <summary>
        /// Eindeutig pro Patient pro Einrichtung
        /// </summary>
        [XmlAttribute("Patient_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Der Geburtsname des Patienten zum Zeitpunkt seiner Geburt
        /// </summary>
        [XmlElement("Patienten_Geburtsname", Order = 8)]
        public string Geburtsname
        {
            get { return _geburtsname; }
            set { _geburtsname = value.ValidateMaxLength(50, _typeName, nameof(this.Geburtsname)); }
        }

        /// <summary>
        /// Differenzierung einer Person nach ihrem Geschlechtsmerkmal
        /// </summary>
        [XmlIgnore]
        public string Geschlecht
        {
            get
            {
                return geschlecht.ToString();
            }          
            set { geschlecht = value.TryParseAsEnumOrThrow<Geschlecht>(_typeName, nameof(this.Geschlecht), false); }
        }

        [XmlElement("Patienten_Geschlecht", Order = 10)]
        public Geschlecht GeschlechtEnumValue
        {
            get
            {
                return geschlecht;
            }
            set
            {
                this.geschlecht = value;
            }
        }


        /// <summary>
        /// Der aktuelle Nachname des Patienten zum Zeitpunkt der Meldung
        /// </summary>
        [XmlElement("Patienten_Nachname", Order = 4)]
        public string Nachname
        {
            get { return _nachname; }
            set { _nachname = value.ValidateMaxLength(50, _typeName, nameof(this.Nachname)); }
        }

        /// <summary>
        /// Namenszusätze des Patienten
        /// </summary>
        [XmlElement("Patienten_Namenszusatz", Order = 6)]
        public string Namenszusatz
        {
            get { return _namenszusatz; }
            set { _namenszusatz = value.ValidateMaxLength(30, _typeName, nameof(this.Namenszusatz)); }
        }

        /// <summary>
        /// Der akademische Titel des Patienten.
        /// Keine Berufs- oder Amtsbezeichungen wie Professor, Direktor, Inspektor oder PD
        /// </summary>
        [XmlElement("Patienten_Titel", Order = 5)]
        public string Titel
        {
            get { return _titel; }
            set { _titel = value.ValidateMaxLength(20, _typeName, nameof(this.Titel)); }
        }

        /// <summary>
        /// Alle bekannten Vornamen des Patienten
        /// </summary>
        [XmlElement("Patienten_Vornamen", Order = 7)]
        public string Vornamen
        {
            get { return _vornamen; }
            set { _vornamen = value.ValidateMaxLength(50, _typeName, nameof(this.Vornamen)); }
        }

        /// <summary>
        /// <para>
        /// Tag, Monat und Jahr der Geburt einer Person
        /// </para>
        /// <para>
        /// (nach dem Gregorianischen Kalender)
        /// </para>
        /// </summary>
        [XmlElement("Patienten_Geburtsdatum", Order = 11)]
        public DatumTyp Geburtsdatum { get; set; }

        /// <summary>
        /// Ein Array von Anschriften des Patienten.
        /// </summary>
        [XmlArrayItem("Adresse", IsNullable = false, Type = typeof(Adresse))]
        [XmlArray("Menge_Adresse", Order = 12)]
        public Adresse[] Menge_Adresse { get; set; }
    }
}