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
    /// Datentyp für histologische Angaben nach ICD-O
    /// </summary>
    [Serializable()]
    [XmlType("Histologie_Typ", Namespace = Root.GekidNamespace)]
    public class HistologieTyp
    {
        /// <summary>
        /// Gibt an, ob der Wert der <see cref="EinsendeNr"/> streng validiert 
        /// werden soll (Alphanumerisch mit Längenprüfung) oder nicht (nur Längenprüfung)
        /// Default: true
        /// </summary>
        public static bool EinsendeNrStrongValidationEnabled = true;

        private static char[] AllowedGradingCodes = "01234XLMHBUT".ToCharArray();
        private HistoGrading? _grading;

        private int? _lkBefallen;
        private int? _lkUntersucht;
        private int? _sentinelLkBefallen;
        private int? _sentinelLkUntersucht;
        private string _einsendeNr;
        private string _id;
        private string _code;
        private string _freitext;
        private MorphologieIcdOVersion? _icdOVersion;

        private string _typeName = "Histologie";

        /// <summary>
        /// Gibt den Differenzierungsgrad des Tumors an
        /// </summary>
        [XmlIgnore]
        public string Grading
        {
            get { return _grading?.ToXmlEnumAttributeName(); }
            //set { _grading = value.ValidateOrThrow(AllowedGradingCodes, _typeName, nameof(this.Grading)); }
            set { _grading = value.TryParseAsEnumOrThrow<HistoGrading>(_typeName, nameof(this.Grading)); }
        }

        [XmlElement("Grading", Order = 6)]
        public HistoGrading? GradingEnumValue
        {
            get { return _grading; }
            set { _grading = value; }
        }

        public bool GradingEnumValueSpecified => GradingEnumValue.HasValue;

        /// <summary>
        /// Die Histologie-Einsendenummer wird vom pathologischen Institut beim
        /// Eingang des Präparates vergeben. Eine eindeutige Zuordnung, welches Präparat
        /// untersucht wurde, für Referenzzwecke ist so möglich.
        /// </summary>
        [XmlElement("Histologie_EinsendeNr", Order = 2)]
        public string EinsendeNr
        {
            get { return _einsendeNr; }
            set {
                _einsendeNr = EinsendeNrStrongValidationEnabled 
                    ? value.ValidateAlphanumericalOrThrow(16, _typeName, nameof(this.EinsendeNr))
                    : value.ValidateMaxLength(16,_typeName, nameof(this.EinsendeNr));
            }
        }

        [XmlAttribute("Histologie_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16, _typeName, nameof(this.Id)); }
        }

        /// <summary>
        /// Gibt in Stringform an, wie viele Lymphknoten befallen sind (einschließlich Sentinel).
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("LK_befallen", DataType = "nonNegativeInteger", Order = 8)]
        public string LkBefallenString
        {
            get { return _lkBefallen?.ToString(); }
            set { _lkBefallen = value.ParseNullableInt(); }
        }

        /// <summary>
        /// Gibt an, wie viele Lymphknoten befallen sind (einschließlich Sentinel).
        /// </summary>
        [XmlIgnore]
        public int? LkBefallen
        {
            get { return _lkBefallen; }
            set { _lkBefallen = value; }
        }

        /// <summary>
        /// Gibt in Stringform an, wie viele Lymphknoten untersucht wurden (einschließlich Sentinel).
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("LK_untersucht", DataType = "nonNegativeInteger", Order = 7)]
        public string LkUntersuchtString
        {
            get { return _lkUntersucht?.ToString(); }
            set { _lkUntersucht = value.ParseNullableInt(); }
        }


        /// <summary>
        /// Gibt an, wie viele Lymphknoten untersucht wurden (einschließlich Sentinel)
        /// </summary>
        [XmlIgnore]
        public int? LkUntersucht
        {
            get { return _lkUntersucht; }
            set { _lkUntersucht = value; }
        }

        /// <summary>
        /// Gibt an, welche Histologie der Tumor aufweist
        /// </summary>
        [XmlElement("Morphologie_Code", Order = 3)]
        public string Code
        {
            get { return _code; }
            set { _code = value.ValidateOrThrow(@"^\d\d\d\d/\d$", _typeName, nameof(this.Code)); }
            
        }

        /// <summary>
        /// Gibt die Originalbezeichnung der morphologischen Diagnose an
        /// </summary>
        [XmlElement("Morphologie_Freitext", Order = 5)]
        public string Freitext
        {
            get { return _freitext; }
            set { _freitext = value.ValidateMaxLength(500, _typeName, nameof(this.Freitext)); }
        }

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-O Version
        /// </summary>
        [XmlIgnore]
        public string IcdOVersion
        {
            get { return ((int?)_icdOVersion).ToString(); }
            //set { _icdOVersion = value.ValidateMaxLength(25, _typeName, nameof(this.IcdOVersion)); }
            set { _icdOVersion = value.TryParseAsEnumOrThrow<MorphologieIcdOVersion>(_typeName, nameof(this.IcdOVersion)); }
        }

        [XmlElement("Morphologie_ICD_O_Version", Order = 4)]
        public MorphologieIcdOVersion? IcdoVersionEnumValue
        {
            get { return _icdOVersion; }
            set { _icdOVersion = value; }
        }

        /// <summary>
        /// Zur Möglichkeit der Steuerung der Serialisierung:
        /// Bei <c>false</c> wird das entsprechende Element im XML nicht geschrieben
        /// </summary>
        [XmlIgnore]
        public bool IcdoVersionEnumValueSpecified => IcdoVersionEnumValue.HasValue;

        /// <summary>
        /// Gibt in Stringform an, wie viele Sentinel-Lymphknoten untersucht wurden.
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("Sentinel_LK_befallen", DataType = "nonNegativeInteger", Order = 10)]
        public string SentinelLkBefallenString
        {
            get { return _sentinelLkBefallen?.ToString(); }
            set { _sentinelLkBefallen = value.ParseNullableInt(); }
        }



        /// <summary>
        /// Gibt an, wie viele Sentinel-Lymphknoten untersucht wurden.
        /// </summary>
        [XmlIgnore]
        public int? SentinelLkBefallen
        {
            get { return _sentinelLkBefallen; }
            set { _sentinelLkBefallen = value; }
        }

        /// <summary>
        /// Gibt in Stringform an, wie viele Sentinel-Lymphknoten untersucht wurden.
        /// Diese Eigenschaft dient nur internen Zwecken und sollte nicht im Code verwendet werden.
        /// </summary>
        [XmlElement("Sentinel_LK_untersucht", DataType = "nonNegativeInteger", Order = 9)]
        public string SentinelLkUntersuchtString
        {
            get { return _sentinelLkUntersucht?.ToString(); }
            set { _sentinelLkUntersucht = value.ParseNullableInt(); }
        }

        /// <summary>
        /// Gibt an, wie viele Sentinel-Lymphknoten untersucht wurden.
        /// </summary>
        [XmlIgnore]
        public int? SentinelLkUntersucht
        {
            get { return _sentinelLkUntersucht; }
            set { _sentinelLkUntersucht = value; }
        }

        /// <summary>
        /// Zeitpunkt, angegeben in Tag, Monat und Jahr, an dem die meldepflichtige
        /// Diagnose mikroskopisch diagnostiziert wurde.
        /// </summary>
        [XmlElement("Tumor_Histologiedatum", Order = 1)]
        public DatumTyp Datum { get; set; }
    }
}
