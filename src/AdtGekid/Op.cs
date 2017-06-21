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
        private string _intention;
        private string _anmerkung;
        private string _id;
        private string _opsVersion;
        private Collection<string> _operateure;
        private Collection<string> _komplikationen;
        private Collection<string> _opsCodes;

        /// <summary>
        /// Sachverhalte, die sich in der Kodierung des Erfassungsdokumentes unpräzise
        /// abbilden oder darüber hinausgehen, können hier genau erfasst werden.
        /// </summary>
        [XmlElement("Anmerkung", Order = 10)]
        public string Anmerkung
        {
            get { return _anmerkung; }
            set { _anmerkung = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Histologische Daten nach der Operation, sofern vorhanden.
        /// </summary>
        [XmlElement("Histologie", Order = 5)]
        public HistologieTyp Histologie { get; set; }

        /// <summary>
        /// Liste von OP-Komplikationen.
        /// </summary>
        [XmlArrayItem("OP_Komplikation", IsNullable = false)]
        [XmlArray("Menge_Komplikation", Order = 8)]
        public Collection<string> Komplikationen
        {
            get { return _komplikationen; }
            set { _komplikationen = value.EnsureValidatedStringList().WithValidator(OpKomplikationValidator.Instance); }
        }

        /// <summary>
        /// Das entsprechende Container-Xml-Element wird dann nicht serialisiert
        /// wenn es leer ist oder keine Elemente enthält
        /// </summary>
        [XmlIgnore]
        public bool KomplikationenSpecified =>
            Komplikationen == null || Komplikationen.Count == 0 ? false : true;

        /// <summary>
        /// Liste der Operateure.
        /// </summary>
        [XmlArrayItem("Name_Operateur", IsNullable = false)]
        [XmlArray("Menge_Operateur", Order = 9)]
        public Collection<string> Operateure
        {
            get { return _operateure; }
            set { _operateure = value.EnsureValidatedStringList().WithValidator(StringValidatorByLength.Max100); }
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
            set { _opsCodes = value.EnsureValidatedStringList().WithValidator(OpsCodeValidator.Instance); }
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
        [XmlArrayItem("TNM", IsNullable = false)]
        [XmlArray("Menge_TNM", Order = 6)]
        public TnmTyp[] TnmKlassifizierungen { get; set; }

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
            set { _id = value.ValidateAlphanumericalOrThrow(16); }
        }

        /// <summary>
        /// Gibt an, mit welchem Ziel die OP durchgeführt wird
        /// </summary>
        [XmlElement("OP_Intention", Order = 1)]
        public string Intention
        {
            get { return _intention; }
            set { _intention = value.ValidateOrThrow(AllowedIntentionCodes); }
        }

        /// <summary>
        /// Gibt an, nach welcher Version (Jahr) der OPS klassifiziert wurde.
        /// </summary>
        [XmlElement("OP_OPS_Version", Order = 4)]
        public string OpsVersion
        {
            get { return _opsVersion; }
            set { _opsVersion = value.ValidateMaxLength(16); }
        }

        /// <summary>
        /// Daten über die lokale und gesamte Beurteilung des postoperativen Residualstatus
        /// </summary>
        [XmlElement("Residualstatus", Order = 7)]
        public ResidualstatusTyp Residualstatus { get; set; }
    }
}
