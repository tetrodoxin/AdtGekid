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
        private static char[] AllowedGradingCodes = "01234XLMHBUT".ToCharArray();
        private string _grading;

        private int? _lkBefallen;
        private int? _lkUntersucht;
        private int? _sentinelLkBefallen;
        private int? _sentinelLkUntersucht;
        private string _einsendeNr;
        private string _id;
        private string _code;
        private string _freitext;
        private string _icdOVersion;

        /// <summary>
        /// Gibt den Differenzierungsgrad des Tumors an
        /// </summary>
        [XmlElement("Grading", Order = 6)]
        public string Grading
        {
            get { return _grading; }
            set { _grading = value.ValidateOrThrow(AllowedGradingCodes); }
        }

        /// <summary>
        /// Die Histologie-Einsendenummer wird vom pathologischen Institut beim
        /// Eingang des Präparates vergeben. Eine eindeutige Zuordnung, welches Präparat
        /// untersucht wurde, für Referenzzwecke ist so möglich.
        /// </summary>
        [XmlElement("Histologie_EinsendeNr", Order = 2)]
        public string EinsendeNr
        {
            get { return _einsendeNr; }
            set { _einsendeNr = value.ValidateAlphanumericalOrThrow(16); }
        }

        [XmlAttribute("Histologie_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16); }
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
            set { _code = value.ValidateMaxLength(6); }
        }

        /// <summary>
        /// Gibt die Originalbezeichnung der morphologischen Diagnose an
        /// </summary>
        [XmlElement("Morphologie_Freitext", Order = 5)]
        public string Freitext
        {
            get { return _freitext; }
            set { _freitext = value.ValidateMaxLength(500); }
        }

        /// <summary>
        /// Bezeichnung der zur Kodierung verwendeten ICD-O Version
        /// </summary>
        [XmlElement("Morphologie_ICD_O_Version", Order = 4)]
        public string IcdOVersion
        {
            get { return _icdOVersion; }
            set { _icdOVersion = value.ValidateMaxLength(25); }
        }

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