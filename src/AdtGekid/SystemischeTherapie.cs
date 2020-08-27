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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdtGekid
{
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSYST", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class SystemischeTherapie
    {
        private SystemTherapieEndeGrund? _endeGrund;
        private SystemTherapieIntention _intention;
        private SystemTherapieStellungOp? _stellungOp;
        private string _anmerkung;
        private string _id;
        private string _protokoll;
        private string _therapieartAnmerkung;
        private Collection<string> _substanzen;
        private Collection<SystemTherapieart> _therapieArten;

        private string _typeName = typeof(SystemischeTherapie).Name;
        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 12)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.Anmerkung)); }
        }

        [XmlArrayItem("SYST_Nebenwirkung", IsNullable = false)]
        [XmlArray("Menge_Nebenwirkung", Order = 11)]
        public NebenwirkungTyp[] Nebenwirkungen { get; set; }

        [XmlArrayItem("SYST_Substanz", IsNullable = false)]
        [XmlArray("Menge_Substanz", Order = 7)]
        public Collection<string> Substanzen
        {
            get { return _substanzen; }
            set { _substanzen = value.EnsureValidatedStringList().WithValidator(StringValidatorByLength.Max255); }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool SubstanzenSpecified =>
            Substanzen != null && Substanzen.Count > 0;

        [XmlIgnore]
        public Collection<string> TherapieArten
        {           
            get { return _therapieArten.AsStringCollection<SystemTherapieart>(); }
            //set { _komplikationen = value.EnsureValidatedStringList().WithValidator(OpKomplikationValidator.CreateInstance(_typeName, nameof(this.Komplikationen))); }
            set { _therapieArten = value.TryParseAsEnumCollectionOrThrow<SystemTherapieart>(); }
        }

      
        [XmlArrayItem("SYST_Therapieart", IsNullable = false)]
        [XmlArray("Menge_Therapieart", Order = 3)]
        public Collection<SystemTherapieart> TherapieArtenEnumCollection
        {
            get { return _therapieArten; }
            set { _therapieArten = value; }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool TherapieArtenEnumCollectionSpecified =>
            _therapieArten != null && _therapieArten.Count > 0;

        [XmlElement("Residualstatus", Order = 10)]
        public ResidualstatusTyp Residualstatus { get; set; }

        /// <summary>
        /// Gibt an, wann die systemische Therapie begonnen wurde.
        /// </summary>
        [XmlElement("SYST_Beginn_Datum", Order = 6)]
        public DatumTyp BeginnDatum { get; set; }

        /// <summary>
        /// Gibt an, wann die systemische Therapie beendet wurde.
        /// </summary>
        [XmlElement("SYST_Ende_Datum", Order = 9)]
        public DatumTyp EndeDatum { get; set; }

        /// <summary>
        /// Gibt den Grund an, warum die Systemtherapie beendet wurde.
        /// </summary>
        [XmlIgnore]
        public string EndeGrund
        {
            get { return _endeGrund.ToString(); }
            //set { _endeGrund = value.ValidateOrThrow(TherapieEndeGrundValidator.SystemischeTherapie, _typeName, nameof(this.EndeGrund)); }
            set {
                if (!value.IsNothing())
                    _endeGrund = value.TryParseAsEnumOrThrow<SystemTherapieEndeGrund>(_typeName, nameof(this.EndeGrund)); 
            }
        }

        [XmlElement("SYST_Ende_Grund", Order = 8)]
        public SystemTherapieEndeGrund? EndeGrundEnumValue
        {
            get { return _endeGrund; }
            set { _endeGrund = value; }
        }

        public bool EndeGrundEnumValueSpecified => EndeGrundEnumValue.HasValue;

        /// <summary>
        /// eindeutig identifizierendes Merkmal der systemischen Therapie
        /// </summary>
        /// <value>
        /// Alphanumerischer Wert bis 16 Zeichen.
        /// </value>
        [XmlAttribute("SYST_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16); }
        }

        /// <summary>
        /// Gibt an, mit welcher Intention die systemische Therapie durchgeführt wird.
        /// </summary>
        [XmlIgnore]
        public string Intention
        {
            get { return _intention.ToString(); }
            //set { _intention = value.ValidateOrThrow(TherapieIntentionValidator.NichtOP, _typeName, nameof(this.Intention)); }
            set { _intention = value.TryParseAsEnumOrThrow<SystemTherapieIntention>(_typeName, nameof(this.Intention)); }
        }

        [XmlElement("SYST_Intention", Order = 1)]
        public SystemTherapieIntention IntentionEnumValue
        {
            get { return _intention; }
            set { _intention = value; }
        }


        /// <summary>
        /// Gibt an, nach welchem Protokoll die Systemtherapie durchgeführt wird.
        /// </summary>
        [XmlElement("SYST_Protokoll", Order = 5)]
        public string Protokoll
        {
            get { return _protokoll; }
            set { _protokoll = value.ValidateMaxLength(255, _typeName, nameof(this.Protokoll)); }
        }

        /// <summary>
        /// Gibt an, in welchem Bezug zu einer operativen Therapie die systemische The-
        /// rapie steht.
        /// </summary>
        [XmlIgnore]
        public string StellungOp
        {
            get { return _stellungOp.ToString(); }
            //set { _stellungOp = value.ValidateOrThrow(StellungOpValidator.Instance, _typeName, nameof(this.StellungOp)); }
            set {
                if (!value.IsNothing())
                    _stellungOp = value.TryParseAsEnumOrThrow<SystemTherapieStellungOp>(_typeName, nameof(this.StellungOp)); 
            }
        }

        [XmlElement("SYST_Stellung_OP", Order = 2)]
        public SystemTherapieStellungOp? StellungOpEnumValue
        {
            get { return _stellungOp; }            
            set { _stellungOp = value; }
        }

        public bool StellungOpEnumValueSpecified => StellungOpEnumValue.HasValue;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("SYST_Therapieart_Anmerkung", Order = 4)]
        public string TherapieartAnmerkung
        {
            get { return _therapieartAnmerkung; }
            set { _therapieartAnmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.TherapieartAnmerkung)); }
        }
    }
}
