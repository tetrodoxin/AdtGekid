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
    [XmlType("Menge_Weitere_Klassifikation_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class WeitereKlassifikation
    {
        private string _name;
        private string _stadium;

        private string _typeName = typeof(WeitereKlassifikation).Name;

        /// <summary>
        /// Datum der Erhebung der hämatologischen oder sonstigen Klassifikation
        /// </summary>
        [XmlElement("Datum", Order = 1)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Name der hämatologischen oder sonstigen Klassifikation
        /// </summary>
        [XmlElement("Name", Order = 2)]
        public string Name
        {
            get { return _name; }
            set { _name = value.ValidateMaxLength(100, _typeName, nameof(this.Name)); }
        }

        /// <summary>
        /// Einstufung gemäß der hämatologischen oder sonstigen Klassifikation
        /// </summary>
        [XmlElement("Stadium", Order = 3)]
        public string Stadium
        {
            get { return _stadium; }
            set { _stadium = value.ValidateMaxLength(15, _typeName, nameof(this.Stadium)); }
        }
    }
}
