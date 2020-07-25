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

namespace AdtGekid
{
    using Validation;
    /// <summary>
    /// Basistyp für alle Meldungen.
    /// Bildet den Root-Knoten des XML.
    /// </summary>
    [Serializable()]
    [XmlType("ADT_GEKID", AnonymousType = true, Namespace = GekidNamespace)]
    [XmlRoot(Namespace = GekidNamespace, IsNullable = false, ElementName = "ADT_GEKID")]
    public class Root
    {
        public const string GekidNamespace = "http://www.gekid.de/namespace";

        private SchemaVersion _version;

        [XmlIgnore]
        public string SchemaVersion
        {
            get { return _version.ToXmlEnumAttributeName();  }
            //set
            //{
            //    _version = value.ValidateNeitherNullNorEmpty(typeof(Root).Name,nameof(this.SchemaVersion));
            //}
            set
            {
                _version = value.TryParseAsEnumOrThrow<SchemaVersion>(typeof(Root).Name,nameof(this.SchemaVersion));
            }
        }

        [XmlAttribute("Schema_Version")]
        public SchemaVersion SchemaVersionEnumValue
        {
            get { return _version; }
            set { _version = value; }
        }



        [XmlElement("Absender", Order = 1)]
        public Absender Absender { get; set; }
       
        [XmlArrayItem("Patient", IsNullable = false)]
        [XmlArray("Menge_Patient", Order = 2)]
        public Patient[] Patienten { get; set; }

        [XmlArrayItem("Melder", IsNullable = false)]
        [XmlArray("Menge_Melder", Order = 3)]
        public MelderTyp[] Melder { get; set; }


    }
}
