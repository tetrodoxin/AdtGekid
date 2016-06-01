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
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungTumorzuordnung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Tumorzuordnung
    {
        private string _seitenlokalisation;
        private string _id;
        private IcdTyp _icdCode;

        /// <summary>
        /// Zeitpunkt, angegeben in Tag, Monat und Jahr, an dem die meldepflichtige Di-
        /// agnose erstmals durch einen Arzt klinisch oder mikroskopisch diagnostiziert
        /// wurde
        /// </summary>
        [XmlElement("Diagnosedatum", Order = 2)]
        public DatumTyp Diagnosedatum { get; set; }

        /// <summary>
        /// Kodierung einer meldepflichtigen Tumorerkrankung nach der aktuellen ICD-10-
        /// GM
        /// </summary>
        [XmlElement("Primaertumor_ICD_Code", Order = 1)]
        public IcdTyp IcdCode
        {
            get { return _icdCode; }
            set { _icdCode = value; }
        }

        /// <summary>
        /// Angabe der betroffenen organspezifischen Seite
        /// </summary>
        [XmlElement("Seitenlokalisation", Order = 3)]
        public string Seitenlokalisation
        {
            get { return _seitenlokalisation; }
            set { _seitenlokalisation = value.ValidateOrThrow(SeitenlokalisationValidator.Instance); }
        }

        /// <summary>
        /// Eindeutig identifizierendes Merkmal des Tumors
        /// </summary>
        /// <value>
        /// Text bis 16 Zeichen.
        /// </value>
        [XmlAttribute("Tumor_ID")]
        public string Id
        {
            get { return _id; }
            set { _id = value.ValidateMaxLength(16); }
        }
    }
}
