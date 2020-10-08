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
    /// Klasse für einen Bestrahlungsvorgang als Teil einer Strahlentherapie.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKIDPatientMeldungSTBestrahlung", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Bestrahlung
    {
        private static char[] AllowedTargetSideCodes = "LRBMU".ToCharArray();
        private const string ApplicationTypePattern = @"^P(RCJ|RCN)?|[KI](HDR|PDR|LDR)?|M(SIRT|PRRT)?|S$";
        private const string TargetAreaPattern = @"^(1\.[1-3]\.)|(2\.[1-9]\.[\+\-]?)|(3\.[1-7]\.[\+\-]?)|(4\.[1-9]\.[\+\-]?)|(5\.([2-9]|1[0-2]?)\.[\+\-]?)|(6\.([2-9]|1[0-6]?)\.[\+\-]?)?|([78]\.[12]\.)$";


        private BestrahlungSeiteZielgebiet? _seiteZielgebiet;
        private BestrahlungApplikationsart _applikationsart;
        private BestrahlungZielgebiet _zielgebiet;
        private StrahlendosisTyp _einzeldosis;
        private StrahlendosisTyp _gesamtdosis;

        private string _entity = typeof(Bestrahlung).Name;

        /// <summary>
        /// Gibt mittels eines Codes aus der Zielgebiet-Tabelle an, 
        /// an welcher Stelle die Bestrahlung durchgeführt wurde.
        /// </summary>
        [XmlIgnore]
        public string Zielgebiet
        {
            get { return _zielgebiet.ToXmlEnumAttributeName(); }            
            set
            {
                _zielgebiet = value.TryParseEnumByXmlEnumAttributeOrThrow<BestrahlungZielgebiet>(nameof(Bestrahlung), nameof(Zielgebiet),true, false); ;
            }
        }

        [XmlElement("ST_Zielgebiet", Order = 1)]
        public BestrahlungZielgebiet ZielgebietEnumValue
        {
            get { return _zielgebiet; }
            set { _zielgebiet = value; }
        }

        [XmlIgnore]
        public string SeiteZielgebiet
        {
            get { return _seiteZielgebiet.ToString(); }
            set { _seiteZielgebiet = value.TryParseAsEnumOrThrow<BestrahlungSeiteZielgebiet>(); }
        }

        /// <summary>
        /// Gibt an, an welcher Stelle (Seitenlokalisation) die Bestrahlung durchgeführt wurde.
        /// </summary>
        [XmlElement("ST_Seite_Zielgebiet", Order = 2)]
        public BestrahlungSeiteZielgebiet? SeiteZielgebietEnumValue
        {
            get { return _seiteZielgebiet; }
            set { _seiteZielgebiet = value; }
        }

        public bool SeiteZielgebietEnumValueSpecified => _seiteZielgebiet.HasValue;

        /// <summary>
        /// Gibt an, wann die Bestrahlung begonnen wurde.
        /// </summary>
        [XmlElement("ST_Beginn_Datum", Order = 3)]
        public DatumTyp BeginnDatum { get; set; }

        /// <summary>
        /// Gibt an, wann die Bestrahlung beendet wurde.
        /// </summary>
        [XmlElement("ST_Ende_Datum", Order = 4)]
        public DatumTyp EndeDatum { get; set; }

        /// <summary>
        /// Gibt an, mit welcher Technik die Strahlentherapie durchgeführt wird.
        /// </summary>
        [XmlIgnore]
        public string Applikationsart
        {
            get { return _applikationsart.ToString(); }
            set { _applikationsart = value.TryParseAsEnumOrThrow<BestrahlungApplikationsart>(_entity, nameof(this.ApplikationsartEnumValue)); }
        }


        /// <summary>
        /// Plichtfeld (vergütungsrelevant) beim Landeskrebsregister BaWü 
        /// </summary>
        [XmlElement("ST_Applikationsart", Order = 5)]
        public BestrahlungApplikationsart ApplikationsartEnumValue
        {
            get { return _applikationsart; }
            set { _applikationsart = value; }            
        }
        

        /// <summary>
        /// Gibt an, mit welcher Gesamtdosis das Zielgebiet bestrahlt wurde
        /// </summary>
        [XmlElement("ST_Gesamtdosis", Order = 6)]
        public StrahlendosisTyp Gesamtdosis
        {
            get { return _gesamtdosis; }
            set { _gesamtdosis = value; }
        }

        /// <summary>
        /// Gibt an, mit welcher Einzeldosis das Zielgebiet bestrahlt wurde. Die Dosis
        /// bezieht sich auf die Verschreibungsisodose.
        /// </summary>
        [XmlElement("ST_Einzeldosis", Order = 7)]
        public StrahlendosisTyp Einzeldosis
        {
            get { return _einzeldosis; }
            set { _einzeldosis = value; }
        }
                                           
    }
}
