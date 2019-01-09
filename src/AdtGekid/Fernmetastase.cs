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
    /// Enthält Angaben zu Fernmetastasen.
    /// </summary>
    [Serializable()]
    [XmlType("Menge_FM_Typ", AnonymousType = true, Namespace = Root.GekidNamespace)]
    public class Fernmetastase
    {
        private const string MetastasisLocationPattern = @"^PUL|OSS|HEP|BRA|LYM|MAR|PLE|PER|ADR|SKI|OTH|GEN$";
        private string _lokalisation;

        private string _typeName = typeof(Fernmetastase).Name;
      

        /// <summary>
        /// Gibt an, wann die Fernmetastase festgestellt wurde.
        /// </summary>
        [XmlElement("FM_Diagnosedatum", Order = 1)]
        public DatumTyp Datum { get; set; }

        /// <summary>
        /// Lokalisation der Fernmetastase als dreistelliger Code (siehe auch TNM-Klassifikation)
        /// </summary>
        [XmlElement("FM_Lokalisation", Order = 2)]
        public string Lokalisation
        {
            get { return _lokalisation; }
            set { _lokalisation = value.ValidateOrThrow(MetastasisLocationPattern, _typeName, nameof(this.Lokalisation)); }
        }
    }
}
