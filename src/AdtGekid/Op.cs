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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using AdtGekid.Validation;
using System.Collections.ObjectModel;

namespace AdtGekid
{
    /// <summary>
    /// Daten zu einer operativen Therapie.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungOP", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Op
    {
        private static char[] AllowedIntentionCodes = "KPDRSX".ToCharArray();
        private OpIntention _intention;
        private string _anmerkung;
        private string _id;
        private OpsVersion _opsVersion;
        private Collection<string> _operateure;
        private Collection<OpKomplikation> _komplikationen;
        private Collection<string> _opsCodes;

        private string _typeName = "Operation";

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 10)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500, _typeName, nameof(this.Anmerkung)); }
        }

        /// <summary>
        /// Histologische Daten nach der Operation, sofern vorhanden.
        /// </summary>
        [XmlElement("Histologie", Order = 5)]
        public HistologieTyp Histologie { get; set; }

        [XmlIgnore]
        public Collection<string> Komplikationen
        {
            get { return _komplikationen.AsStringEnumerable<OpKomplikation>() as Collection<string>; }
            //set { _komplikationen = value.EnsureValidatedStringList().WithValidator(OpKomplikationValidator.CreateInstance(_typeName, nameof(this.Komplikationen))); }
            set { _komplikationen = value.TryParseAsEnumCollectionOrThrow<OpKomplikation>() as Collection<OpKomplikation>; }
        }

        /// <summary>
        /// Liste von OP-Komplikationen.
        /// </summary>
        [XmlArrayItem("OP_Komplikation", IsNullable = false)]
        [XmlArray("Menge_Komplikation", Order = 8)]
        public Collection<OpKomplikation> KomplikationenEnumValue
        {
            get { return _komplikationen; }
            set { _komplikationen = value; }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool KomplikationenEnumValueSpecified =>
            Komplikationen != null && Komplikationen.Count > 0;



        /// <summary>
        /// Liste der Operateure.
        /// </summary>
        [XmlArrayItem("Name_Operateur", IsNullable = false)]
        [XmlArray("Menge_Operateur", Order = 9)]
        public Collection<string> Operateure
        {
            get { return _operateure; }
            set { _operateure = value.EnsureValidatedStringList().WithValidator(StringValidatorByLength.CreateInstanceForMax100(_typeName, nameof(this.Operateure))); }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool OperateureSpecified =>
            Operateure == null || Operateure.Count == 0 ? false : true;

        /// <summary>
        /// Liste der OPS-Codes.
        /// </summary>
        [XmlArrayItem("OP_OPS", IsNullable = false)]
        [XmlArray("Menge_OPS", Order = 3)]
        public Collection<string> OpsCodes
        {
            get { return _opsCodes; }
            set { _opsCodes = value.EnsureValidatedStringList().WithValidator(OpsCodeValidator.CreateInstance(_typeName, nameof(this.OpsCodes))); }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool OpsCodesSpecified =>
            OpsCodes == null || OpsCodes.Count == 0 ? false : true;



        /// <summary>
        /// Liste von postoperativen TNM-Klassifizierungen.
        /// </summary>        
        [XmlElement("TNM", Order = 6)]
        public TnmTyp TnmKlassifizierung { get; set; }

        /// <summary>
        /// Datum der OP
        /// </summary>
        [XmlElement("OP_Datum", Order = 2)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// eindeutig identifizierendes Merkmal der Operation
        /// </summary>
        [XmlAttribute("OP_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.Id)); }
        }

        [XmlIgnore]       
        public string Intention
        {
            get { return _intention.ToString(); }
            //set { _intention = value.ValidateOrThrow(AllowedIntentionCodes, _typeName, nameof(this.Intention)); }
            set { _intention = value.TryParseAsEnumOrThrow<OpIntention>(_typeName, nameof(this.Intention)); }
        }

        /// <summary>
        /// Gibt an, mit welchem Ziel die OP durchgeführt wird
        /// </summary>
        [XmlElement("OP_Intention", Order = 1)]
        public OpIntention IntentionEnumValue
        {
            get { return _intention; }
            set { _intention = value; }
        }

        /// <summary>
        /// Gibt an, nach welcher Version (Jahr) der OPS klassifiziert wurde.
        /// </summary>
        [XmlIgnore]
        public string OpsVersion
        {
            get { return ((int)_opsVersion).ToString(); }
            //set { _opsVersion = value.ValidateMaxLength(16, _typeName, nameof(this.OpsVersion)); }
            set { _opsVersion = value.TryParseAsEnumOrThrow<OpsVersion>(_typeName, nameof(this.OpsVersion)); }
        }

        [XmlElement("OP_OPS_Version", Order = 4)]
        public OpsVersion OpsVersionEnumValue
        {
            get { return _opsVersion; }
            set
            {
                _opsVersion = value;
            }
        }

        [XmlIgnore]
        public bool OpsVersionEnumValueSpecified => OpsVersionEnumValue != AdtGekid.OpsVersion.NotSpecified;

        /// <summary>
        /// Daten über die lokale und gesamte Beurteilung des postoperativen Residualstatus
        /// </summary>
        [XmlElement("Residualstatus", Order = 7)]
        public ResidualstatusTyp Residualstatus { get; set; }
    }
}
